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
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _service.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await _service.GetById(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto dto)
    {
        var product = new Product { Name = dto.Name, Price = dto.Price };
        await _service.Add(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpGet("by-price")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllByPrice([FromQuery] ProductPriceQueryParams query)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var products = await _service.GetAllByPrice(query.MinPrice ?? 0, query.MaxPrice ?? double.MaxValue);
        return Ok(products);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Product product)
    {
        var success = await _service.Update(product);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] Product product)
    {
        var success = await _service.Delete(product);
        if (!success) return NotFound();

        return NoContent();
    }
}
