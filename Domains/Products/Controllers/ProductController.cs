using Demo.Domains.Products.Models;
using Demo.Domains.Products.Services;
using Demo.Infra.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Domains.Products.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = "BasicAuth")]
public class ProductsController(IProductService service) : ControllerBase
{
    private readonly IProductService _service = service;

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        var product = _service.GetById(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public IActionResult Create(ProductDto dto)
    {
        var product = new Product { Name = dto.Name, Price = dto.Price };
        _service.Add(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpGet("by-price")]
    public ActionResult<IEnumerable<Product>> GetAllByPrice([FromQuery] ProductPriceQueryParams query)
    {
        // Validate model state (e.g., check if both minPrice and maxPrice are provided correctly)
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Call the service method to get all products by price range
        var products = _service.GetAllByPrice(query.MinPrice ?? 0, query.MaxPrice ?? double.MaxValue);

        return Ok(products);
    }

    [HttpPut]
    public IActionResult Update([FromBody] Product product)
    {
        var success = _service.Update(product);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] Product product)
    {
        var success = _service.Delete(product);
        if (!success) return NotFound();

        return NoContent();
    }

}

