using Demo.Domains.Products.Models;
using Demo.Domains.Products.Repositories;

namespace Demo.Domains.Products.Services;

public class ProductService(IProductRepository repository, ILogger<ProductService> logger) : IProductService
{
    private readonly IProductRepository _repository = repository;
    private readonly ILogger<ProductService> _logger = logger;

    public IEnumerable<Product> GetAll()
    {
        var products = _repository.GetAll().ToList();
        _logger.LogInformation("Retrieved {Count} products from database.", products.Count);
        return products;
    }

    public Product? GetById(int id)
    {
        var product = _repository.GetById(id);
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

    public IEnumerable<Product> GetAllByPrice(double minPrice, double maxPrice)
        {
            var products = _repository.GetAllByPrice(minPrice, maxPrice).ToList();
            _logger.LogInformation("Retrieved {Count} products with price between {MinPrice} and {MaxPrice}.", products.Count, minPrice, maxPrice);
            return products;
        }

    public void Add(Product product)
    {
        _repository.Add(product);
        _logger.LogInformation("Added new product to database: {ProductName}", product.Name);
    }
}
