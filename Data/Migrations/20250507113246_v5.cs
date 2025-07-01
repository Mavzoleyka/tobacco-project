using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CigarettesPhotos_СigarettesProducts_СigarettesProductId",
                table: "CigarettesPhotos");

            migrationBuilder.DropIndex(
                name: "IX_CigarettesPhotos_СigarettesProductId",
                table: "CigarettesPhotos");

            migrationBuilder.DropColumn(
                name: "СigarettesProductId",
                table: "CigarettesPhotos");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CigarettesPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CigarettesPhotos_ProductId",
                table: "CigarettesPhotos",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CigarettesPhotos_СigarettesProducts_ProductId",
                table: "CigarettesPhotos",
                column: "ProductId",
                principalTable: "СigarettesProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CigarettesPhotos_СigarettesProducts_ProductId",
                table: "CigarettesPhotos");

            migrationBuilder.DropIndex(
                name: "IX_CigarettesPhotos_ProductId",
                table: "CigarettesPhotos");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CigarettesPhotos");

            migrationBuilder.AddColumn<int>(
                name: "СigarettesProductId",
                table: "CigarettesPhotos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CigarettesPhotos_СigarettesProductId",
                table: "CigarettesPhotos",
                column: "СigarettesProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CigarettesPhotos_СigarettesProducts_СigarettesProductId",
                table: "CigarettesPhotos",
                column: "СigarettesProductId",
                principalTable: "СigarettesProducts",
                principalColumn: "Id");
        }
    }
}
