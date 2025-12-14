using BlazorMemoApp.Data;
using BlazorMemoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorMemoApp.Services;

public class BuyerPrivilegeService
{
    private readonly IServiceProvider _serviceProvider;

    public BuyerPrivilegeService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<List<int>> GetAllowedBuyerIdsAsync(string userId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var privileges = await db.UserBuyerPrivileges
            .Where(p => p.UserId == userId)
            .ToListAsync();

        // If user has "All Buyers" privilege (BuyerId is null), return empty list to indicate all access
        if (privileges.Any(p => p.BuyerId == null))
        {
            return new List<int>(); // Empty list means all buyers allowed
        }

        return privileges.Where(p => p.BuyerId.HasValue).Select(p => p.BuyerId!.Value).ToList();
    }

    public async Task<bool> HasAllBuyersAccessAsync(string userId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await db.UserBuyerPrivileges
            .AnyAsync(p => p.UserId == userId && p.BuyerId == null);
    }

    public async Task<List<MemoAdressModel>> FilterBuyersAsync(string userId, List<MemoAdressModel> allBuyers)
    {
        var hasAllAccess = await HasAllBuyersAccessAsync(userId);
        if (hasAllAccess)
        {
            return allBuyers;
        }

        var allowedBuyerIds = await GetAllowedBuyerIdsAsync(userId);
        if (allowedBuyerIds.Count == 0)
        {
            // No privileges set - return empty (or you could return all for admin flexibility)
            return new List<MemoAdressModel>();
        }

        return allBuyers.Where(b => allowedBuyerIds.Contains(b.Id)).ToList();
    }

    public async Task<List<UserBuyerPrivilegeModel>> GetUserPrivilegesAsync(string userId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await db.UserBuyerPrivileges
            .Include(p => p.Buyer)
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task SaveUserPrivilegesAsync(string userId, List<int?> buyerIds)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Remove existing privileges
        var existing = await db.UserBuyerPrivileges
            .Where(p => p.UserId == userId)
            .ToListAsync();
        db.UserBuyerPrivileges.RemoveRange(existing);

        // Add new privileges
        foreach (var buyerId in buyerIds)
        {
            db.UserBuyerPrivileges.Add(new UserBuyerPrivilegeModel
            {
                UserId = userId,
                BuyerId = buyerId
            });
        }

        await db.SaveChangesAsync();
    }
}
