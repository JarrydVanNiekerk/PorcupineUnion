using Newtonsoft.Json;
using System.Net;
using System.Text;
using UserManagement.Core.Models;

public class PermissionControllerIT : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private Permission testPermission;

    public PermissionControllerIT(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        testPermission = new Permission { Name = "Test Permission" };
        var content = new StringContent(JsonConvert.SerializeObject(testPermission), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/api/Permission", content);
        if (response.IsSuccessStatusCode)
        {
            testPermission = JsonConvert.DeserializeObject<Permission>(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task DisposeAsync()
    {
        if (testPermission?.PermissionId > 0)
        {
            await _client.DeleteAsync($"/api/Permission/{testPermission.PermissionId}");
        }
    }

    [Fact]
    public async Task CreatePermission_ReturnsCreatedAtRoute()
    {
        var newPermission = new Permission { Name = "New Permission" };
        var content = new StringContent(JsonConvert.SerializeObject(newPermission), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/Permission", content);
        response.EnsureSuccessStatusCode();

        var returnedPermission = JsonConvert.DeserializeObject<Permission>(await response.Content.ReadAsStringAsync());
        Assert.Equal("New Permission", returnedPermission.Name);
        Assert.True(returnedPermission.PermissionId > 0);
    }

    [Fact]
    public async Task UpdatePermission_ReturnsNoContent()
    {
        testPermission.Name = "Updated Permission";
        var content = new StringContent(JsonConvert.SerializeObject(testPermission), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"/api/Permission/{testPermission.PermissionId}", content);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task GetPermissionById_ReturnsPermission()
    {
        var response = await _client.GetAsync($"/api/Permission/{testPermission.PermissionId}");
        response.EnsureSuccessStatusCode();

        var permission = JsonConvert.DeserializeObject<Permission>(await response.Content.ReadAsStringAsync());
        Assert.Equal(testPermission.PermissionId, permission.PermissionId);
        Assert.Equal("Test Permission", permission.Name);
    }

    [Fact]
    public async Task DeletePermission_ReturnsNoContent()
    {
        var response = await _client.DeleteAsync($"/api/Permission/{testPermission.PermissionId}");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task GetPermissionByInvalidId_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/Permission/9999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}