using JasperCloud.ResponseModels;

namespace JasperCloud.Tests.ControllerTests;

[TestClass]
public class LoginControllerTest : ControllerBase
{
    private readonly Mock<IUserRepository> _userRepoMock;

    public LoginControllerTest()
    {
        _userRepoMock = new Mock<IUserRepository>();
    }

    [TestMethod]
    public async Task CreateAccountReturnsOk()
    {
        // Arrange
        var userRequest = new UserRequest {
            Username = "test",
            Email = "test@mail.com",
            Password = "password123"
        };

        _userRepoMock.Setup(u => u.AddAsync(new User()));
        var service = new LoginService(_userRepoMock.Object);
        var loginController = new LoginController(service);

        // Act
        var result = await loginController.CreateAccount(userRequest);

        // Assert
        Assert.IsTrue(result is StatusCodeResult);
    }

    [TestMethod]
    public async Task UserLoginReturnsOk()
    {
        // Arrange
        var loginRequest = new LoginRequest {
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

        var loginResponse = new LoginResponse {
            Id = 1,
            Username = "test",
            Email = "test@test.com"
        };

        _userRepoMock.Setup(u => u.GetByUsernameAsync(loginRequest.Username)).ReturnsAsync(user);
        var service = new LoginService(_userRepoMock.Object);
        var loginController = new LoginController(service);

        // Act
        var result = await service.UserLogin(loginRequest);

        // Assert
        Assert.AreEqual(result.ToString(), loginResponse.ToString());
    }
}