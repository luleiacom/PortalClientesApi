using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalClientesApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposIAToPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotaIA",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrioridadIA",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotaIA",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "PrioridadIA",
                table: "Pedidos");
        }
    }
}
