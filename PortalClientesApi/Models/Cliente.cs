namespace PortalClientesApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        // Un cliente puede tener muchos pedidos
        public List<Pedido> Pedidos { get; set; } = new();
    }
}