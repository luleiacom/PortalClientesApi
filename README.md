# PortalClientesApi

API REST desarrollada en **.NET 10** con **Entity Framework Core** y **SQL Server**, para la gestión de clientes y pedidos. Proyecto de portfolio orientado a roles de desarrollo backend con integración a plataformas CRM (Creatio/Freedom UI).

## Stack técnico

- C# / .NET 10
- ASP.NET Core Web API (Minimal APIs)
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

## Funcionalidades

- CRUD completo de **Clientes** (`/api/Cliente`)
- CRUD completo de **Pedidos** (`/api/Pedido`)
- Relación uno a muchos entre Cliente y Pedido
- Documentación interactiva vía Swagger UI

## Cómo correrlo localmente

1. Cloná el repositorio
2. Configurá tu cadena de conexión en `appsettings.json`
3. Corré las migraciones: `Update-Database`
4. Ejecutá el proyecto (`F5` en Visual Studio, o `dotnet run`)
5. Accedé a `https://localhost:<puerto>/swagger`

## Próximas mejoras

- Endpoint OData para consultas avanzadas
- Portal web conectado a la API
- Automatización asistida por IA
