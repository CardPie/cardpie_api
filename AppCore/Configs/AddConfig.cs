using System.Net.Mime;
using System.Text;
using System.Text.Json.Serialization;
using AppCore.Extensions;
using AppCore.Middlewares;
using AppCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AppCore.Configs;

public static class AddConfigServiceCollectionExtensions
{
    private const string MyAllowAllOrigins = "_myAllowAllOrigins";

    public static void AddConfig(this IServiceCollection services, List<string> projectRegis,
        List<string> ignoreServices)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(MyAllowAllOrigins, policyBuilder =>
            {
                policyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("location", "Content-Disposition", "Link", "X-Total-Count", "X-Limit");
            });
        });
        services.AddConfigSwagger();
        services.AddHttpContextAccessor();

        // Service regis service
        services.RegisAllService(projectRegis.ToArray(), ignoreServices.ToArray());
        
        // Add JWT authentication
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                // Configure JWT authentication options
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = EnvironmentExtension.GetJwtIssuer(),
                    ValidAudience = EnvironmentExtension.GetJwtAudience(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentExtension.GetJwtAccessTokenSecret()))
                };
            })
            .AddGoogle(options =>
            {
                // Configure Google authentication options
                options.ClientId = EnvironmentExtension.GetGoogleClientId();
                options.ClientSecret = EnvironmentExtension.GetGoogleClientSecret();
            });
        
        // Service Other
        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            options.ValueProviderFactories.Add(new SnakeCaseQueryValueProviderFactory());
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var result = new ValidationFailedResult(context.ModelState);
                result.ContentTypes.Add(MediaTypeNames.Application.Json);
                return result;
            };
        }).AddJsonOptions(option =>
        {
            option.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
            option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            option.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        services.AddLogging(EnvironmentExtension.GetAppLogFolder());
    }

    public static void UseConfig(this IApplicationBuilder app)
    {
        app.Use((context, next) =>
        {
            context.Request.PathBase = EnvironmentExtension.GetPath();
            return next();
        });
        app.UseCors(MyAllowAllOrigins);
        app.UseConfigSwagger();
        app.UseAuthentication();
        app.UseMiddleware<HandleResponseMiddleware>();
    }
}