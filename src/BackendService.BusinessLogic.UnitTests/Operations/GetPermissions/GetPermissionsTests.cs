using BackendService.BusinessLogic.Operations.GetPermissions;
using BackendService.BusinessLogic.Operations.GetPermissions.Models;
using BackendService.BusinessLogic.Tasks.GetPermissionsByLogin;
using BackendService.BusinessLogic.Tasks.GetPermissionsByLogin.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace BackendService.BusinessLogic.UnitTests.Operations.GetPermissions;

[TestClass]
public sealed class GetPermissionsTests
{
    private Mock<IGetPermissionsByLoginTask> _getPermissionsByLoginTask;
    private Mock<ILogger<GetPermissionsOperation>> _logger;
    
    private GetPermissionsOperation _getPermissionsOperation;

    public GetPermissionsTests()
    {
        _getPermissionsByLoginTask = new Mock<IGetPermissionsByLoginTask>();
        _logger = new Mock<ILogger<GetPermissionsOperation>>();

        _getPermissionsOperation = new GetPermissionsOperation(_getPermissionsByLoginTask.Object, _logger.Object);
    }

    [TestMethod]
    public async Task GetPermissionsTest_UserNotFound_Successfully()
    {
        _getPermissionsByLoginTask.Setup(g => g.GetAsync(It.IsAny<GetPermissionsByLoginTaskRequest>())).ReturnsAsync(() => new GetPermissionsByLoginTaskResponse(new[] { new GetPermissionsByLoginTaskRolePermission("TestRole1", new[] { new GetPermissionsByLoginTaskPermission("TestPermission1", null) }) }));

        await _getPermissionsOperation.GetAsync(new GetPermissionsOperationRequest("Login"));

        _getPermissionsByLoginTask.Verify(x => x.GetAsync(It.IsAny<GetPermissionsByLoginTaskRequest>()), Times.Once);
    }
}