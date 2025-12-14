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
        public DbSet<BuyerStyleOrderModel> BuyerStyleOrders { get; set; }
        public DbSet<SynchronizingModel> Synchronizings { get; set; }
        public DbSet<PoExchangeRateModel> PoExchangeRates { get; set; }
        public DbSet<MemoAttachmentModel> MemoAttachments { get; set; }
        public DbSet<EmailSettingsModel> EmailSettings { get; set; }
        public DbSet<UserBuyerPrivilegeModel> UserBuyerPrivileges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MemoDetailModel>()
                .HasMany(d => d.SpiBomDetails)
                .WithOne(s => s.MemoDetail)
                .HasForeignKey(s => s.MemoDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BuyerStyleOrderModel>()
                .HasOne(o => o.Buyer)
                .WithMany()
                .HasForeignKey(o => o.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BuyerStyleOrderModel>()
                .HasOne(o => o.Style)
                .WithMany()
                .HasForeignKey(o => o.StyleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MemoAttachmentModel>()
                .HasOne(a => a.MemoHeader)
                .WithMany(h => h.Attachments)
                .HasForeignKey(a => a.MemoHeaderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
