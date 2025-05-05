using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Demo.Domains.Products.Repositories;
using Demo.Domains.Products.Models;
using NHibernate.Tool.hbm2ddl;
using Demo.Infra.Databases.Postgres;

namespace Demo.Infra.Extensions;

public static class DatabaseExtensions
{
    public static void AddNHibernate(this IServiceCollection services, string connectionString)
    {
        var config = Fluently.Configure()
            .Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductMap>())
            .BuildConfiguration();

        // Automatically update database schema at startup       
        new SchemaUpdate(config).Execute(false, true);

        // Automatically generate database schema at startup, but drop all schema before
        // new SchemaExport(config).Create(false, true);

        var sessionFactory = config.BuildSessionFactory();

        services.AddSingleton(sessionFactory);
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
