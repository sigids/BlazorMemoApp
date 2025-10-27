namespace BlazorMemoApp.Models;

public record PoSearchResult(string PONumber);

public class PoDetailItem
{
    public string Article { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public string UOM { get; set; } = string.Empty;
}
