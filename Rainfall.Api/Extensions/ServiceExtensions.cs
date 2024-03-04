using Microsoft.Extensions.Options;
using Rainfall.ReportService;
using System.Net.Http.Headers;
using Polly;

namespace Rainfall.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddRainfallReportService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfigurations(configuration);

        services.AddHttpClient<IRainfallReportService, RainfallReportService>()
            .ConfigureHttpClient((provider, client) =>
            {
                var apiSettings = provider.GetRequiredService<RainFallReportClientSettings>();
                client.BaseAddress = new Uri(apiSettings.BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddTransientHttpErrorPolicy(builder => builder.RetryAsync(1));

        return services;
    }

    private static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RainFallReportClientSettings>(configuration.GetSection(nameof(RainFallReportClientSettings)));
        services.AddSingleton<RainFallReportClientSettings>(provider =>
        {
            var settings = provider.GetRequiredService<IOptions<RainFallReportClientSettings>>().Value;
            return settings;
        });
    }
}
