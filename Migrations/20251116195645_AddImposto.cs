using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prototipoGestao.Migrations
{
    /// <inheritdoc />
    public partial class AddImposto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Imposto",
                table: "Despesas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imposto",
                table: "Despesas");
        }
    }
}
