using System.Reflection;
using AppCore.Configs;
using AppCore.Extensions;
using MainData;
using MainData.Middlewares;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    //Setting the status of deployment
});
//
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    var connectString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseMySql(connectString, ServerVersion.AutoDetect(connectString), b =>
    {
    });
});

//
builder.Services.AddControllers();
builder.Services.AddScoped<MainUnitOfWork>();
builder.Services.AddConfig(new List<string>
{
    "AppCore",
    "MainData",
    Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty
}, new List<string>
{
    nameof(IApiVersionDescriptionProvider),
    "UnitOfWork",
    "IUnitOfWork"
});

//

var app = builder.Build();
app.UseConfig();
//
app.MapControllers();
app.UseMiddleware<AuthMiddleware>();
app.Run();