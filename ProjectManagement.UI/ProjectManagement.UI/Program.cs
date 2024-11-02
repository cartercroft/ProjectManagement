using ProjectManagement.UI;
using Microsoft.AspNetCore.Components.Authorization;
using ProjectManagement.Classes;
using Blazored.Toast;
using ProjectManagement.Models;
using LayerAbstractions.Interfaces;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
.AddInteractiveServerComponents();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider,
    CustomAuthenticationStateProvider>();
builder.Services.AddBlazoredToast();

builder.AddConfiguredHttpClients();
builder.Services.AddProjectManagementClients();
builder.Services.AddAuthentication();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

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
    .AddInteractiveServerRenderMode()
    .AllowAnonymous();

app.Run();
