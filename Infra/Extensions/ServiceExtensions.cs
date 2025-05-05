using Demo.Domains.Products.Services;
using Demo.Infra.Security;
using Microsoft.AspNetCore.Authentication;

namespace Demo.Infra.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");

        builder.Services.AddAuthentication("BasicAuth")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuth", null);

        builder.Services.AddControllers();
        builder.Services.AddNHibernate(connectionString);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();

        // DI
        builder.Services.AddScoped<IProductService, ProductService>();
    }
}
