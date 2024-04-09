using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using UserManagement.WebAPI.Controllers;
using UserManagement.Services.Interfaces;
using UserManagement.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockService;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mockService = new Mock<IUserService>();
        _controller = new UserController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllUsers_ReturnsOkObjectResult_WithAListOfUsers()
    {
        // Arrange
        var users = new List<User> { new User { UserId = 1, Name = "John Doe" } };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(users);

        // Act
        var result = await _controller.GetAllUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnUsers = Assert.IsType<List<User>>(okResult.Value);
        Assert.Single(returnUsers);
        Assert.Equal("John Doe", returnUsers[0].Name);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((User)null);

        // Act
        var result = await _controller.GetUser(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetUser_ReturnsOkObjectResult_WithUser_WhenUserExists()
    {
        // Arrange
        var user = new User { UserId = 1, Name = "John Doe" };
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(user);

        // Act
        var result = await _controller.GetUser(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUser = Assert.IsType<User>(okResult.Value);
        Assert.Equal("John Doe", returnedUser.Name);
    }

    [Fact]
    public async Task CreateUser_ReturnsCreatedAtAction_WithUser()
    {
        // Arrange
        var user = new User { Name = "Jane Doe" };
        _mockService.Setup(s => s.InsertAsync(user)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.CreateUser(user);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedUser = Assert.IsType<User>(createdAtActionResult.Value);
        Assert.Equal("Jane Doe", returnedUser.Name);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var user = new User { UserId = 1, Name = "Updated Name" };
        _mockService.Setup(s => s.UpdateAsync(user)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateUser(user.UserId, user);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
    [Fact]
    public async Task GetUserCount_ReturnsOkObjectResult_WithTotalUserCount()
    {
        // Arrange
        var userCount = 5;
        _mockService.Setup(s => s.GetUserCountAsync()).ReturnsAsync(userCount);

        // Act
        var result = await _controller.GetUserCount();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(userCount, okResult.Value);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNoContent_WhenDeleteIsSuccessful()
    {
        // Arrange
        var user = new User { UserId = 1, Name = "John Doe" };
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(user);
        _mockService.Setup(s => s.DeleteAsync(user)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteUser(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

}
