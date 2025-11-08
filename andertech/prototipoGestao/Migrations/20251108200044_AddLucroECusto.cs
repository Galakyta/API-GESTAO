using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prototipoGestao.Migrations
{
    /// <inheritdoc />
    public partial class AddLucroECusto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CustoDeOperacao",
                table: "Dispositivos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Lucro",
                table: "Dispositivos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustoDeOperacao",
                table: "Dispositivos");

            migrationBuilder.DropColumn(
                name: "Lucro",
                table: "Dispositivos");
        }
    }
}
