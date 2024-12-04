using Microsoft.AspNetCore.Http;

namespace JasperCloud.Tests.ControllerTests;

[TestClass]
public class AccountControllerTest : ControllerBase
{
    private readonly Mock<IUserRepository> _userRepoMock;

    public AccountControllerTest()
    {
        _userRepoMock = new Mock<IUserRepository>();
    }

    [TestMethod]
    public async Task CreateAccountReturnsOk()
    {
        // Arrange
        var newUser = new CreateAccountViewModel {
            Username = "test",
            Email = "test@mail.com",
            Password = "password123"
        };

        _userRepoMock.Setup(u => u.AddAsync(new User()));
        var service = new AccountService(_userRepoMock.Object);
        var loginController = new AccountController(service);

        // Act
        var result = await loginController.Create(newUser);

        // Assert
        Assert.IsTrue(result is StatusCodeResult);
    }

    [TestMethod]
    public async Task UserLoginReturnsNullLoginResponse()
    {
        // Arrange
        var loginRequest = new LoginViewModel {
            Username = "test",
            Password = "password123"
        };

        var user = new User{
            Id = 1,
            Username = "test",
            Email = "test@test.com",
            PasswordHash = "TruGIY218RvRz0a4nsbuHxaus5WH0+JD2rPrU4iuGlc=",
            PasswordSalt = "f9i5iG+KWL9+z5Vh+EpufQ=="
        };

        _userRepoMock.Setup(u => u.GetByUsernameAsync(loginRequest.Username)).ReturnsAsync(user);
        var service = new AccountService(_userRepoMock.Object);
        var loginController = new AccountController(service);

        // Act
        var result = await service.UserLoginAsync(loginRequest);

        // Assert
        Assert.AreEqual(result, null);
    }
}