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
using ProjectManagement.API.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ProjectManagementContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ProjectManagement"), b => b.MigrationsAssembly("ProjectManagement.EF"))
.EnableSensitiveDataLogging()
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
    contextLifetime: ServiceLifetime.Scoped
);

builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ProjectManagementContext>()
                .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddCustomPolicies();
});
builder.Services.AddTransient<IAuthService, ProjectManagement.API.Services.AuthService>();

//Configure DI stuff for mapper, repos, and services.
builder.Services.ConfigureAndAddMapper();
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerGen(config => {
    config.SwaggerDoc("v1", new OpenApiInfo() { Title = "WebAPI", Version = "v1" });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter your JWT token in this field",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    config.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };

    config.AddSecurityRequirement(securityRequirement);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("/api")
    .ApplyCustomEndpointConfiguration();
    //.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
