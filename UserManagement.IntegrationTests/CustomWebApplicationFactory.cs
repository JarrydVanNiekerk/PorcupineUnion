using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Core.Data;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(PorcupineDbContext)); 

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<PorcupineDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryTestDB");
            });
        });
    }
}
