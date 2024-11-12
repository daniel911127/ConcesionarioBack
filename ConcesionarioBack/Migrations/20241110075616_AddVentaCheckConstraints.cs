using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcesionarioBack.Migrations
{
    /// <inheritdoc />
    public partial class AddVentaCheckConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE Ventas ADD CONSTRAINT CK_Ventas_CarroOrMoto CHECK (CarroId IS NOT NULL OR MotoId IS NOT NULL)");
            migrationBuilder.Sql(
                "ALTER TABLE Ventas ADD CONSTRAINT CK_Ventas_CarroAndMoto CHECK (CarroId IS NULL OR MotoId IS NULL)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE Ventas DROP CONSTRAINT CK_Ventas_CarroOrMoto");
            migrationBuilder.Sql(
                "ALTER TABLE Ventas DROP CONSTRAINT CK_Ventas_CarroAndMoto");

        }
    }
}
