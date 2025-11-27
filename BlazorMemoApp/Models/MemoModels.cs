using System.ComponentModel.DataAnnotations;
using BlazorMemoApp.Data;

namespace BlazorMemoApp.Models;

public class MemoHeaderModel
{
    public int Id { get; set; }

    [Display(Name = "Memo No")]
    public string MemoNo { get; set; } = string.Empty; // Stubbed auto-gen in UI

    [Display(Name = "Memo Date")]
    public DateTime MemoDate { get; set; } = DateTime.Today;

    public int? BuyerId { get; set; }
    public int? StyleId { get; set; }
    public int? SupplierId { get; set; }

    [Display(Name = "PO Number")]
    public string? PONumber { get; set; }

    public int? GmtQty { get; set; }
    public decimal? GmtFobValue { get; set; }

    // Approval fields
    [Display(Name = "Approval Status")]
    public string ApproveStatus { get; set; } = "Pending"; // Pending | Approved | Rejected (future)

    public string? ApproveUserId { get; set; }
    public ApplicationUser? ApproveUser { get; set; }

    [Display(Name = "Approval Date")]
    public DateTime? ApproveDate { get; set; }

    public List<MemoDetailModel> Details { get; set; } = new();
}

public class MemoDetailModel
{
    public int Id { get; set; }
    public int MemoHeaderId { get; set; }
    public MemoHeaderModel MemoHeader { get; set; } = null!;

    // From PO Detail API
    public string? PONumber { get; set; }
    public string Article { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Unit { get; set; } = string.Empty;

    // Editable inputs
    public int BOMQty { get; set; }
    public int AvailStockQty { get; set; }
    public int MCQQty { get; set; }
    public decimal SurchargePaid { get; set; }
    public int StockUsableQty { get; set; }
    public decimal TotalExtraCollected { get; set; }

    public string ItemCode { get; set; } = string.Empty;

    // Computed fields (per PRD)
    public decimal BOMAmount => Price * BOMQty;
    public decimal StockAmount => Price * AvailStockQty;
    public int PurchaseQty => BOMQty - AvailStockQty;
    public decimal PurchaseAmount => Price * PurchaseQty;
    public decimal MCQAmount => Price * MCQQty;
    public decimal Diff => MCQAmount - PurchaseAmount;
    public int StockFromMCQ => MCQQty - PurchaseQty;
    public decimal StockUsableAmount => Price * StockUsableQty;
    public int StockNonUsableQty => MCQQty - PurchaseQty - StockUsableQty;
    public decimal StockNonUsableAmount => Price * StockNonUsableQty;
    public decimal TotalExtraPaid => Diff + SurchargePaid;

}

public class MemoAdressModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameAbbr { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;    
}

public class BuyerStyleModel
{
    public int Id { get; set; }
    public int BuyerId { get; set; }
    public MemoAdressModel? Buyer { get; set; }
    public string StyleName { get; set; } = string.Empty;
    public string? Description { get; set; }
}