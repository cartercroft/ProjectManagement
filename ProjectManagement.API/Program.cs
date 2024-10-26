using Microsoft.EntityFrameworkCore;
using ProjectManagement.API;
using ProjectManagement.Models;
using ProjectManagement.EF;
using Microsoft.OpenApi.Models;
using static ProjectManagement.API.RouteGroupBuilderExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Classes;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProjectManagementContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ProjectManagement"), b => b.MigrationsAssembly("ProjectManagement.Repositories"))
    .EnableSensitiveDataLogging()
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
    contextLifetime: ServiceLifetime.Scoped
);

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Keys.PrivateKey)),
            ValidateIssuer = true,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization(opt =>
{
    opt.AddCustomPolicies();
});

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ProjectManagementContext>();

//Configure DI stuff for mapper, repos, and services.
builder.Services.ConfigureAndAddMapper();
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerGen(config => {
    config.SwaggerDoc("v1", new OpenApiInfo() { Title = "WebAPI", Version = "v1" });
    config.InferSecuritySchemes();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("/api")
    .ApplyCustomEndpointConfiguration()
    .MapIdentityApi<User>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
