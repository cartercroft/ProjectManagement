using Microsoft.EntityFrameworkCore;
using ProjectManagement.Repositories;
using ProjectManagement.API;

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

//Configure DI stuff for mapper, repos, and services.
builder.Services.ConfigureAndAddMapper();
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
