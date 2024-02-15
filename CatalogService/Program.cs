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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Internal", options =>
    {
        options.Authority = builder.Configuration["JwtIssuerOptions:Authority"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    })
    .AddJwtBearer("Site", options =>
    {
        options.Authority = builder.Configuration["JwtIssuerOptions:Authority"];
        options.Audience = builder.Configuration["JwtIssuerOptions:Audience"];
        options.RequireHttpsMetadata = false;
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllowEndUser", policy =>
    {
        policy.AuthenticationSchemes.Add("Site");
        policy.RequireClaim(JwtRegisteredClaimNames.Sub);
    });
    options.AddPolicy("AllowClient", policy =>
    {
        policy.AuthenticationSchemes.Add("Internal");
        policy.Requirements.Add(new DenyAnonymousAuthorizationRequirement());
    });
});
//// JWT Token OpenID Authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.Authority = builder.Configuration["JwtIssuerOptions:Authority"];
//    options.RequireHttpsMetadata = false;// Get authority from appsettings.json
//    options.Audience = builder.Configuration["JwtIssuerOptions:Audience"]; // Get audience from appsettings.json
//});

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
