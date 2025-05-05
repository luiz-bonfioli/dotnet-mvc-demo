using Demo.Domains.Products.Models;
using Demo.Domains.Products.Repositories;

namespace Demo.Domains.Products.Services;

public class ProductService(IProductRepository repository, ILogger<ProductService> logger) : IProductService
{
    private readonly IProductRepository _repository = repository;
    private readonly ILogger<ProductService> _logger = logger;

    public async Task<IEnumerable<Product>> GetAll()
    {
        var products = (await _repository.GetAll()).ToList();
        _logger.LogInformation("Retrieved {Count} products from database.", products.Count);
        return products;
    }

    public async Task<Product?> GetById(int id)
    {
        var product = await _repository.GetById(id);
        if (product == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found.", id);
        }
        else
        {
            _logger.LogInformation("Product with ID {ProductId} retrieved from database.", id);
        }

        return product;
    }

    public async Task<IEnumerable<Product>> GetAllByPrice(double minPrice, double maxPrice)
    {
        var products = (await _repository.GetAllByPrice(minPrice, maxPrice)).ToList();
        _logger.LogInformation("Retrieved {Count} products with price between {MinPrice} and {MaxPrice}.", products.Count, minPrice, maxPrice);
        return products;
    }

    public async Task Add(Product product)
    {
        await _repository.Add(product);
        _logger.LogInformation("Added new product to database: {ProductName}", product.Name);
    }

    public async Task<bool> Update(Product product)
    {
        var existingProduct = await _repository.GetById(product.Id);
        if (existingProduct == null) return false;

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;

        await _repository.Update(existingProduct);
        _logger.LogInformation("Updated product with ID {ProductId}.", product.Id);
        return true;
    }

    public async Task<bool> Delete(Product product)
    {
        var existingProduct = await _repository.GetById(product.Id);
        if (existingProduct == null) return false;

        await _repository.Delete(existingProduct);
        _logger.LogInformation("Deleted product with ID {ProductId}.", product.Id);
        return true;
    }
}
