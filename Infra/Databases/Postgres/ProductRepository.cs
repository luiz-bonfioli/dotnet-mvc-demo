using Demo.Domains.Products.Models;
using Demo.Domains.Products.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Demo.Infra.Databases.Postgres;

public class ProductRepository(ISessionFactory sessionFactory) : IProductRepository
{
    private readonly ISessionFactory _sessionFactory = sessionFactory;

    public async Task<IEnumerable<Product>> GetAll()
    {
        using var session = _sessionFactory.OpenSession();
        return await session.Query<Product>().ToListAsync();
    }

    public async Task<Product?> GetById(int id)
    {
        using var session = _sessionFactory.OpenSession();
        return await session.GetAsync<Product>(id);
    }

    public async Task<IEnumerable<Product>> GetAllByPrice(double minPrice, double maxPrice)
    {
        using var session = _sessionFactory.OpenSession();
        return await session.Query<Product>()
            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            .ToListAsync();
    }

    public async Task Add(Product product)
    {
        using var session = _sessionFactory.OpenSession();
        using var transaction = session.BeginTransaction();
        await session.SaveAsync(product);
        await transaction.CommitAsync();
    }

    public async Task Update(Product product)
    {
        using var session = _sessionFactory.OpenSession();
        using var transaction = session.BeginTransaction();
        await session.UpdateAsync(product);
        await transaction.CommitAsync();
    }

    public async Task Delete(Product product)
    {
        using var session = _sessionFactory.OpenSession();
        using var transaction = session.BeginTransaction();
        await session.DeleteAsync(product);
        await transaction.CommitAsync();
    }
}
