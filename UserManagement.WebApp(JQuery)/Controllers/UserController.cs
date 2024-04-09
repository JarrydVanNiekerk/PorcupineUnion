using Microsoft.AspNetCore.Mvc;
using UserManagement.Core.Models;
using System.Net.Http.Json;
public class UserController : Controller
{
    private readonly HttpClient _client;

    public UserController(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("API");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _client.GetAsync("User");
        var users = await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        var content = JsonContent.Create(user);
        var response = await _client.PostAsync("User", content);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");
        return View("Error");
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(User user, [FromForm] int[] groupIds)
    {
        var content = JsonContent.Create(user);
        var response = await _client.PutAsync($"User/{user.UserId}", content);
        if (!response.IsSuccessStatusCode)
            return View("Error");

        // Process new group memberships
        foreach (var groupId in groupIds)
        {
            var userGroup = new UserGroup { UserId = user.UserId, GroupId = groupId };
            var ugContent = JsonContent.Create(userGroup);
            var ugResponse = await _client.PostAsync("UserGroup", ugContent);
            if (!ugResponse.IsSuccessStatusCode)
            {
                // Handle failure
            }
        }

        return RedirectToAction("Index");
    }
    public IActionResult GetUserForm()
    {
        var newUser = new User(); 
        return PartialView("Partials/_UserForm", newUser);
    }

    public async Task<IActionResult> GetUserDetails(int id)
    {
        var response = await _client.GetAsync($"User/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return NotFound("User not found");
        }

        
        var user = await response.Content.ReadFromJsonAsync<User>();
        return PartialView("Partials/_UserForm", user);
    }

    public async Task<IActionResult> DeleteUser(int id)
    {
        var response = await _client.DeleteAsync($"User/{id}");
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");
        return View("Error");
    }
}