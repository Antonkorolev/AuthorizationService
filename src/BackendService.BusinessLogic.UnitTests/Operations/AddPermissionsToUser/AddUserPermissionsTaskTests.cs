using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions;
using DatabaseContext.AuthorizationServiceDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;

namespace BackendService.BusinessLogic.UnitTests.Operations.AddPermissionsToUser;

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
        // TODO
    }
    
    [TestMethod]
    public async Task AddPermissionsWithoutRestrictions()
    {
        // TODO
    }
    
    [TestMethod]
    public async Task AddPermissionsWithRestrictions()
    {
        // TODO
    }
}