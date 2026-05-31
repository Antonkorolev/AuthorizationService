using BackendService.BusinessLogic.Operations.AddPermissionsToUser;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Models;
using DatabaseContext.AuthorizationServiceDb;
using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;

namespace BackendService.BusinessLogic.UnitTests.Operations;

[TestClass]
public sealed class AddPermissionsToUserTests
{
    private IAuthorizationServiceDbContext _context;
    private Mock<ILogger<AddPermissionsToUserOperation>> _logger;
    private AddPermissionsToUserOperation _addPermissionsToUserOperation;

    public AddPermissionsToUserTests()
    {
        _context = new AuthorizationServiceDbContext(new DbContextOptionsBuilder<AuthorizationServiceDbContext>()
            .UseInMemoryDatabase($"AuthorizationServiceDbContext")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);

        _logger = new Mock<ILogger<AddPermissionsToUserOperation>>();

        //_addPermissionsToUserOperation = new AddPermissionsToUserOperation(_context, _logger.Object);
    }

    [TestMethod]
    public async Task AddPermsToUserWithoutGroups()
    {
        const string login = "TestLogin";

        var role1 = new Roles { RoleId = 1, RoleName = "Group Reader", RoleCode = "GroupReader", Description = "" };
        var permission1 = new Permissions { PermissionId = 1, PermissionName = "View Files List", PermissionCode = "ViewFileList", Description = ""};
        var permission2 = new Permissions { PermissionId = 2, PermissionName = "View Files Details", PermissionCode = "ViewFilesDetails", Description = ""};
        
        var role2 = new Roles { RoleId = 2, RoleName = "Group Changer", RoleCode = "GroupChanger", Description = string.Empty };
        var permission3 = new Permissions { PermissionId = 3, PermissionName = "Upload Files", PermissionCode = "Upload Files", Description = ""};
        var permission4 = new Permissions { PermissionId = 4, PermissionName = "Update Files", PermissionCode = "UpdateFiles", Description = ""};
        
        var role3 = new Roles { RoleId = 3, RoleName = "Group Deleter", RoleCode = "GroupDeleter", Description = string.Empty };
        var permission5 = new Permissions { PermissionId = 5, PermissionName = "Delete Files", PermissionCode = "DeleteFiles", Description = ""};
        
        var restrictionType = new RestrictionTypes { RestrictionTypeId = 1, RestrictionTypeName = "Group Id", RestrictionTypeCode = "GroupId", Description = "" };
        
        await _context.User.AddAsync(new Users { UserId = 1, Login = login });
        
        var permissionEntityEntry1 = await _context.Permissions.AddAsync(permission1);
        var permissionEntityEntry2 = await _context.Permissions.AddAsync(permission2);
        var permissionEntityEntry3 = await _context.Permissions.AddAsync(permission3);
        var permissionEntityEntry4 = await _context.Permissions.AddAsync(permission4);
        var permissionEntityEntry5 = await _context.Permissions.AddAsync(permission5);
        
        var roleEntityEntry1 = await _context.Roles.AddAsync(role1);
        var roleEntityEntry2 = await _context.Roles.AddAsync(role2);
        var roleEntityEntry3 = await _context.Roles.AddAsync(role3);

        await _context.RolePermissions.AddAsync(new RolePermissions { RoleId = roleEntityEntry1.Entity.RoleId, PermissionId = permissionEntityEntry1.Entity.PermissionId });
        await _context.RolePermissions.AddAsync(new RolePermissions { RoleId = roleEntityEntry1.Entity.RoleId, PermissionId = permissionEntityEntry2.Entity.PermissionId });
        await _context.RolePermissions.AddAsync(new RolePermissions { RoleId = roleEntityEntry2.Entity.RoleId, PermissionId = permissionEntityEntry3.Entity.PermissionId });
        await _context.RolePermissions.AddAsync(new RolePermissions { RoleId = roleEntityEntry2.Entity.RoleId, PermissionId = permissionEntityEntry4.Entity.PermissionId });
        await _context.RolePermissions.AddAsync(new RolePermissions { RoleId = roleEntityEntry3.Entity.RoleId, PermissionId = permissionEntityEntry5.Entity.PermissionId });

        await _context.RestrictionTypes.AddAsync(restrictionType);
        
        await _context.SaveChangesAsync(CancellationToken.None);

        await _addPermissionsToUserOperation.AddAsync(new AddPermissionsOperationRequest(
            login, 
            new[]
            {
                new RoleInfo(role1.RoleCode, restrictionType.RestrictionTypeCode, "1"),
                new RoleInfo(role2.RoleCode, restrictionType.RestrictionTypeCode, "2")
            }));

        var userRoles = await _context.UserRoles.Where(ur => ur.UserId == 1).OrderBy(ur => ur.RoleId).ToArrayAsync();
        Assert.AreEqual(2, userRoles.Length);
        Assert.AreEqual(1, userRoles.First().RoleId);
        Assert.AreEqual(2, userRoles.Last().RoleId);

        var userRestrictions = await _context.UserRestrictions.Where(ur => ur.UserId == 1).ToArrayAsync();
        Assert.AreEqual(4, userRestrictions.Length);

        var userRestriction1 = userRestrictions.FirstOrDefault(ur => ur.PermissionId == permission1.PermissionId);
        Assert.IsNotNull(userRestriction1);
        Assert.AreEqual(role1.RoleId, userRestriction1.RoleId);
        Assert.AreEqual(restrictionType.RestrictionTypeId, userRestriction1.RestrictionTypeId);
        Assert.AreEqual("1", userRestriction1.RestrictionValue);
        
        var userRestriction2 = userRestrictions.FirstOrDefault(ur => ur.PermissionId == permission2.PermissionId);
        Assert.IsNotNull(userRestriction2);
        Assert.AreEqual(role1.RoleId, userRestriction2.RoleId);
        Assert.AreEqual(restrictionType.RestrictionTypeId, userRestriction2.RestrictionTypeId);
        Assert.AreEqual("1", userRestriction2.RestrictionValue);
        
        var userRestriction3 = userRestrictions.FirstOrDefault(ur => ur.PermissionId == permission3.PermissionId);
        Assert.IsNotNull(userRestriction3);
        Assert.AreEqual(role1.RoleId, userRestriction3.RoleId);
        Assert.AreEqual(restrictionType.RestrictionTypeId, userRestriction3.RestrictionTypeId);
        Assert.AreEqual("2", userRestriction3.RestrictionValue);
        
        var userRestriction4 = userRestrictions.FirstOrDefault(ur => ur.PermissionId == permission4.PermissionId);
        Assert.IsNotNull(userRestriction4);
        Assert.AreEqual(role1.RoleId, userRestriction4.RoleId);
        Assert.AreEqual(restrictionType.RestrictionTypeId, userRestriction4.RestrictionTypeId);
        Assert.AreEqual("2", userRestriction4.RestrictionValue);
    }
}