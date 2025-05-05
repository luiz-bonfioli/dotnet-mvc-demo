namespace Demo.Infra.Extensions;

public static class EndpointExtensions
{
    public static void ConfigureEndpoints(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapControllers();
    }
}
