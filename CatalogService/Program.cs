using CatalogService.Data;
using CatalogService.Repository;
using CatalogService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using CatalogServiceService = CatalogService.Services.CatalogService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration for Entity Framework
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adding services
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<ICatalogService, CatalogServiceService>();

// Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "CatalogServiceInstance";
});

// JWT Token OpenID Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents()
    {
        OnMessageReceived = msg =>
        {
            var token = msg?.Request.Headers.Authorization.ToString();
            string path = msg?.Request.Path ?? "";
            if (!string.IsNullOrEmpty(token))

            {
                Console.WriteLine("Access token");
                Console.WriteLine($"URL: {path}");
                Console.WriteLine($"Token: {token}\r\n");
            }
            else
            {
                Console.WriteLine("Access token");
                Console.WriteLine("URL: " + path);
                Console.WriteLine("Token: No access token provided\r\n");
            }
            return Task.CompletedTask;
        }
    };
    options.Authority = builder.Configuration["JwtIssuerOptions:Authority"];
    options.RequireHttpsMetadata = false;// Get authority from appsettings.json
    options.Audience = builder.Configuration["JwtIssuerOptions:Audience"]; // Get audience from appsettings.json
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CatalogDbContext>();

        // Check if database is empty
        if (!context.CatalogItems.Any())
        {
            var seeder = new CatalogItemSeeder();
            var items = seeder.GenerateCatalogItems(20); // Generate 10 catalog items
            context.CatalogItems.AddRange(items);
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Enable authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
