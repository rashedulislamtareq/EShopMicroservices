using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BuildingBlocks.Logging;

public static class LoggingExtensions
{
    /// <summary>
    /// Registers Serilog (Elasticsearch sink) and the Elastic APM agent with all auto-instrumentations
    /// (ASP.NET Core, HttpClient, gRPC, EF Core, SqlClient, etc.) for distributed tracing.
    /// APM settings are read from the "ElasticApm" configuration section.
    /// </summary>
    public static WebApplicationBuilder AddElasticLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog(SeriLogger.Configure);
        builder.Services.AddAllElasticApm();

        return builder;
    }
}
