using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using MVC.Services;
using ShopUI.Models;

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add HTTP clients with configuration from appsettings.json
var identityUrl = configuration.GetValue<string>("IdentityUrl");
var callBackUrl = configuration.GetValue<string>("CallBackUrl");
var redirectUrl = configuration.GetValue<string>("RedirectUri");
var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

builder.Services.AddTransient<IIdentityParser<ApplicationUser>, IdentityParser>();

builder.Services.AddHttpClient("CatalogApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CatalogServiceUrl"]); // Адрес для сервиса Catalog из конфигурации
});

builder.Services.AddHttpClient("BasketApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BasketServiceUrl"]); // Адрес для сервиса Basket из конфигурации
});

builder.Services.AddHttpClient("OrderApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["OrderServiceUrl"]); // Адрес для сервиса Order из конфигурации
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(setup => setup.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
    .AddOpenIdConnect(options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Authority = identityUrl;
        options.Events.OnRedirectToIdentityProvider = async n =>
        {
            n.ProtocolMessage.RedirectUri = redirectUrl;
            await Task.FromResult(0);
        };
        options.SignedOutRedirectUri = callBackUrl;
        options.ClientId = "mvc_pkce";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.RequireHttpsMetadata = false;
        options.UsePkce = true;
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("mvc");
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Add Minimal API controllers
app.MapControllers();

// Add MVC controllers
app.MapControllerRoute(
    name: "catalog",
    pattern: "{controller=Catalog}/{action=Index}/{id?}"); // Изменил паттерн маршрутизации для Catalog

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}