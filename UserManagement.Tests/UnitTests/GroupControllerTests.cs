using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.Interfaces;
using UserManagement.Core.Models;
using UserManagement.WebAPI.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GroupControllerTests
{
    private readonly Mock<IGroupService> _mockService;
    private readonly GroupController _controller;

    public GroupControllerTests()
    {
        _mockService = new Mock<IGroupService>();
        _controller = new GroupController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllGroups_ReturnsOkObjectResult_WithAListOfGroups()
    {
        // Arrange
        var groups = new List<Group> { new Group { GroupId = 1, Name = "Admins" } };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(groups);

        // Act
        var result = await _controller.GetAllGroups();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnGroups = Assert.IsType<List<Group>>(okResult.Value);
        Assert.Single(returnGroups);
        Assert.Equal("Admins", returnGroups[0].Name);
    }

    [Fact]
    public async Task GetGroup_ReturnsNotFound_WhenGroupDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((Group)null);

        // Act
        var result = await _controller.GetGroup(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateGroup_ReturnsCreatedAtActionResult_WithGroup()
    {
        // Arrange
        var group = new Group { Name = "New Group" };
        _mockService.Setup(s => s.InsertAsync(group)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.CreateGroup(group);

        // Assert
        var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedGroup = Assert.IsType<Group>(createdAtResult.Value);
        Assert.Equal("New Group", returnedGroup.Name);
    }
}
