using BlazorMemoApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorMemoApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<MemoHeaderModel> Memos { get; set; }
        public DbSet<MemoDetailModel> MemoDetails { get; set; }
        public DbSet<MemoAdressModel> MemoAddresses { get; set; }
        public DbSet<BuyerStyleModel> BuyerStyles { get; set; }
    }
}
