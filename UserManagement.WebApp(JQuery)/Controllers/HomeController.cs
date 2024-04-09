using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserManagement.WebApp_JQuery_.Models;

namespace UserManagement.WebApp_JQuery_.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("API");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("User");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                
                return View((object)data);
            }
            return View("Error");
        }
    }
}
