using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PortalClientesApi.Data;
using PortalClientesApi.Models;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Cliente").WithTags(nameof(Cliente));

        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.Clientes.ToListAsync();
        })
        .WithName("GetAllClientes");

        group.MapGet("/{id}", async Task<Results<Ok<Cliente>, NotFound>> (int id, AppDbContext db) =>
        {
            return await db.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Cliente model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetClienteById");

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Cliente cliente, AppDbContext db) =>
        {
            var affected = await db.Clientes
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.Id, cliente.Id)
                .SetProperty(m => m.Nombre, cliente.Nombre)
                .SetProperty(m => m.Email, cliente.Email)
                .SetProperty(m => m.Telefono, cliente.Telefono)
                .SetProperty(m => m.FechaAlta, cliente.FechaAlta)
                .SetProperty(m => m.Pedidos, cliente.Pedidos)
        );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCliente");

        group.MapPost("/", async (Cliente cliente, AppDbContext db) =>
        {
            db.Clientes.Add(cliente);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Cliente/{cliente.Id}",cliente);
        })
        .WithName("CreateCliente");

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, AppDbContext db) =>
        {
            var affected = await db.Clientes
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCliente");
    }
}
