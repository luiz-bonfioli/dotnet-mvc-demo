using Demo.Infra.Middlewares;

namespace Demo.Infra.Extensions;

public static class MiddlewareExtensions
{
    public static void ConfigureMiddlewares(this WebApplication app)
    {
        app.UseOpenTelemetryPrometheusScrapingEndpoint();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<HeadersValidationMiddleware>();

        app.UseHttpsRedirection();
    }
}
