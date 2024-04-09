using Xunit;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using UserManagement.Core.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;

public class GroupControllerIT : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private Group testGroup;

    public GroupControllerIT(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        testGroup = new Group { Name = "Test Group" };
        var content = new StringContent(JsonConvert.SerializeObject(testGroup), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/api/Group", content);
        if (response.IsSuccessStatusCode)
        {
            testGroup = JsonConvert.DeserializeObject<Group>(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task DisposeAsync()
    {
        if (testGroup?.GroupId > 0)
        {
            await _client.DeleteAsync($"/api/Group/{testGroup.GroupId}");
        }
    }

    [Fact]
    public async Task CreateGroup_ReturnsCreatedAtRoute()
    {
        var newGroup = new Group { Name = "New Group" };
        var content = new StringContent(JsonConvert.SerializeObject(newGroup), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/Group", content);
        response.EnsureSuccessStatusCode();

        var returnedGroup = JsonConvert.DeserializeObject<Group>(await response.Content.ReadAsStringAsync());
        Assert.Equal("New Group", returnedGroup.Name);
        Assert.True(returnedGroup.GroupId > 0);
    }

    [Fact]
    public async Task UpdateGroup_ReturnsNoContent()
    {
        testGroup.Name = "Updated Group";
        var content = new StringContent(JsonConvert.SerializeObject(testGroup), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"/api/Group/{testGroup.GroupId}", content);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task GetGroupById_ReturnsGroup()
    {
        var response = await _client.GetAsync($"/api/Group/{testGroup.GroupId}");
        response.EnsureSuccessStatusCode();

        var group = JsonConvert.DeserializeObject<Group>(await response.Content.ReadAsStringAsync());
        Assert.Equal(testGroup.GroupId, group.GroupId);
        Assert.Equal("Test Group", group.Name);
    }

    [Fact]
    public async Task DeleteGroup_ReturnsNoContent()
    {
        var response = await _client.DeleteAsync($"/api/Group/{testGroup.GroupId}");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task GetGroupByInvalidId_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/Group/9999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}