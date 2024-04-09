using Microsoft.AspNetCore.Mvc;
using UserManagement.Core.Models;
using System.Net.Http.Json;
using System.Net.Http;
using Azure;

public class PermissionController : Controller
{
    private readonly HttpClient _client;

    public PermissionController(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("API");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _client.GetAsync("Permission");
        var permissions = await response.Content.ReadFromJsonAsync<IEnumerable<Permission>>();
        return View(permissions);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePermission(Permission permission)
    {
        var content = JsonContent.Create(permission);
        var response = await _client.PostAsync("Permission", content);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");
        return View("Error");
    }

    public IActionResult GetPermissionForm()
    {
        var newPermission = new Permission();
        return PartialView("Partials/_PermissionForm", newPermission);
    }

    public async Task<IActionResult> GetPermissionDetails(int id)
    {
        var response = await _client.GetAsync($"Permission/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return NotFound("Permission not found");
        }

        var permission = await response.Content.ReadFromJsonAsync<Permission>();
        return PartialView("Partials/_PermissionForm", permission);
    }

    [HttpPost]
    public async Task<IActionResult> EditPermission(Permission permission)
    {
        var content = JsonContent.Create(permission);
        var response = await _client.PutAsync($"Permission/{permission.PermissionId}", content);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");
        return View("Error");
    }
    public async Task<IActionResult> GetAvailablePermissions()
    {
        var response = await _client.GetAsync("Permission");
        if (!response.IsSuccessStatusCode)
        {
            // Handle the error appropriately
            return NotFound("Permissions not found");
        }

        var permissions = await response.Content.ReadFromJsonAsync<IEnumerable<Permission>>();
        return PartialView("Partials/_AvailablePermissions", permissions);
    }

    public async Task<IActionResult> DeletePermission(int id)
    {
        var response = await _client.DeleteAsync($"Permission/{id}");
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");
        return View("Error");
    }
}
