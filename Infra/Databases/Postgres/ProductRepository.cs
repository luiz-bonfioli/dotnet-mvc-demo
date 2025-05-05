using Demo.Domains.Products.Models;
using Demo.Domains.Products.Repositories;
using NHibernate;

namespace Demo.Infra.Databases.Postgres;

public class ProductRepository(ISessionFactory sessionFactory) : IProductRepository
{
    private readonly ISessionFactory _sessionFactory = sessionFactory;

    public IEnumerable<Product> GetAll()
    {
        var session = _sessionFactory.OpenSession();
        return session.Query<Product>().ToList();
    }

    public Product? GetById(int id)
    {
        var session = _sessionFactory.OpenSession();
        return session.Get<Product>(id);
    }

    public IEnumerable<Product> GetAllByPrice(double minPrice, double maxPrice)
    {
        var session = _sessionFactory.OpenSession();
        return [.. session.Query<Product>().Where(p => p.Price >= minPrice && p.Price <= maxPrice)];
    }

    public void Add(Product product)
    {
        var session = _sessionFactory.OpenSession();
        var transaction = session.BeginTransaction();
        session.Save(product);
        transaction.Commit();
    }

    public void Update(Product product)
    {
        using var session = _sessionFactory.OpenSession();
        using var transaction = session.BeginTransaction();
        session.Update(product);
        transaction.Commit();
    }

    public void Delete(Product product)
    {
        using var session = _sessionFactory.OpenSession();
        using var transaction = session.BeginTransaction();
        session.Delete(product);
        transaction.Commit();
    }
}
