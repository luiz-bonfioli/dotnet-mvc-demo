using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Demo.Infra.Extensions;

public static class TelemetryExtensions
{
    public static void ConfigureOpenTelemetry(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(r => r.AddService("MyDotNetService"))
            .WithTracing(t => t
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter())
            .WithMetrics(m => m
                .AddAspNetCoreInstrumentation()
                .AddPrometheusExporter());
    }
}
