using Demo.Domains.Products.Models;

namespace Demo.Domains.Products.Services;

public interface IProductService
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
    IEnumerable<Product> GetAllByPrice(double minPrice, double maxPrice);
    void Add(Product product);
}
