using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions.Models;
using DatabaseContext.AuthorizationServiceDb;
using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;

namespace BackendService.BusinessLogic.UnitTests.Operations.AddPermissionsToUser.Tasks;

[TestClass]
public sealed class AddUserPermissionsTaskTests
{
    private AuthorizationServiceDbContext _context;
    private Mock<ILogger<AddUserPermissionsTask>> _logger;
    
    private AddUserPermissionsTask _addUserPermissionsTask;
    
    public AddUserPermissionsTaskTests()
    {
        _context = new AuthorizationServiceDbContext(new DbContextOptionsBuilder<AuthorizationServiceDbContext>()
            .UseInMemoryDatabase("AuthorizationServiceDb")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);

        _logger = new Mock<ILogger<AddUserPermissionsTask>>();
        
        _addUserPermissionsTask = new AddUserPermissionsTask(_context, _logger.Object);
    }

    [TestMethod]
    public async Task AddEmptyPermissions()
    {
        var userEntityEntry = await _context.User.AddAsync(new Users { Login = "TestLogin1"});
        await _context.SaveChangesAsync(CancellationToken.None);
        
        await _addUserPermissionsTask.AddAsync(new AddUserPermissionsTaskRequest(userEntityEntry.Entity.UserId, Enumerable.Empty<AddUserPermissionsTaskRoleInfo>()));

        var userRole = await _context.UserRoles.Where(ur => ur.UserId == userEntityEntry.Entity.UserId).ToArrayAsync();
        Assert.AreEqual(0, userRole.Length);

        var userRestrictions = await _context.UserRestrictions.Where(ur => ur.UserId == userEntityEntry.Entity.UserId).ToArrayAsync();
        Assert.AreEqual(0, userRestrictions.Length);
    }
    
    [TestMethod]
    public async Task AddPermissionsWithoutRestrictions()
    {
        const string roleCode1 = "RoleCode1";
        const string roleCode2 = "RoleCode2";

        var userEntityEntry = await _context.User.AddAsync(new Users { Login = "TestLogin2"});
        
        var role1 = await _context.Roles.AddAsync(new Roles { RoleCode = roleCode1, RoleName = "Test1", Description = "" });
        var permission11 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode1", PermissionName = "PermissionName1", Description = "" });
        var permission12 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode1", PermissionName = "PermissionName1", Description = "" });
        await _context.RolePermissions.AddRangeAsync(new RolePermissions { RoleId = role1.Entity.RoleId, PermissionId = permission11.Entity.PermissionId }, new RolePermissions { RoleId = role1.Entity.RoleId, PermissionId = permission12.Entity.PermissionId });
        
        var role2 = await _context.Roles.AddAsync(new Roles { RoleCode = roleCode2, RoleName = "Test2", Description = "" });
        var permission21 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode1", PermissionName = "PermissionName1", Description = "" });
        var permission22 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode1", PermissionName = "PermissionName1", Description = "" });
        await _context.RolePermissions.AddRangeAsync(new RolePermissions { RoleId = role2.Entity.RoleId, PermissionId = permission21.Entity.PermissionId }, new RolePermissions { RoleId = role2.Entity.RoleId, PermissionId = permission22.Entity.PermissionId });
        
        await _context.SaveChangesAsync(CancellationToken.None);
        
        await _addUserPermissionsTask.AddAsync(new AddUserPermissionsTaskRequest(userEntityEntry.Entity.UserId, new [] { new AddUserPermissionsTaskRoleInfo(roleCode1, null, null), new AddUserPermissionsTaskRoleInfo(roleCode2, null, null)}));

        var userRoles = await _context.UserRoles.Where(ur => ur.UserId == userEntityEntry.Entity.UserId).Select(ur => ur.RoleId).ToArrayAsync();
        Assert.AreEqual(2, userRoles.Length);

        var firstRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode == roleCode1);
        Assert.IsNotNull(firstRole);
        Assert.IsTrue(userRoles.Contains(firstRole.RoleId));

