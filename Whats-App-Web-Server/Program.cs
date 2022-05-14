using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Whats_App_Web_Server.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Whats_App_Web_ServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Whats_App_Web_ServerContext") ?? throw new InvalidOperationException("Connection string 'Whats_App_Web_ServerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Rates}/{action=Index}/{id?}");

app.Run();
