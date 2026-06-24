using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using inventarySystem_backend.application.DTOs;
using inventarySystem_backend.application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace inventarySystem_backend.controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if(category == null)
        {
            return NotFound(new { message = $"Categoria con ID {id} no encontrada."});
        }
        return Ok(category);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateCategoryDto dto)
    {
        var updated = await _categoryService.UpdateAsync(id, dto);
        if (!updated)
        {
            return NotFound(new { message = $"Imposible actualizar. Categoría con ID {id} no existe."});
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _categoryService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Imposible eliminar. Categoría con ID {id} no existe."});
        }
        return NoContent();    
    }
}
