using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalClientesApi.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        public string Estado { get; set; } = "Pendiente";

        // Relación con Cliente
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
