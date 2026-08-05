using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventarySystem_backend.application.DTOs;
using inventarySystem_backend.application.Interfaces;
using inventarySystem_backend.application.Services;
using Microsoft.AspNetCore.Mvc;

namespace inventarySystem_backend.controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }


    // 1. POST: api/inventory/stock-in
    [HttpPost("stock-in")]
    public async Task<IActionResult> StockIn([FromBody] CreateTransactionDto dto)
    {
        var result = await _inventoryService.RegisterStockInAsync(dto);
        if (result == null)
        {
            return NotFound(new { message = $"Imposible registrar entrada. El producto con ID {dto.ProductId} no existe"});
        }
        return Ok(result);
    }


    // 2. POST: api/inventory/stock-out
    [HttpPost("stock-out")]
    public async Task<IActionResult> StockOut([FromBody] CreateTransactionDto dto)
    {
        try
        {
            var result = await _inventoryService.RegisterStockOutAsync(dto);
            if (result == null)
            {
                return NotFound(new { message = $"Imposible registrar salida. El producto con ID {dto.ProductId} no existe." });
            }
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            // Captura la excepción de stock insuficiente que lanzamos en la regla de negocio
            return BadRequest(new { message = ex.Message });
        }
    }

    // 3. GET: api/inventory/history/{productId}
    [HttpGet("history/{productId:int}")]
    public async Task<IActionResult> GetHistory(int productId)
    {
        var history = await _inventoryService.GetHistoryByProductIdAsync(productId);
        return Ok(history);
    }
}
