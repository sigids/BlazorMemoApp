namespace BlazorMemoApp.Models;

public record PoSearchResult(string PONumber);

public class PoDetailItem
{
    public string Article { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Spec_Value { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public string Uom_Po { get; set; } = string.Empty;
    public string Custom1 { get; set; }
}
