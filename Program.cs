using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Module32.Controllers;
using Module32.Middlewares;
using Module32.Models.Db;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BlogContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);
builder.Services.AddTransient<IBlogRepository, BlogRepository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseMiddleware<LoggingMiddleware>();
var us = new User()
{
    Id = Guid.NewGuid(),
    FirstName = string.Empty,
    LastName = string.Empty,
    JoinDate= DateTime.Now

};

app.MapGet("/db", (BlogContext rep) => rep.Users.ToList());
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Index}/{id?}");*/
app.Run();