using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prototipoGestao.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoPersonalizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoPersonalizado",
                table: "Dispositivos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoPersonalizado",
                table: "Dispositivos");
        }
    }
}
