using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.EnsureUserExistsTask;
using DatabaseContext.AuthorizationServiceDb;
using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;

namespace BackendService.BusinessLogic.UnitTests.Operations.AddPermissionsToUser.Tasks;

[TestClass]
public sealed class EnsureUserExistsTaskTests
{
    private readonly IAuthorizationServiceDbContext _context;
    private readonly Mock<ILogger<EnsureUserExistsTask>> _logger;
    private readonly EnsureUserExistsTask _ensureUserExistsTask;

    public EnsureUserExistsTaskTests()
    {
        _context = new AuthorizationServiceDbContext(new DbContextOptionsBuilder<AuthorizationServiceDbContext>()
            .UseInMemoryDatabase("AuthorizationServiceDbContext")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);

        _logger = new Mock<ILogger<EnsureUserExistsTask>>();

        _ensureUserExistsTask = new EnsureUserExistsTask(_context, _logger.Object);
    }

    [TestMethod]
    public async Task UserNotExists()
    {
        const string login = "TestLogin19";
        
        var expectedUser = await _context.User.FirstOrDefaultAsync(users => users.Login == login);
        Assert.IsNull(expectedUser);

        var userId = await _ensureUserExistsTask.ExecuteAsync(login);

        expectedUser = await _context.User.FirstOrDefaultAsync(users => users.Login == login);
        
        Assert.IsNotNull(expectedUser);
        Assert.AreEqual(userId, expectedUser.UserId);
        Assert.AreEqual(login, expectedUser.Login);
    }

    [TestMethod]
    public async Task UserExists()
    {
        const string login = "TestLogin29";

        await _context.User.AddAsync(new Users { Login = login });
        await _context.SaveChangesAsync(CancellationToken.None);
        
        var expectedUsers = await _context.User.Where(users => users.Login == login).ToArrayAsync();
        Assert.AreEqual(1, expectedUsers.Length);

        var userId = await _ensureUserExistsTask.ExecuteAsync(login);

        expectedUsers = await _context.User.Where(users => users.Login == login).ToArrayAsync();
        Assert.AreEqual(1, expectedUsers.Length);

        var user = expectedUsers.First();
        Assert.AreEqual(userId, user.UserId);
        Assert.AreEqual(login, user.Login);
    }
}