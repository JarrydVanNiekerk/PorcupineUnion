
using Microsoft.EntityFrameworkCore;
using System;
using UserManagement.Core.Data;
using UserManagement.Core.Models;
using UserManagement.Repository.Implementations;
using UserManagement.Repository.Interfaces;
using UserManagement.Services.Implementations;
using UserManagement.Services.Interfaces;


public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // container services

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IGroupService, GroupService>();
        builder.Services.AddScoped<IPermissionService, PermissionService>();
        builder.Services.AddScoped<IUserGroupService, UserGroupService>();
        builder.Services.AddScoped<IGroupPermissionService, GroupPermissionService>();



        // Dependency Injection of PorcuoineDb
        builder.Services.AddDbContext<PorcupineDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // My request pipeline http
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}