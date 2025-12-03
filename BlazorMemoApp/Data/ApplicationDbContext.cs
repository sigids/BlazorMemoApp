using BlazorMemoApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorMemoApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<MemoHeaderModel> Memos { get; set; }
        public DbSet<MemoDetailModel> MemoDetails { get; set; }
        public DbSet<SpiBomDetailModel> SpiBomDetails { get; set; }
        public DbSet<MemoAdressModel> MemoAddresses { get; set; }
        public DbSet<BuyerStyleModel> BuyerStyles { get; set; }
        public DbSet<PoExchangeRateModel> PoExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MemoDetailModel>()
                .HasMany(d => d.SpiBomDetails)
                .WithOne(s => s.MemoDetail)
                .HasForeignKey(s => s.MemoDetailId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
