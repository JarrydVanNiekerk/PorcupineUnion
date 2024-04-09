using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using UserManagement.Core.Models;
using System.Net.Http;
using Azure;

public class GroupController : Controller
{
    private readonly HttpClient _client;

    public GroupController(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("API");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _client.GetAsync("Group");
        var groups = await response.Content.ReadFromJsonAsync<IEnumerable<Group>>();
        return View(groups);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(Group group)
    {
        var content = JsonContent.Create(group);
        var response = await _client.PostAsync("Group", content);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");
        return View("Error");
    }

    public async Task<IActionResult> GetAvailableGroups()
    {
        var response = await _client.GetAsync("Group");
        if (!response.IsSuccessStatusCode)
        {
            // Handle the error appropriately
            return NotFound("Group not found");
        }

        var groups = await response.Content.ReadFromJsonAsync<IEnumerable<Group>>();
        return PartialView("Partials/_AvailableGroups", groups);
    }

    [HttpPost]
    public async Task<IActionResult> EditGroup(Group group, [FromForm] int[] permissionIds)
    {
        // Update the group details first
        var content = JsonContent.Create(group);
        var response = await _client.PutAsync($"Group/{group.GroupId}", content);
        if (!response.IsSuccessStatusCode)
            return View("Error");

        // Process new permissions
        foreach (var permId in permissionIds)
        {
            var groupPermission = new GroupPermission { GroupId = group.GroupId, PermissionId = permId };
            var gpContent = JsonContent.Create(groupPermission);
            var gpResponse = await _client.PostAsync("GroupPermission", gpContent);
            if (!gpResponse.IsSuccessStatusCode)
            {
                // Handle failure
            }
        }

        return RedirectToAction("Index");
    }

    public IActionResult GetGroupForm()
    {
        var newGroup = new Group(); 
        return PartialView("Partials/_GroupForm", newGroup);
    }

    public async Task<IActionResult> GetGroupDetails(int id)
    {
        var response = await _client.GetAsync($"Group/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return NotFound("Group not found");
        }

        var group = await response.Content.ReadFromJsonAsync<Group>();
        return PartialView("Partials/_GroupForm", group);
    }



    public async Task<IActionResult> DeleteGroup(int id)
    {
        var response = await _client.DeleteAsync($"Group/{id}");
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");
        return View("Error");
    }

}
