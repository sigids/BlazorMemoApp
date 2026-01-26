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
        public DbSet<UserFactoryRoleModel> UserFactoryRoles { get; set; }
        public DbSet<UserFactoryUnitPrivilegeModel> UserFactoryUnitPrivileges { get; set; }
        public DbSet<MemoAllocationHeaderModel> MemoAllocations { get; set; }
        public DbSet<MemoAllocationDetailModel> MemoAllocationDetails { get; set; }
        public DbSet<MemoAllocationSpiModel> MemoAllocationSpis { get; set; }
        public DbSet<MemoMultiStyleModel> MemoMultiStyles { get; set; }

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

            // MemoAllocation Header -> Buyer relationship
            modelBuilder.Entity<MemoAllocationHeaderModel>()
                .HasOne(h => h.Buyer)
                .WithMany()
                .HasForeignKey(h => h.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);

            // MemoAllocation Header -> Detail relationship
            modelBuilder.Entity<MemoAllocationDetailModel>()
                .HasOne(d => d.MemoAllocationHeader)
                .WithMany(h => h.Details)
                .HasForeignKey(d => d.MemoAllocationHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MemoAllocationDetailModel>()
                .HasOne(d => d.Buyer)
                .WithMany()
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);

            // MemoAllocation Detail -> SPI SubDetail relationship
            modelBuilder.Entity<MemoAllocationSpiModel>()
                .HasOne(s => s.MemoAllocationDetail)
                .WithMany(d => d.SpiAllocations)
                .HasForeignKey(s => s.MemoAllocationDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MemoAllocationSpiModel>()
                .HasOne(s => s.BuyerAllocated)
                .WithMany()
                .HasForeignKey(s => s.BuyerAllocatedId)
                .OnDelete(DeleteBehavior.NoAction);

            // MemoMultiStyle -> MemoHeader relationship
            modelBuilder.Entity<MemoMultiStyleModel>()
                .HasOne(m => m.MemoHeader)
                .WithMany(h => h.MultiStyles)
                .HasForeignKey(m => m.MemoHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MemoMultiStyleModel>()
                .HasOne(m => m.Style)
                .WithMany()
                .HasForeignKey(m => m.StyleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
