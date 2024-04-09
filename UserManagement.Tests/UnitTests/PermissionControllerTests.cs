using Microsoft.AspNetCore.Mvc;
using Moq;
using UserManagement.Core.Models;
using UserManagement.Services.Interfaces;
using UserManagement.WebAPI.Controllers;

public class PermissionControllerTests
{
    private readonly Mock<IPermissionService> _mockService;
    private readonly PermissionController _controller;

    public PermissionControllerTests()
    {
        _mockService = new Mock<IPermissionService>();
        _controller = new PermissionController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllPermissions_ReturnsOkObjectResult_WithAListOfPermissions()
    {
        // Arrange
        var permissions = new List<Permission> { new Permission { PermissionId = 1, Name = "Read" } };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(permissions);

        // Act
        var result = await _controller.GetAllPermissions();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnPermissions = Assert.IsType<List<Permission>>(okResult.Value);
        Assert.Single(returnPermissions);
        Assert.Equal("Read", returnPermissions[0].Name);
    }
}
