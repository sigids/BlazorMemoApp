using BlazorMemoApp.Data;
using BlazorMemoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorMemoApp.Services;

public class FactoryPrivilegeService
{
    private readonly IServiceProvider _serviceProvider;

    public FactoryPrivilegeService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<FactoryRoleType?> GetUserFactoryRoleAsync(string userId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var role = await db.UserFactoryRoles
            .FirstOrDefaultAsync(r => r.UserId == userId);

        return role?.RoleType;
    }

    public async Task<bool> IsWarehouseUserAsync(string userId)
    {
        var role = await GetUserFactoryRoleAsync(userId);
        return role == FactoryRoleType.Warehouse;
    }

    public async Task<bool> IsQcUserAsync(string userId)
    {
        var role = await GetUserFactoryRoleAsync(userId);
        return role == FactoryRoleType.QC;
    }

    public async Task<bool> IsPpicUserAsync(string userId)
    {
        var role = await GetUserFactoryRoleAsync(userId);
        return role == FactoryRoleType.PPIC;
    }

    public async Task<bool> HasFactoryRoleAsync(string userId)
    {
        var role = await GetUserFactoryRoleAsync(userId);
        return role.HasValue;
    }

    public async Task<List<string>> GetAllowedUnitsAsync(string userId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var privileges = await db.UserFactoryUnitPrivileges
            .Where(p => p.UserId == userId)
            .ToListAsync();

        // If user has "All Units" privilege (UnitName is null), return empty list to indicate all access
        if (privileges.Any(p => p.UnitName == null))
        {
            return new List<string>();
        }

        return privileges.Where(p => p.UnitName != null).Select(p => p.UnitName!).ToList();
    }

    public async Task<bool> HasAllUnitsAccessAsync(string userId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await db.UserFactoryUnitPrivileges
            .AnyAsync(p => p.UserId == userId && p.UnitName == null);
    }

    public async Task<bool> HasUnitAccessAsync(string userId, string unitName)
    {
        var hasAllAccess = await HasAllUnitsAccessAsync(userId);
        if (hasAllAccess)
        {
            return true;
        }

        var allowedUnits = await GetAllowedUnitsAsync(userId);
        return allowedUnits.Contains(unitName, StringComparer.OrdinalIgnoreCase);
    }

    public async Task<List<UserFactoryRoleModel>> GetAllUserFactoryRolesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await db.UserFactoryRoles
            .Include(r => r.User)
            .ToListAsync();
    }

    public async Task<List<UserFactoryUnitPrivilegeModel>> GetUserUnitPrivilegesAsync(string userId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await db.UserFactoryUnitPrivileges
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task SaveUserFactoryRoleAsync(string userId, FactoryRoleType? roleType)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Remove existing role
        var existing = await db.UserFactoryRoles
            .Where(r => r.UserId == userId)
            .ToListAsync();
        db.UserFactoryRoles.RemoveRange(existing);

        // Add new role if specified
        if (roleType.HasValue)
        {
            db.UserFactoryRoles.Add(new UserFactoryRoleModel
            {
                UserId = userId,
                RoleType = roleType.Value
            });
        }

        await db.SaveChangesAsync();
    }

    public async Task SaveUserUnitPrivilegesAsync(string userId, List<string?> unitNames)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Remove existing privileges
        var existing = await db.UserFactoryUnitPrivileges
            .Where(p => p.UserId == userId)
            .ToListAsync();
        db.UserFactoryUnitPrivileges.RemoveRange(existing);

        // Add new privileges
        foreach (var unitName in unitNames)
        {
            db.UserFactoryUnitPrivileges.Add(new UserFactoryUnitPrivilegeModel
            {
                UserId = userId,
                UnitName = unitName
            });
        }

        await db.SaveChangesAsync();
    }
}
