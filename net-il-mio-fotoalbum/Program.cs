using System.Globalization;
using Microsoft.AspNetCore.Identity;
using net_il_mio_fotoalbum.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AlbumContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AlbumContext>();

// Add services to the container.
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Photo}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
using (var ctx = scope.ServiceProvider.GetService<AlbumContext>())
{
    ctx!.Seed();
}

app.Run();
