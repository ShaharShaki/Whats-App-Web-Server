using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Whats_App_Web_Server.Data;
using Whats_App_Web_Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Whats_App_Web_ServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Whats_App_Web_ServerContext") ?? throw new InvalidOperationException("Connection string 'Whats_App_Web_ServerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow All",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseCors("Allow All");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Rates}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<HubMessage>("/hubMessage");
});

app.Run();
