using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PortalClientesApi.Data;
using PortalClientesApi.Models;
using PortalClientesApi.Services;

public static class PedidoEndpoints
{
    public static void MapPedidoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Pedido").WithTags(nameof(Pedido));

        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.Pedidos.ToListAsync();
        })
        .WithName("GetAllPedidos");

        group.MapGet("/{id}", async Task<Results<Ok<Pedido>, NotFound>> (int id, AppDbContext db) =>
        {
            return await db.Pedidos.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Pedido model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPedidoById");

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Pedido pedido, AppDbContext db) =>
        {
            var affected = await db.Pedidos
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.Id, pedido.Id)
                .SetProperty(m => m.Fecha, pedido.Fecha)
                .SetProperty(m => m.Total, pedido.Total)
                .SetProperty(m => m.Estado, pedido.Estado)
                .SetProperty(m => m.ClienteId, pedido.ClienteId)
                .SetProperty(m => m.Cliente, pedido.Cliente)
        );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePedido");

        group.MapPost("/", async (Pedido pedido, AppDbContext db, GroqService groqService) =>
        {
            var cliente = await db.Clientes.FindAsync(pedido.ClienteId);

            if (cliente != null)
            {
                try
                {
                    pedido.MensajeConfirmacion = await groqService.GenerarMensajeConfirmacionAsync(
                        cliente.Nombre, pedido.Total);

                    var (nota, prioridad) = await groqService.AnalizarPedidoAsync(
                        cliente.Nombre, pedido.Total);

                    pedido.NotaIA = nota;
                    pedido.PrioridadIA = prioridad;
                }
                catch (Exception ex)
                {
                    pedido.MensajeConfirmacion = $"ERROR DEBUG: {ex.Message}";
                    pedido.NotaIA = "No se pudo generar (error de IA).";
                    pedido.PrioridadIA = "Normal";
                }
            }

            db.Pedidos.Add(pedido);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Pedido/{pedido.Id}", pedido);
        })
        .WithName("CreatePedido");

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, AppDbContext db) =>
        {
            var affected = await db.Pedidos
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePedido");
    }
}