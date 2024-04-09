using Microsoft.AspNetCore.Mvc;
using Moq;
using UserManagement.Core.Models;
using UserManagement.Services.Interfaces;
using UserManagement.WebAPI.Controllers;

public class UserGroupControllerTests
{
    private readonly Mock<IUserGroupService> _mockService;
    private readonly UserGroupController _controller;

    public UserGroupControllerTests()
    {
        _mockService = new Mock<IUserGroupService>();
        _controller = new UserGroupController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllUserGroups_ReturnsOkObjectResult_WithAListOfUserGroups()
    {
        // Arrange
        var userGroups = new List<UserGroup> { new UserGroup { UserId = 1, GroupId = 2 } };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(userGroups);

        // Act
        var result = await _controller.GetAllUserGroups();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnUserGroups = Assert.IsType<List<UserGroup>>(okResult.Value);
        Assert.Single(returnUserGroups);
    }
    [Fact]
    public async Task GetUsersPerGroupCount_ReturnsOkObjectResult_WithDictionary()
    {
        // Arrange
        var fakeCounts = new Dictionary<int, int> { { 1, 10 }, { 2, 20 } };
        _mockService.Setup(s => s.GetUsersCountPerGroupAsync()).ReturnsAsync(fakeCounts);

        // Act
        var result = await _controller.GetUsersPerGroupCount();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<Dictionary<int, int>>(okResult.Value);
        Assert.Equal(fakeCounts, returnValue);
    }

    [Fact]
    public async Task GetUsersPerGroupCount_ReturnsInternalServerError_OnException()
    {
        // Arrange
        _mockService.Setup(s => s.GetUsersCountPerGroupAsync()).ThrowsAsync(new System.Exception("Database error"));

        // Act
        var result = await _controller.GetUsersPerGroupCount();

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        Assert.Equal("An error occurred while retrieving the user counts per group. Database error", statusCodeResult.Value);
    }
}
