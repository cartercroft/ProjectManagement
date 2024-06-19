using ProjectManagement.UI;
using Microsoft.AspNetCore.Components.Authorization;
using ProjectManagement.Classes;
using ProjectManagement.UI.Components.Auth;
using Azure.Core.Pipeline;
using System.Net;
using Blazored.Toast;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider,
    CustomAuthenticationStateProvider>();
builder.Services.AddScoped<ILoginManager, CustomAuthenticationStateProvider>();
builder.Services.AddBlazoredToast();

builder.AddConfiguredHttpClients();
builder.Services.AddProjectManagementClients();
builder.Services.AddAuthentication()
    .AddBearerToken();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
