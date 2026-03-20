using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using VeterinaryClinic.Data;
using VeterinaryClinic.Data.Repositories;
using VeterinaryClinic.Services;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Helpers;
using VeterinaryClinic.Services.Implementations;
using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinic.Tests.Services;

public class UserServiceIntegrationTests : IDisposable
{
    private readonly VetClinicDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserService _userService;

    public UserServiceIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<VetClinicDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new VetClinicDbContext(options);

        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>(), NullLoggerFactory.Instance);
        _mapper = config.CreateMapper();

        var userRepo = new UserRepository(_context);
        _userService = new UserService(userRepo, _mapper);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task RegisterUserAsync_WithValidData_Succeeds()
    {
        // Arrange
        var dto = new RegisterUserDto
        {
            Username = "newowner",
            Password = "Pass123!",
            ConfirmPassword = "Pass123!",
            Email = "newowner@test.bg",
            FirstName = "Нов",
            LastName = "Потребител",
            Role = UserRole.Owner
        };

        // Act
        var (success, message) = await _userService.RegisterUserAsync(dto);

        // Assert
        Assert.True(success);
        Assert.Equal("User registered successfully", message);
    }

    [Fact]
    public async Task RegisterUserAsync_PasswordMismatch_Fails()
    {
        // Arrange
        var dto = new RegisterUserDto
        {
            Username = "user1",
            Password = "Pass123!",
            ConfirmPassword = "DifferentPass!",
            Email = "user1@test.bg",
            FirstName = "Иван",
            LastName = "Иванов",
            Role = UserRole.Owner
        };

        // Act
        var (success, message) = await _userService.RegisterUserAsync(dto);

        // Assert
        Assert.False(success);
        Assert.Equal("Passwords do not match", message);
    }

    [Fact]
    public async Task RegisterUserAsync_DuplicateUsername_Fails()
    {
        // Arrange
        var dto = new RegisterUserDto
        {
            Username = "existinguser",
            Password = "Pass123!",
            ConfirmPassword = "Pass123!",
            Email = "first@test.bg",
            FirstName = "Иван",
            LastName = "Иванов",
            Role = UserRole.Owner
        };
        await _userService.RegisterUserAsync(dto);

        // Try to register same username
        var dto2 = new RegisterUserDto
        {
            Username = "existinguser",
            Password = "Pass456!",
            ConfirmPassword = "Pass456!",
            Email = "second@test.bg",
            FirstName = "Петър",
            LastName = "Петров",
            Role = UserRole.Owner
        };

        // Act
        var (success, message) = await _userService.RegisterUserAsync(dto2);

        // Assert
        Assert.False(success);
        Assert.Equal("Username already exists", message);
    }

    [Fact]
    public async Task ValidateLoginAsync_CorrectCredentials_ReturnsUser()
    {
        // Arrange
        var dto = new RegisterUserDto
        {
            Username = "logintest",
            Password = "Login123!",
            ConfirmPassword = "Login123!",
            Email = "login@test.bg",
            FirstName = "Логин",
            LastName = "Тест",
            Role = UserRole.Owner
        };
        await _userService.RegisterUserAsync(dto);

        // Act
        var result = await _userService.ValidateLoginAsync("logintest", "Login123!");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("logintest", result.Username);
        Assert.Equal(UserRole.Owner, result.Role);
    }

    [Fact]
    public async Task ValidateLoginAsync_WrongPassword_ReturnsNull()
    {
        // Arrange
        var dto = new RegisterUserDto
        {
            Username = "passtest",
            Password = "Correct123!",
            ConfirmPassword = "Correct123!",
            Email = "passtest@test.bg",
            FirstName = "Тест",
            LastName = "Парола",
            Role = UserRole.Owner
        };
        await _userService.RegisterUserAsync(dto);

        // Act
        var result = await _userService.ValidateLoginAsync("passtest", "WrongPassword");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ValidateLoginAsync_NonExistentUser_ReturnsNull()
    {
        // Act
        var result = await _userService.ValidateLoginAsync("nonexistent", "anypassword");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetUserByIdAsync_ExistingUser_ReturnsUserDto()
    {
        // Arrange
        var dto = new RegisterUserDto
        {
            Username = "getbyid",
            Password = "Test123!",
            ConfirmPassword = "Test123!",
            Email = "getbyid@test.bg",
            FirstName = "Тест",
            LastName = "Потребител",
            Role = UserRole.Owner
        };
        await _userService.RegisterUserAsync(dto);
        var user = await _userService.GetUserByUsernameAsync("getbyid");

        // Act
        var result = await _userService.GetUserByIdAsync(user!.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("getbyid", result.Username);
    }

    [Fact]
    public async Task GetUserByIdAsync_NonExistentId_ReturnsNull()
    {
        // Act
        var result = await _userService.GetUserByIdAsync(9999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllUsersAsync_ReturnsAllUsers()
    {
        // Arrange
        await _userService.RegisterUserAsync(new RegisterUserDto { Username = "u1", Password = "P1!", ConfirmPassword = "P1!", Email = "u1@t.bg", FirstName = "A", LastName = "B", Role = UserRole.Owner });
        await _userService.RegisterUserAsync(new RegisterUserDto { Username = "u2", Password = "P1!", ConfirmPassword = "P1!", Email = "u2@t.bg", FirstName = "C", LastName = "D", Role = UserRole.Owner });

        // Act
        var all = await _userService.GetAllUsersAsync();

        // Assert
        Assert.Equal(2, all.Count());
    }
}
