using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using IdentityServer4.Models;
using IdentityService.Domain;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов для Identity
builder.Services.AddIdentityServer()
    .AddInMemoryClients(Clients.GetClients())
    .AddInMemoryApiScopes(Scopes.GetApiScopes())
    .AddTestUsers(Users.Get())
    .AddDeveloperSigningCredential();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Добавление IdentityServer
app.Map("/identity", identityApp =>
{
    identityApp.UseIdentityServer();
});

app.Run();