        var secondRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode == roleCode2);
        Assert.IsNotNull(secondRole);
        Assert.IsTrue(userRoles.Contains(secondRole.RoleId));

        var userRestrictions = await _context.UserRestrictions.Where(ur => ur.UserId == userEntityEntry.Entity.UserId).ToArrayAsync();
        Assert.AreEqual(4, userRestrictions.Length);
        
        var firstUserRestriction = userRestrictions.FirstOrDefault(r => r.PermissionId == permission11.Entity.PermissionId);
        Assert.IsNotNull(firstUserRestriction);
        Assert.IsNull(firstUserRestriction.RestrictionTypeId);
        Assert.IsNull(firstUserRestriction.RestrictionValue);

        var secondUserRestriction = userRestrictions.FirstOrDefault(r => r.PermissionId == permission12.Entity.PermissionId);
        Assert.IsNotNull(secondUserRestriction);
        Assert.IsNull(secondUserRestriction.RestrictionTypeId);
        Assert.IsNull(secondUserRestriction.RestrictionValue);
        
        var thirdUserRestriction = userRestrictions.FirstOrDefault(r => r.PermissionId == permission21.Entity.PermissionId);
        Assert.IsNotNull(thirdUserRestriction);
        Assert.IsNull(thirdUserRestriction.RestrictionTypeId);
        Assert.IsNull(thirdUserRestriction.RestrictionValue);
        
        var fourthUserRestriction = userRestrictions.FirstOrDefault(r => r.PermissionId == permission22.Entity.PermissionId);
        Assert.IsNotNull(fourthUserRestriction);
        Assert.IsNull(fourthUserRestriction.RestrictionTypeId);
        Assert.IsNull(fourthUserRestriction.RestrictionValue);
    }

    [TestMethod]
    public async Task AddPermissionsWithRestrictionsForSomeRoles()
    {
        const string roleCode1 = "RoleCode21";
        const string roleCode2 = "RoleCode22";

        const string restrictionValue1 = "TestRestrictionValue2";
        
        var userEntityEntry = await _context.User.AddAsync(new Users { Login = "TestLogin3"});
        
        var role1 = await _context.Roles.AddAsync(new Roles { RoleCode = roleCode1, RoleName = "Test2", Description = "" });
        var permission11 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode111", PermissionName = "PermissionName111", Description = "" });
        var permission12 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode112", PermissionName = "PermissionName112", Description = "" });
        await _context.RolePermissions.AddRangeAsync(new RolePermissions { RoleId = role1.Entity.RoleId, PermissionId = permission11.Entity.PermissionId }, new RolePermissions { RoleId = role1.Entity.RoleId, PermissionId = permission12.Entity.PermissionId });
        
        var role2 = await _context.Roles.AddAsync(new Roles { RoleCode = roleCode2, RoleName = "Test3", Description = "" });
        var permission21 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode221", PermissionName = "PermissionName221", Description = "" });
        var permission22 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode222", PermissionName = "PermissionName1", Description = "" });
        await _context.RolePermissions.AddRangeAsync(new RolePermissions { RoleId = role2.Entity.RoleId, PermissionId = permission21.Entity.PermissionId }, new RolePermissions { RoleId = role2.Entity.RoleId, PermissionId = permission22.Entity.PermissionId });

        var restrictionType = await _context.RestrictionTypes.AddAsync(new RestrictionTypes { RestrictionTypeCode = "TestRestrictionType1", RestrictionTypeName = "RestrictionType Name", Description = "" });
        
        await _context.SaveChangesAsync(CancellationToken.None);
        
        await _addUserPermissionsTask.AddAsync(new AddUserPermissionsTaskRequest(userEntityEntry.Entity.UserId, new [] { new AddUserPermissionsTaskRoleInfo(roleCode1, restrictionType.Entity.RestrictionTypeCode, restrictionValue1), new AddUserPermissionsTaskRoleInfo(roleCode2, null, null)}));

        var userRoles = await _context.UserRoles.Where(ur => ur.UserId == userEntityEntry.Entity.UserId).Select(ur => ur.RoleId).ToArrayAsync();
        Assert.AreEqual(2, userRoles.Length);

        var firstRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode == roleCode1);
        Assert.IsNotNull(firstRole);
        Assert.IsTrue(userRoles.Contains(firstRole.RoleId));

        var secondRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode == roleCode2);
        Assert.IsNotNull(secondRole);
        Assert.IsTrue(userRoles.Contains(secondRole.RoleId));

        var userRestrictions = await _context.UserRestrictions.Where(ur => ur.UserId == userEntityEntry.Entity.UserId).ToArrayAsync();
        Assert.AreEqual(4, userRestrictions.Length);
        
        var firstUserRestriction = userRestrictions.FirstOrDefault(r => r.PermissionId == permission11.Entity.PermissionId);
        Assert.IsNotNull(firstUserRestriction);
        Assert.AreEqual(restrictionType.Entity.RestrictionTypeId, firstUserRestriction.RestrictionTypeId);
        Assert.AreEqual(restrictionValue1, firstUserRestriction.RestrictionValue);

        var secondUserRestriction = userRestrictions.FirstOrDefault(r => r.PermissionId == permission12.Entity.PermissionId);
        Assert.IsNotNull(secondUserRestriction);
        Assert.AreEqual(restrictionType.Entity.RestrictionTypeId, secondUserRestriction.RestrictionTypeId);
        Assert.AreEqual(restrictionValue1, secondUserRestriction.RestrictionValue);
        
        var thirdUserRestriction = userRestrictions.FirstOrDefault(r => r.PermissionId == permission21.Entity.PermissionId);
        Assert.IsNotNull(thirdUserRestriction);
        Assert.IsNull(thirdUserRestriction.RestrictionTypeId);
        Assert.IsNull(thirdUserRestriction.RestrictionValue);
        
        var fourthUserRestriction = userRestrictions.FirstOrDefault(r => r.PermissionId == permission22.Entity.PermissionId);
        Assert.IsNotNull(fourthUserRestriction);
        Assert.IsNull(fourthUserRestriction.RestrictionTypeId);
        Assert.IsNull(fourthUserRestriction.RestrictionValue);
    }

    [TestMethod]
    public async Task AddPermissionsWithRestrictionsForAllRoles()
    {
        const string roleCode1 = "RoleCode12";
        const string roleCode2 = "RoleCode121";

        const string restrictionValue1 = "TestRestrictionValue12";
        const string restrictionValue2 = "TestRestrictionValue11";
        
        var userEntityEntry = await _context.User.AddAsync(new Users { Login = "TestLogin4"});
        
        var role1 = await _context.Roles.AddAsync(new Roles { RoleCode = roleCode1, RoleName = "Test4", Description = "" });
        var permission11 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode11", PermissionName = "PermissionName1", Description = "" });
        var permission12 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode12", PermissionName = "PermissionName1", Description = "" });
        await _context.RolePermissions.AddRangeAsync(new RolePermissions { RoleId = role1.Entity.RoleId, PermissionId = permission11.Entity.PermissionId }, new RolePermissions { RoleId = role1.Entity.RoleId, PermissionId = permission12.Entity.PermissionId });
        
        var role2 = await _context.Roles.AddAsync(new Roles { RoleCode = roleCode2, RoleName = "Test5", Description = "" });
        var permission21 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode21", PermissionName = "PermissionName1", Description = "" });
        var permission22 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode21", PermissionName = "PermissionName1", Description = "" });
        await _context.RolePermissions.AddRangeAsync(new RolePermissions { RoleId = role2.Entity.RoleId, PermissionId = permission21.Entity.PermissionId }, new RolePermissions { RoleId = role2.Entity.RoleId, PermissionId = permission22.Entity.PermissionId });

        var restrictionType1 = await _context.RestrictionTypes.AddAsync(new RestrictionTypes { RestrictionTypeCode = "TestRestrictionType2", RestrictionTypeName = "RestrictionTypeName1", Description = "" });
        var restrictionType2 = await _context.RestrictionTypes.AddAsync(new RestrictionTypes { RestrictionTypeCode = "TestRestrictionType3", RestrictionTypeName = "RestrictionTypeName2", Description = "" });
        
        await _context.SaveChangesAsync(CancellationToken.None);
        
        await _addUserPermissionsTask.AddAsync(new AddUserPermissionsTaskRequest(userEntityEntry.Entity.UserId, new [] { new AddUserPermissionsTaskRoleInfo(roleCode1, restrictionType1.Entity.RestrictionTypeCode, restrictionValue1), new AddUserPermissionsTaskRoleInfo(roleCode2, restrictionType2.Entity.RestrictionTypeCode, restrictionValue2)}));

        var userRoles = await _context.UserRoles.Where(ur => ur.UserId == userEntityEntry.Entity.UserId).Select(ur => ur.RoleId).ToArrayAsync();
        Assert.AreEqual(2, userRoles.Length);

        var firstRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode == roleCode1);
        Assert.IsNotNull(firstRole);
        Assert.IsTrue(userRoles.Contains(firstRole.RoleId));

        var secondRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode == roleCode2);
        Assert.IsNotNull(secondRole);
        Assert.IsTrue(userRoles.Contains(secondRole.RoleId));

        var userRestrictions = await _context.UserRestrictions.Where(ur => ur.UserId == userEntityEntry.Entity.UserId).ToArrayAsync();
        Assert.AreEqual(4, userRestrictions.Length);
        
        var firstUserRestriction = userRestrictions.FirstOrDefault(r => r.PermissionId == permission11.Entity.PermissionId);
        Assert.IsNotNull(firstUserRestriction);
        Assert.AreEqual(restrictionType1.Entity.RestrictionTypeId, firstUserRestriction.RestrictionTypeId);
        Assert.AreEqual(restrictionValue1, firstUserRestriction.RestrictionValue);

        var secondUserRestriction = userRestrictions.FirstOrDefault(r => r.PermissionId == permission12.Entity.PermissionId);
        Assert.IsNotNull(secondUserRestriction);
        Assert.AreEqual(restrictionType1.Entity.RestrictionTypeId, secondUserRestriction.RestrictionTypeId);
        Assert.AreEqual(restrictionValue1, secondUserRestriction.RestrictionValue);
        
        var thirdUserRestriction = userRestrictions.FirstOrDefault(r => r.PermissionId == permission21.Entity.PermissionId);
        Assert.IsNotNull(thirdUserRestriction);
        Assert.AreEqual(restrictionType2.Entity.RestrictionTypeId, thirdUserRestriction.RestrictionTypeId);
        Assert.AreEqual(restrictionValue2, thirdUserRestriction.RestrictionValue);
        
        var fourthUserRestriction = userRestrictions.FirstOrDefault(r => r.PermissionId == permission22.Entity.PermissionId);
        Assert.IsNotNull(fourthUserRestriction);
        Assert.AreEqual(restrictionType2.Entity.RestrictionTypeId, fourthUserRestriction.RestrictionTypeId);
        Assert.AreEqual(restrictionValue2, fourthUserRestriction.RestrictionValue);
    }
}