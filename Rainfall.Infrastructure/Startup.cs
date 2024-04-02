using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rainfall.Infrastructure.Middleware;
using Rainfall.Infrastructure.Validations;
using System.Reflection;

namespace Rainfall.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var applicationAssembly = typeof(Core.Startup).GetTypeInfo().Assembly;
        return services
            .AddExceptionMiddleware()
            .AddBehaviours(applicationAssembly);
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config) =>
        builder
            .UseExceptionMiddleware();

    private static IServiceCollection AddExceptionMiddleware(this IServiceCollection services) =>
        services.AddScoped<GlobalExceptionMiddleware>();

    private static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app) =>
        app.UseMiddleware<GlobalExceptionMiddleware>();

    private static IServiceCollection AddBehaviours(this IServiceCollection services, Assembly assemblyContainingValidators)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
