using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Infrastructure;
using XYZEngineeringProject.Application;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false) // change
    .AddEntityFrameworkStores<Context>();

builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => fv.DisableDataAnnotationsValidation = true);

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

// DependencyInjection
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

//builder.Services.AddRazorPages();

//Set Authorize Rule
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
