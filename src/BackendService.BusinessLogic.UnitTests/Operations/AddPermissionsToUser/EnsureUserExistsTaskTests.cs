using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.EnsureUserExistsTask;
using DatabaseContext.AuthorizationServiceDb;
using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;

namespace BackendService.BusinessLogic.UnitTests.Operations.AddPermissionsToUser;

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
        const string login = "TestLogin1";
        
        var expectedUser = await _context.User.FirstOrDefaultAsync(users => users.Login == login);
        Assert.IsNull(expectedUser);

        await _ensureUserExistsTask.ExecuteAsync(login);

        expectedUser = await _context.User.FirstOrDefaultAsync(users => users.Login == login);
        
        Assert.IsNotNull(expectedUser);
    }

    [TestMethod]
    public async Task UserExists()
    {
        const string login = "TestLogin2";

        await _context.User.AddAsync(new Users { Login = login });
        await _context.SaveChangesAsync(CancellationToken.None);
        
        var expectedUser = await _context.User.Where(users => users.Login == login).ToArrayAsync();
        Assert.AreEqual(1, expectedUser.Length);

        await _ensureUserExistsTask.ExecuteAsync(login);

        expectedUser = await _context.User.Where(users => users.Login == login).ToArrayAsync();
        Assert.AreEqual(1, expectedUser.Length);
        
        Assert.IsNotNull(expectedUser);
    }
}