using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using ProductsCatalog.Client.Constants;
using ProductsCatalog.Client.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddEntityFrameworkMySQL()
    .AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddResponseCaching();

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
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await dbContext.Database.MigrateAsync();

// users and roles seed
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

if (!await roleManager.RoleExistsAsync(Roles.Admin))
{
    await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
}

if (!await roleManager.RoleExistsAsync(Roles.AdvancedUser))
{
    await roleManager.CreateAsync(new IdentityRole(Roles.AdvancedUser));
}

if (!await roleManager.RoleExistsAsync(Roles.User))
{
    await roleManager.CreateAsync(new IdentityRole(Roles.User));
}

var adminUserEmail = "admin@bb.com";
if (!dbContext.Users.Any(_ => _.Email.Equals(adminUserEmail)))
{
    var result =
        await userManager.CreateAsync(new IdentityUser(adminUserEmail) { Email = adminUserEmail },
            "Qwaszx@1");
    if (result.Succeeded)
    {
        var user = dbContext.Users.FirstOrDefault(_ => _.Email.Equals(adminUserEmail));
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        await userManager.ConfirmEmailAsync(user, token);
        await userManager.AddToRoleAsync(user, Roles.Admin);
    }
}

var advancedUserEmail = "adv@bb.com";
if (!dbContext.Users.Any(_ => _.Email.Equals(advancedUserEmail)))
{
    var result =
        await userManager.CreateAsync(new IdentityUser(advancedUserEmail) { Email = advancedUserEmail },
            "Qwaszx@1");
    if (result.Succeeded)
    {
        var user = dbContext.Users.FirstOrDefault(_ => _.Email.Equals(advancedUserEmail));
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        await userManager.ConfirmEmailAsync(user, token);
        await userManager.AddToRoleAsync(user, Roles.AdvancedUser);
    }
}

var userEmail = "user@bb.com";
if (!dbContext.Users.Any(_ => _.Email.Equals(userEmail)))
{
    var result = await userManager.CreateAsync(new IdentityUser(userEmail) { Email = userEmail },
        "Qwaszx@1");
    if (result.Succeeded)
    {
        var user = dbContext.Users.FirstOrDefault(_ => _.Email.Equals(userEmail));
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        await userManager.ConfirmEmailAsync(user, token);
        await userManager.AddToRoleAsync(user, Roles.User);
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();