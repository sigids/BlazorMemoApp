using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorMemoApp.Models;

/// <summary>
/// Header table for MemoAllocation
/// </summary>
public class MemoAllocationHeaderModel
{
    public int Id { get; set; }

    [Display(Name = "Unit Name")]
    public string UnitName { get; set; } = string.Empty;

    [Display(Name = "Memo #")]
    public string MemoNo { get; set; } = string.Empty;

    [Display(Name = "Date")]
    public DateTime MemoDate { get; set; } = DateTime.Now;

    [Display(Name = "PO No")]
    public string? PONo { get; set; }

    [Display(Name = "Buyer")]
    public int? BuyerId { get; set; }
    public MemoAdressModel? Buyer { get; set; }

    // Warehouse Approval
    [Display(Name = "WH Approval")]
    public bool WhApprovalStatus { get; set; } = false;
    
    [Display(Name = "WH Approval Date")]
    public DateTime? WhApprovalDate { get; set; }
    
    public string? WhApprovalUserId { get; set; }
    
    [Display(Name = "WH Approved By")]
    public string? WhApprovalUserName { get; set; }

    // QC Approval
    [Display(Name = "QC Approval")]
    public bool QcApprovalStatus { get; set; } = false;
    
    [Display(Name = "QC Approval Date")]
    public DateTime? QcApprovalDate { get; set; }
    
    public string? QcApprovalUserId { get; set; }
    
    [Display(Name = "QC Approved By")]
    public string? QcApprovalUserName { get; set; }

    public List<MemoAllocationDetailModel> Details { get; set; } = new();
}

/// <summary>
/// Detail table for MemoAllocation - populated from PO Items
/// </summary>
public class MemoAllocationDetailModel
{
    public int Id { get; set; }

    public int MemoAllocationHeaderId { get; set; }
    public MemoAllocationHeaderModel? MemoAllocationHeader { get; set; }

    [Display(Name = "PO No")]
    public string? PONo { get; set; }

    [Display(Name = "Mat Name")]
    public string MatName { get; set; } = string.Empty;

    public string Article { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;

    [Display(Name = "Item Code")]
    public string ItemCode { get; set; } = string.Empty;

    public string Supplier { get; set; } = string.Empty;

    [Display(Name = "Unit PO")]
    public string UnitPO { get; set; } = string.Empty;

    [Display(Name = "Rate PO")]
    public decimal RatePO { get; set; }

    [Display(Name = "Currency PO")]
    public string CurrencyPO { get; set; } = string.Empty;

    [Display(Name = "Qty Stage")]
    public decimal QtyStage { get; set; }

    [Display(Name = "Balance Associate")]
    public decimal QtyBlncAssc { get; set; }

    [Display(Name = "Qty Actual")]
    public decimal QtyActual { get; set; }

    [Display(Name = "Date Conf Wh")]
    public DateTime? DateConfWh { get; set; }

    [Display(Name = "Qty Pass")]
    public decimal QtyPass { get; set; }

    [Display(Name = "Date Conf Qc")]
    public DateTime? DateConfQc { get; set; }

    [Display(Name = "WH Item Approval")]
    public bool WhApprovalItem { get; set; } = false;

    [Display(Name = "QC Item Approval")]
    public bool QcApprovalItem { get; set; } = false;

    [Display(Name = "Amount Pass")]
    public decimal AmountPass => QtyPass * RatePO;

    public int? BuyerId { get; set; }
    public MemoAdressModel? Buyer { get; set; }

    [Display(Name = "Qty Allocate")]
    public decimal QtyAllocate => SpiAllocations?.Sum(s => s.QtyAllocate) ?? 0;

    [Display(Name = "Amount Allocate")]
    public decimal AmountAllocate => QtyAllocate * RatePO;

    [Display(Name = "Qty Balance")]
    public decimal QtyBalance => QtyPass - QtyAllocate;

    [Display(Name = "Amount Balance")]
    public decimal AmountBalance => QtyBalance * RatePO;

    [Display(Name = "Remark")]
    [DataType(DataType.MultilineText)]
    [Column(TypeName = "nvarchar(max)")]
    public string? Remark { get; set; }

    [Display(Name = "WH Remark")]
    [DataType(DataType.MultilineText)]
    [Column(TypeName = "nvarchar(max)")]
    public string? WhRemark { get; set; }

    [Display(Name = "QC Remark")]
    [DataType(DataType.MultilineText)]
    [Column(TypeName = "nvarchar(max)")]
    public string? QcRemark { get; set; }

    public List<MemoAllocationSpiModel> SpiAllocations { get; set; } = new();
}

/// <summary>
/// SubDetail table for MemoAllocation - SPI Allocations per Detail
/// </summary>
public class MemoAllocationSpiModel
{
    public int Id { get; set; }

    public int MemoAllocationDetailId { get; set; }
    public MemoAllocationDetailModel? MemoAllocationDetail { get; set; }

    [Display(Name = "SPI No")]
    public string SpiNo { get; set; } = string.Empty;

    [Display(Name = "Qty Allocate")]
    public decimal QtyAllocate { get; set; }

    [Display(Name = "Buyer Allocated")]
    public int? BuyerAllocatedId { get; set; }
    public MemoAdressModel? BuyerAllocated { get; set; }
}

public static class UnitMaster
{
    public static readonly List<string> Units = new() { "UnitTgr", "Unit1", "Unit2", "Unit3", "UnitJFC", "UnitFC" };

    public static readonly Dictionary<string, List<string>> UnitWarehouses = new()
    {
        { "UnitTgr", new List<string> { "War-J1" } },
        { "Unit1", new List<string> { "War-Y1" } },
        { "Unit2", new List<string> { "War-Y2", "War-Y3"} },
        { "Unit3", new List<string> { "War-K1" } },
        { "UnitFC", new List<string> { "War-FC" } },
        { "UnitJFC", new List<string> { "War-JFC" } }
    };

    public static List<string> GetWarehouses(string unitName)
    {
        return UnitWarehouses.TryGetValue(unitName, out var warehouses) ? warehouses : new List<string>();
    }
}
