namespace Demo.Infra.Extensions;

public static class LoggingExtensions
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
    }
}
