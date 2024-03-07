using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductsCatalog.Client.Constants;
using ProductsCatalog.Client.Data;
using ProductsCatalog.Client.Models;

namespace ProductsCatalog.Client.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = Roles.Admin)]
    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize(Roles = Roles.Admin)]
    public IActionResult ListOfUsers()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }

    public async Task<IActionResult> AddNewUser()
    {
        return RedirectToAction(nameof(ListOfUsers));
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> ChangePassword([Bind("Id")] IdentityUser identityUser)
    {
        var user = await _userManager.FindByIdAsync(identityUser.Id);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(user, token, "newQwaszx@1");

        return RedirectToAction(nameof(ListOfUsers));
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> BlockUser([Bind("Id")] IdentityUser identityUser)
    {
        var user = await _userManager.FindByIdAsync(identityUser.Id);

        user.LockoutEnabled = true;
        user.LockoutEnd = DateTimeOffset.MaxValue;

        _userManager.UpdateAsync(user);

        _context.SaveEntitiesChanges();

        return RedirectToAction(nameof(ListOfUsers));
    }

    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeleteUser([Bind("Id")] IdentityUser identityUser)
    {
        var user = await _userManager.FindByIdAsync(identityUser.Id);
        _userManager.DeleteAsync(user);

        // _context.SaveEntitiesChanges();

        return RedirectToAction(nameof(ListOfUsers));;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}