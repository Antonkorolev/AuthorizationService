using BackendService.BusinessLogic.Operations.GetPermissions;
using BackendService.BusinessLogic.Operations.GetPermissions.Models;
using DatabaseContext.AuthorizationServiceDb;
using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;

namespace BackendService.BusinessLogic.UnitTests.Operations.GetPermissions;

[TestClass]
public sealed class GetPermissionsTests
{
    // private readonly IAuthorizationServiceDbContext _context;
    // private readonly Mock<ILogger<GetPermissionsOperation>> _logger;
    //
    // private readonly GetPermissionsOperation _getPermissionsOperation;
    //
    // public GetPermissionsTests()
    // {
    //     _context = new AuthorizationServiceDbContext(new DbContextOptionsBuilder<AuthorizationServiceDbContext>()
    //         .UseInMemoryDatabase("AuthorizationServiceDbContext")
    //         .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
    //         .Options);
    //
    //     _logger = new Mock<ILogger<GetPermissionsOperation>>();
    //
    //     _getPermissionsOperation = new GetPermissionsOperation(_context, _logger.Object);
    // }
    //
    // [TestMethod]
    // public async Task GetPermissionsTest_UserNotFound_Successfully()
    // {
    //     const string login = "TestLogin2";
    //
    //     var response = await _getPermissionsOperation.GetAsync(new GetPermissionsOperationRequest(login));
    //     
    //     Assert.AreEqual(0, response?.RolePermissions.Count());
    // }
    //
    // [TestMethod]
    // public async Task GetPermissionsTest_UserWithoutPermissions_Successfully()
    // {
    //     const string login = "TestLogin3";
    //     
    //     await _context.User.AddAsync(new Users { Login = login });
    //
    //     await _context.SaveChangesAsync(CancellationToken.None);
    //
    //     var response = await _getPermissionsOperation.GetAsync(new GetPermissionsOperationRequest(login));
    //     
    //     Assert.AreEqual(0, response?.RolePermissions.Count());
    // }
    //
    // [TestMethod]
    // public async Task GetPermissionsTest_UserWithPermissions_Successfully()
    // {
    //     const string login = "TestLogin4";
    //     
    //     var user = await _context.User.AddAsync(new Users { Login = login });
    //     
    //     var role1 = await _context.Roles.AddAsync(new Roles { RoleName = "TestRoleName1", RoleCode = "TestRoleCode1", Description = "TestDescription1" });
    //     var permission11 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode11", PermissionName = "PermissionName11", Description = "" });
    //     var permission12 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode12", PermissionName = "PermissionName12", Description = "" });
    //     await _context.RolePermissions.AddRangeAsync(new RolePermissions { RoleId = role1.Entity.RoleId, PermissionId = permission11.Entity.PermissionId }, new RolePermissions { RoleId = role1.Entity.RoleId, PermissionId = permission12.Entity.PermissionId });
    //     
    //     var role2 = await _context.Roles.AddAsync(new Roles { RoleName = "TestRoleName2", RoleCode = "TestRoleCode2", Description = "TestDescription2" });
    //     var permission21 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode21", PermissionName = "PermissionName21", Description = "" });
    //     var permission22 = await _context.Permissions.AddAsync(new Permissions { PermissionCode = "PermissionCode22", PermissionName = "PermissionName22", Description = "" });
    //     await _context.RolePermissions.AddRangeAsync(new RolePermissions { RoleId = role2.Entity.RoleId, PermissionId = permission21.Entity.PermissionId }, new RolePermissions { RoleId = role2.Entity.RoleId, PermissionId = permission22.Entity.PermissionId });
    //     
    //     var restrictionType = await _context.RestrictionTypes.AddAsync(new RestrictionTypes { RestrictionTypeName = "RestrictionTypeName1", RestrictionTypeCode = "RestrictionTypeCode1", Description = "" });
    //     
    //     await _context.UserRoles.AddAsync(new UserRoles { UserId = user.Entity.UserId, RoleId = role1.Entity.RoleId});
    //     await _context.UserRoles.AddAsync(new UserRoles { UserId = user.Entity.UserId, RoleId = role2.Entity.RoleId});
    //     
    //     await _context.UserRestrictions.AddAsync(new UserRestrictions { UserId = user.Entity.UserId, RoleId = role1.Entity.RoleId, PermissionId = permission11.Entity.PermissionId });
    //     await _context.UserRestrictions.AddAsync(new UserRestrictions { UserId = user.Entity.UserId, RoleId = role1.Entity.RoleId, PermissionId = permission12.Entity.PermissionId, RestrictionTypeId = restrictionType.Entity.RestrictionTypeId, RestrictionValue = "RestrictionValue1"});
    //     
    //     await _context.UserRestrictions.AddAsync(new UserRestrictions { UserId = user.Entity.UserId, RoleId = role2.Entity.RoleId, PermissionId = permission21.Entity.PermissionId });
    //     await _context.UserRestrictions.AddAsync(new UserRestrictions { UserId = user.Entity.UserId, RoleId = role2.Entity.RoleId, PermissionId = permission22.Entity.PermissionId });
    //     
    //     await _context.SaveChangesAsync(CancellationToken.None);
    //     
    //     var response = await _getPermissionsOperation.GetAsync(new GetPermissionsOperationRequest(login));
    //     
    //     Assert.AreEqual(2, response?.RolePermissions.Count());
    //
    //     var firstRole = response?.RolePermissions.First();
    //     Assert.IsNotNull(firstRole);
    //     Assert.AreEqual("TestRoleCode1", firstRole.RoleCode);
    //     
    //     Assert.AreEqual(2, firstRole.Permissions.Count());
    //
    //     var firstRoleFirstPermission = firstRole.Permissions.First();
    //     Assert.AreEqual("PermissionCode11", firstRoleFirstPermission.PermissionCode);
    //     Assert.IsNull(firstRoleFirstPermission.Restrictions);
    //     
    //     var firstRoleSecondPermission = firstRole.Permissions.Last();
    //     Assert.AreEqual("PermissionCode12", firstRoleSecondPermission.PermissionCode);
    //     Assert.AreEqual("RestrictionTypeCode1", firstRoleSecondPermission.Restrictions?.RestrictionTypeCode);
    //     Assert.AreEqual("RestrictionValue1", firstRoleSecondPermission.Restrictions?.RestrictionValue);
    //     
    //     var secondRole = response?.RolePermissions.Last();
    //     Assert.IsNotNull(secondRole);
    //     Assert.AreEqual("TestRoleCode2", secondRole.RoleCode);
    //     
    //     Assert.AreEqual(2, secondRole.Permissions.Count());
    //
    //     var secondRoleFirstPermission = secondRole.Permissions.First();
    //     Assert.AreEqual("PermissionCode21", secondRoleFirstPermission.PermissionCode);
    //     Assert.IsNull(secondRoleFirstPermission.Restrictions);
    //     
    //     var secondRoleSecondPermission = secondRole.Permissions.Last();
    //     Assert.AreEqual("PermissionCode22", secondRoleSecondPermission.PermissionCode);
    //     Assert.IsNull(secondRoleSecondPermission.Restrictions);
    // }
}