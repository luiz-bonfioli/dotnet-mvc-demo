using Demo.Domains.Products.Models;

namespace Demo.Domains.Products.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task<IEnumerable<Product>> GetAllByPrice(double minPrice, double maxPrice);
    Task Add(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(Product product);
}
