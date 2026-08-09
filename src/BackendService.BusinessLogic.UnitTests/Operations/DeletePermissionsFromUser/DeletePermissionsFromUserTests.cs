using BackendService.BusinessLogic.Operations.DeletePermissionsFromUser;
using BackendService.BusinessLogic.Operations.DeletePermissionsFromUser.Models;
using DatabaseContext.AuthorizationServiceDb;
using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;

namespace BackendService.BusinessLogic.UnitTests.Operations.DeletePermissionsFromUser;

[TestClass]
public sealed class DeletePermissionsFromUserTests
{
    private IAuthorizationServiceDbContext _context;
    private Mock<ILogger<DeletePermissionsFromUserOperation>> _logger;

    private DeletePermissionsFromUserOperation _operation;
    
    public DeletePermissionsFromUserTests()
    {
        _context = new AuthorizationServiceDbContext(new DbContextOptionsBuilder<AuthorizationServiceDbContext>()
            .UseInMemoryDatabase("AuthorizationServiceDbContext")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);

        _logger = new Mock<ILogger<DeletePermissionsFromUserOperation>>();
        
        _operation = new DeletePermissionsFromUserOperation(_context, _logger.Object);
    }

    [TestMethod]
    public async Task DeletePermissionsFromUser_UserExistsInDb()
    {
        const string login = "TestLogin1";
        
        var user = await _context.User.AddAsync(new Users { Login = login });

        var role1 = await _context.Roles.AddAsync(new Roles { RoleName = "TestRoleName1", RoleCode = "TestRoleCode1", Description = "TestDescription1" });
        var permission11 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode11", PermissionName = "PermissionName11", Description = "" });
        var permission12 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode12", PermissionName = "PermissionName12", Description = "" });
        await _context.RolePermissions.AddRangeAsync(new RolePermissions { RoleId = role1.Entity.RoleId, PermissionId = permission11.Entity.PermissionId }, new RolePermissions { RoleId = role1.Entity.RoleId, PermissionId = permission12.Entity.PermissionId });
        
        var role2 = await _context.Roles.AddAsync(new Roles { RoleName = "TestRoleName2", RoleCode = "TestRoleCode2", Description = "TestDescription2" });
        var permission21 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode21", PermissionName = "PermissionName21", Description = "" });
        var permission22 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode22", PermissionName = "PermissionName22", Description = "" });
        await _context.RolePermissions.AddRangeAsync(new RolePermissions { RoleId = role2.Entity.RoleId, PermissionId = permission21.Entity.PermissionId }, new RolePermissions { RoleId = role2.Entity.RoleId, PermissionId = permission22.Entity.PermissionId });

        var restrictionType = await _context.RestrictionTypes.AddAsync(new RestrictionTypes { RestrictionTypeName = "RestrictionTypeName1", RestrictionTypeCode = "RestrictionTypeCode1", Description = "" });

        await _context.UserRoles.AddAsync(new UserRoles { UserId = user.Entity.UserId, RoleId = role1.Entity.RoleId});
        await _context.UserRoles.AddAsync(new UserRoles { UserId = user.Entity.UserId, RoleId = role2.Entity.RoleId});

        await _context.UserRestrictions.AddAsync(new UserRestrictions { UserId = user.Entity.UserId, RoleId = role1.Entity.RoleId, PermissionId = permission11.Entity.PermissionId });
        await _context.UserRestrictions.AddAsync(new UserRestrictions { UserId = user.Entity.UserId, RoleId = role1.Entity.RoleId, PermissionId = permission12.Entity.PermissionId, RestrictionTypeId = restrictionType.Entity.RestrictionTypeId, RestrictionValue = "RestrictionValue1"});

        await _context.UserRestrictions.AddAsync(new UserRestrictions { UserId = user.Entity.UserId, RoleId = role2.Entity.RoleId, PermissionId = permission21.Entity.PermissionId });
        await _context.UserRestrictions.AddAsync(new UserRestrictions { UserId = user.Entity.UserId, RoleId = role2.Entity.RoleId, PermissionId = permission22.Entity.PermissionId, RestrictionTypeId = restrictionType.Entity.RestrictionTypeId, RestrictionValue = "RestrictionValue2"});
        
        await _context.SaveChangesAsync(CancellationToken.None);
        
        await _operation.DeleteAsync(new DeletePermissionsOperationRequest(login));

        var userRoles =  await _context.UserRoles.Where(ur => ur.UserId == user.Entity.UserId).ToArrayAsync();
        Assert.AreEqual(0, userRoles.Length);
        
        var userRestrictions = _context.UserRestrictions.Where(ur => ur.UserId == user.Entity.UserId).ToArrayAsync();
        Assert.AreEqual(0, userRoles.Length);
    }
    
    [TestMethod]
    public async Task DeletePermissionsFromUser_UserNotExistsInDb()
    {
        const string login = "TestLogin2";
        
        var user = await _context.User.AddAsync(new Users { Login = login });

        await _context.SaveChangesAsync(CancellationToken.None);
        
        await _operation.DeleteAsync(new DeletePermissionsOperationRequest(login));

        var userRoles =  await _context.UserRoles.Where(ur => ur.UserId == user.Entity.UserId).ToArrayAsync();
        Assert.AreEqual(0, userRoles.Length);
        
        var userRestrictions = _context.UserRestrictions.Where(ur => ur.UserId == user.Entity.UserId).ToArrayAsync();
        Assert.AreEqual(0, userRoles.Length);
    }
}