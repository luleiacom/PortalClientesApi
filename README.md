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

## Funcionalidades adicionales

- **Endpoint OData** (`/odata/ClientesOData`) para consultas avanzadas con filtros, ordenamiento y selección de campos
- **Portal web** conectado a la API en tiempo real — repositorio separado: [portal-clientes-frontend](https://github.com/luleiacom/portal-clientes-frontend)
- **Automatización asistida por IA**: al crear un pedido, se genera automáticamente un mensaje de confirmación personalizado usando un modelo de lenguaje (Groq API)
