using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalClientesApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarMensajeConfirmacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MensajeConfirmacion",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MensajeConfirmacion",
                table: "Pedidos");
        }
    }
}
