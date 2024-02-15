using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVC.Services;
using ShopUI.Models;
using System;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IIdentityParser<ApplicationUser> _identityParser;

    public AccountController(
        ILogger<AccountController> logger,
        IIdentityParser<ApplicationUser> identityParser)
    {
        _logger = logger;
        _identityParser = identityParser;
    }

    // Добавленный метод для вызова аутентификации через OpenIdConnect
    public IActionResult Login(string returnUrl = "/")
    {
        return Challenge(new AuthenticationProperties { RedirectUri = returnUrl });
    }

    public IActionResult SignIn()
    {
        var user = _identityParser.Parse(User);

        _logger.LogInformation($"User {user.Name} authenticated");

        return RedirectToAction(nameof(CatalogController.Index), "Catalog");
    }

    public async Task<IActionResult> Signout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

        var homeUrl = Url.Action(nameof(CatalogController.Index), "Catalog");
        return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme,
            new AuthenticationProperties { RedirectUri = homeUrl });
    }
}

