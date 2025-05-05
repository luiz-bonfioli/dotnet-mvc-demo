using Demo.Domains.Products.Models;

namespace Demo.Domains.Products.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
    IEnumerable<Product> GetAllByPrice(double minPrice, double maxPrice);
    void Add(Product product);
}
