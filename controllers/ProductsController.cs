using inventarySystem_backend.application.DTOs;
using inventarySystem_backend.application.Interfaces;
using inventarySystem_backend.application.Services;
using inventarySystem_backend.infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace inventarySystem_backend.controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if(product == null)
        {
            return NotFound(new { message = $"Produto con ID {id} no encontrado"});
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var createdProduct = await _productService.CreateAsync(dto);
        if (createdProduct == null)
        {
            // Si retorna null es porque nuestra regla de negocio detectó que la CategoryId no existe
            return BadRequest(new { message = $"La categoría con ID {dto.CategoryId} especificada no existe." });
        }
        
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateProductDto dto)
    {
        var updated = await _productService.UpdateAsync(id, dto);
        if (!updated)
        {
            return NotFound(new { message = $"Imposible actualizar. Producto con ID {id} no existe."});
        }
        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Imposible eliminar. Producto con ID {id} no existe."});
        }
        return NoContent();    
    }

}
