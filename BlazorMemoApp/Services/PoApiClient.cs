using BlazorMemoApp.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Xml.Linq;

namespace BlazorMemoApp.Services;

public class PoApiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<PoApiClient> _logger;

    public PoApiClient(HttpClient http, ILogger<PoApiClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    // GET /Api/GetPoes?pono={query}
    public async Task<List<string>> SearchPosAsync(string query, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<string>();
        var url = $"/Api/GetPoes?pono={Uri.EscapeDataString(query)}";
        
        string xml = string.Empty;
        List<string> list = new();
        // First, try JSON (if the API ever returns JSON array of strings)
        try
        {
            var jsonResult = await _http.GetFromJsonAsync<List<string>>(url, ct);
            if (jsonResult != null && jsonResult.Count > 0)
            {
                return jsonResult;
            }
        }
        catch (Exception jsonEx)
        {
            _logger.LogDebug(jsonEx, "SearchPosAsync JSON parse failed. Will try XML. BaseAddress={Base}, Url={Url}", _http.BaseAddress, url);
        }

        // Fallback to XML parsing (current API returns XML with DataContract namespace)
        try
        {
            xml = await _http.GetStringAsync(url, ct);
            list = ParsePoListFromXml(xml);
            return list;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to search POs. BaseAddress={Base}, Url={Url},xml= {xml}, list = {list}", _http.BaseAddress, url, xml, list);
            return new List<string>();
        }
    }

    private static List<string> ParsePoListFromXml(string xml)
    {
        var result = new List<string>();
        if (string.IsNullOrWhiteSpace(xml)) return result;

        //Console.WriteLine("cek xml " + xml);
        
        
        try
        {
            
            using var doc = JsonDocument.Parse(xml);

            foreach (var element in doc.RootElement.EnumerateArray())
            {
                if (element.TryGetProperty("Pono", out var pono))
                {
                    result.Add(pono.GetString()?.Trim() ?? "");
                }
            }

            return result.Distinct().ToList();
        }
        catch
        {
            
            // If XML is malformed or unexpected, just return whatever we collected so far
            Console.WriteLine("Catch Xml issue");
            return result;
        }
    }

    // GET /Api/GetPoH2HDetail?pono={selectedPO}
    public async Task<List<PoDetailItem>> GetPoDetailsAsync(string poNumber, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(poNumber)) return new List<PoDetailItem>();
        var url = $"/Api/GetPoH2HDetail?pono={Uri.EscapeDataString(poNumber)}";

        // Try JSON first
        try
        {
            var result = await _http.GetFromJsonAsync<List<PoDetailItem>>(url, ct);
            if (result != null && result.Count > 0)
            {
                return result;
            }
        }
        catch (Exception jsonEx)
        {
            _logger.LogDebug(jsonEx, "GetPoDetailsAsync JSON parse failed. Will try XML. BaseAddress={Base}, Url={Url}", _http.BaseAddress, url);
        }

        // Fallback to XML parsing
        try
        {
            var xml = await _http.GetStringAsync(url, ct);
            var list = ParsePoDetailsFromXml(xml);

            //_logger.LogInformation("Fetched {xml} PO detail items for PO {PONumber}",xml , poNumber);

            return list;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get PO details. BaseAddress={Base}, Url={Url}", _http.BaseAddress, url);
            return new List<PoDetailItem>();
        }
    }

    private static List<PoDetailItem> ParsePoDetailsFromXml(string xml)
    {
        var items = new List<PoDetailItem>();
        if (string.IsNullOrWhiteSpace(xml)) return items;

        var doc = XDocument.Parse(xml);
        System.Diagnostics.Debug.WriteLine("cek xml detail " + xml);
        // Find item-like elements that have expected children (namespace-agnostic)
        // 
        var candidates = doc
            .Descendants()
            .Where(e => e.Elements().Any())
            .Where(e => e.Elements().Any(c => c.Name.LocalName == "Article")
                        && e.Elements().Any(c => c.Name.LocalName == "Color")
                        && e.Elements().Any(c => c.Name.LocalName == "Spec_Value")
                        && e.Elements().Any(c => c.Name.LocalName == "Rate")
                        && e.Elements().Any(c => c.Name.LocalName == "Uom_Po"));

        foreach (var el in candidates)
        {
            string GetStr(string name) => el.Elements().FirstOrDefault(c => c.Name.LocalName == name)?.Value?.Trim() ?? string.Empty;
            decimal GetDec(string name)
            {
                var s = GetStr(name);
                return decimal.TryParse(s, out var d) ? d : 0m;
            }

            var item = new PoDetailItem
            {
                Article = GetStr("Article"),
                Color = GetStr("Color"),
                Spec_Value = GetStr("Spec_Value"),
                Rate = GetDec("Rate"),
                Uom_Po = GetStr("Uom_Po")
            };
            items.Add(item);
        }

        return items;
    }
}
