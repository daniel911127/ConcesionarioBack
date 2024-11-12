using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcesionarioBack.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarroId",
                table: "Motos",
                newName: "MotoId");

            migrationBuilder.AlterColumn<string>(
                name: "Velocidades",
                table: "Motos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Cilindraje",
                table: "Motos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MotoId",
                table: "Motos",
                newName: "CarroId");

            migrationBuilder.AlterColumn<string>(
                name: "Velocidades",
                table: "Motos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Cilindraje",
                table: "Motos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
