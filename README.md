# Inventory System Backend 🚀

![.NET 10](https://img.shields.io/badge/.NET-10.0-blueviolet?style=for-the-badge&logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL_Server-Docker-red?style=for-the-badge&logo=microsoft-sql-server)
![Architecture](https://img.shields.io/badge/Architecture-Clean_Architecture-green?style=for-the-badge)

Un backend robusto, escalable y seguro para la gestión de inventarios y control de stock, desarrollado con **.NET 10 (LTS)** utilizando **C#** y las mejores prácticas de la industria de software.

---

## 🏛️ Arquitectura y Patrones de Diseño

El proyecto está diseñado bajo los principios de **Clean Architecture** (Separación Lógica por Carpetas) y **SOLID**, garantizando un bajo acoplamiento y una alta mantenibilidad.

### Estructura del Proyecto
```text
inventarySystem-backend/
├── Controllers/       # Capa de Presentación: Controladores REST y Endpoints
├── Domain/            # Núcleo del Negocio: Entidades puras, Enums e Interfaces de Repositorio
├── Application/       # Lógica de Aplicación: Servicios, DTOs (Records) y Mapeos
├── Infrastructure/    # Detalles Técnicos: EF Core, AppDbContext, Migraciones y Repositorios
├── Middleware/        # Filtros globales y manejo de excepciones
└── Program.cs         # Inicializador y Contenedor de Inyección de Dependencias