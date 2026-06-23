namespace inventarySystem_backend.domain.Enums;
public enum TransactionType
{
    StockIn = 1, // Entrada de mercancía (Compra, ajuste positivo)
    StockOut = 2 // Salída de mercancía (Venta, merma, ajuste negativo)
}
