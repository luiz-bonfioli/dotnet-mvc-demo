using Demo.Domains.Products.Models;

namespace Demo.Domains.Products.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task<IEnumerable<Product>> GetAllByPrice(double minPrice, double maxPrice);
    Task Add(Product product);
    Task Update(Product product);
    Task Delete(Product product);
}
