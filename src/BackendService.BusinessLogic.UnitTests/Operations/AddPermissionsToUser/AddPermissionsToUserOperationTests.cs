using BackendService.BusinessLogic.Operations.AddPermissionsToUser;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Models;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions.Models;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.EnsureUserExistsTask;
using DatabaseContext.AuthorizationServiceDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;

namespace BackendService.BusinessLogic.UnitTests.Operations.AddPermissionsToUser;

[TestClass]
public sealed class AddPermissionsToUserOperationTests
{
    private AuthorizationServiceDbContext _context;
    private Mock<IEnsureUserExistsTask> _ensureUserExistsTask;
    private Mock<IAddUserPermissionsTask> _addUserPermissionsTask;
    private Mock<ILogger<AddPermissionsToUserOperation>> _logger;

    private IAddPermissionsToUserOperation _operation = default!;

    public AddPermissionsToUserOperationTests()
    {
        _context = _context = new AuthorizationServiceDbContext(new DbContextOptionsBuilder<AuthorizationServiceDbContext>()
            .UseInMemoryDatabase("AuthorizationServiceDb")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);

        _ensureUserExistsTask = new Mock<IEnsureUserExistsTask>();
        _addUserPermissionsTask = new Mock<IAddUserPermissionsTask>();
        _logger = new Mock<ILogger<AddPermissionsToUserOperation>>();

        _operation = new AddPermissionsToUserOperation(_context, _ensureUserExistsTask.Object, _addUserPermissionsTask.Object, _logger.Object);
    }

    [TestMethod]
    public async Task AddAsync_WhenSuccessful_BeginsTransactionAddsPermissionsAndCommits()
    {
        const string login = "test.user";

        var roleInfos = new RoleInfo[]
        {
            new("TestRole1", "TestTestrictionTypeCode1", "TestRestrictionValue1"),
            new("TestRole2", "TestTestrictionTypeCode2", "TestRestrictionValue2")
        };

        await _operation.AddAsync(new AddPermissionsOperationRequest(login, roleInfos));

        _ensureUserExistsTask.Verify(x => x.ExecuteAsync(login), Times.Once);
        _addUserPermissionsTask.Verify(x => x.AddAsync(It.IsAny<AddUserPermissionsTaskRequest>()), Times.Once);
    }
}