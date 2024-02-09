var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add HTTP clients with configuration from appsettings.json
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
