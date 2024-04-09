using Xunit;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using UserManagement.Core.Models;
using Microsoft.AspNetCore.Mvc.Testing;

public class UserControllerIT : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private User testUser;

    public UserControllerIT(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        
        testUser = new User { Name = "Initial User" };
        var content = new StringContent(JsonConvert.SerializeObject(testUser), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/api/User", content);
        if (response.IsSuccessStatusCode)
        {
            testUser = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            
        }
    }

    public async Task DisposeAsync()
    {
        if (testUser?.UserId > 0)
        {
            
            await _client.DeleteAsync($"/api/User/{testUser.UserId}");
        }
    }

    [Fact]
    public async Task CreateUser_ReturnsCreatedAtRoute()
    {
        var newUser = new User { Name = "Test User" };
        var content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/User", content);
        response.EnsureSuccessStatusCode(); 

        var returnedUser = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        Assert.Equal("Test User", returnedUser.Name);
        Assert.True(returnedUser.UserId > 0);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNoContent()
    {
        
        testUser.Name = "Updated Name";
        var content = new StringContent(JsonConvert.SerializeObject(testUser), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"/api/User/{testUser.UserId}", content);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task GetUserById_ReturnsUser()
    {
        var response = await _client.GetAsync($"/api/User/{testUser.UserId}");
        response.EnsureSuccessStatusCode(); 

        var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        Assert.Equal(testUser.UserId, user.UserId);
        Assert.Equal("Initial User", user.Name); 
    }

    [Fact]
    public async Task DeleteUser_ReturnsNoContent()
    {
        var response = await _client.DeleteAsync($"/api/User/{testUser.UserId}");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        
    }

    
    [Fact]
    public async Task GetUserByInvalidId_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/User/9999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
