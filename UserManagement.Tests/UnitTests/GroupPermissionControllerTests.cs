using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.Interfaces;
using UserManagement.Core.Models;
using UserManagement.WebAPI.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

public class GroupPermissionControllerTests
{
    private readonly Mock<IGroupPermissionService> _mockService;
    private readonly GroupPermissionController _controller;

    public GroupPermissionControllerTests()
    {
        _mockService = new Mock<IGroupPermissionService>();
        _controller = new GroupPermissionController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllGroupPermissions_ReturnsOkObjectResult_WithAListOfGroupPermissions()
    {
        // Arrange
        var groupPermissions = new List<GroupPermission> {
            new GroupPermission { GroupId = 1, PermissionId = 1 }
        };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(groupPermissions);

        // Act
        var result = await _controller.GetAllGroupPermissions();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPermissions = Assert.IsType<List<GroupPermission>>(okResult.Value);
        Assert.Single(returnedPermissions);
    }

    [Fact]
    public async Task GetGroupPermission_ReturnsOkObjectResult_WithGroupPermission()
    {
        // Arrange
        var groupPermission = new GroupPermission { GroupId = 1, PermissionId = 1 };
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(groupPermission);

        // Act
        var result = await _controller.GetGroupPermission(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPermission = Assert.IsType<GroupPermission>(okResult.Value);
        Assert.Equal(1, returnedPermission.GroupId);
    }

    [Fact]
    public async Task CreateGroupPermission_ReturnsCreatedAtAction_WithGroupPermission()
    {
        // Arrange
        var groupPermission = new GroupPermission { GroupId = 1, PermissionId = 1 };
        _mockService.Setup(s => s.InsertAsync(groupPermission)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.CreateGroupPermission(groupPermission);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetGroupPermission", createdAtActionResult.ActionName);
    }

    [Fact]
    public async Task DeleteGroupPermission_ReturnsNoContent_WhenDeleteIsSuccessful()
    {
        // Arrange
        var groupPermission = new GroupPermission { GroupId = 1, PermissionId = 1 };
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(groupPermission);
        _mockService.Setup(s => s.DeleteAsync(groupPermission)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteGroupPermission(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteGroupPermission_ReturnsNotFound_WhenGroupPermissionDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((GroupPermission)null);

        // Act
        var result = await _controller.DeleteGroupPermission(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
