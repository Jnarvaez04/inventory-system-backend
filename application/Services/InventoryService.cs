using inventarySystem_backend.application.DTOs;
using inventarySystem_backend.application.Interfaces;
using inventarySystem_backend.domain.Entities;
using inventarySystem_backend.domain.Enums;
using inventarySystem_backend.domain.Interfaces;
using Mapster;

namespace inventarySystem_backend.application.Services;

public class InventoryService : IInventoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public InventoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    //Registrar entrada de stock (stock in)
    public async Task<TransactionResponseDto?> RegisterStockInAsync(CreateTransactionDto dto)
    {
        // Validar que el producto exista realmente
        var product = await _unitOfWork.Products.GetByIdAsync(dto.ProductId);
        if(product == null) return null;

        // Modificar el stock del producto directamente
        product.Stock += dto.Quantity;
        _unitOfWork.Products.Update(product);

        // Se crea el histórico de la transacción
        var transaction = new InventoryTransaction
        {
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            Type = TransactionType.StockIn,
            Reason = dto.Reason ?? "Entrada de stock estándar",
            Date = DateTime.UtcNow,
            UserId = 1
        };

        await _unitOfWork.InventoryTransactions.AddAsync(transaction);

        // Salvado atómico (unit of work)
        // SQL Server guardará la transacción y actualizará el stock en un solo bloque. Si uno falla, todo se cancela.
        await _unitOfWork.SaveChangesAsync();

        // Refrescamos el objeto para que traiga la información del Producto mapeada en el DTO
        var savedTransaction = await _unitOfWork.InventoryTransactions.GetByIdAsync(transaction.Id);
        return savedTransaction.Adapt<TransactionResponseDto>();
    }

    //Registrar la salida de stock (stock out)
    public async Task<TransactionResponseDto?> RegisterStockOutAsync(CreateTransactionDto dto)
    {
        // validar que el producto exista
        var product = await _unitOfWork.Products.GetByIdAsync(dto.ProductId);
        if(product == null) return null;

        // No permitir stock negativo
        if (product.Stock < dto.Quantity)
        {
            throw new InvalidOperationException($"Stock insuficiente. Stock actual: {product.Stock}, solicitado: {dto.Quantity}");
        }

        //Paso 1: restar el Stock del producto
        product.Stock -= dto.Quantity;
        _unitOfWork.Products.Update(product);

        //Paso 2: crear registro historico de la salida
        var transaction = new InventoryTransaction
        {
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            Type = TransactionType.StockOut,
            Reason = dto.Reason ?? "Salida de stock estándar",
            Date = DateTime.UtcNow,
            UserId = 1
        };

        await _unitOfWork.InventoryTransactions.AddAsync(transaction);

        //Paso 3: Confirmación en lote
        await _unitOfWork.SaveChangesAsync();

        var savedTransaction = await _unitOfWork.InventoryTransactions.GetByIdAsync(transaction.Id);
        return savedTransaction.Adapt<TransactionResponseDto>();

    }

    //Consultar el historial o kardex de un producto
    public async Task<IEnumerable<TransactionResponseDto>> GetHistoryByProductIdAsync(int productId)
    {
        var transactions = await _unitOfWork.InventoryTransactions.GetByProductIdAsync(productId);

        return transactions.Adapt<IEnumerable<TransactionResponseDto>>();
    }
}