using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CigarettesCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CigarettesCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CigarettesManufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CigarettesManufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "СigarettesProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CategodyId = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_СigarettesProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_СigarettesProducts_CigarettesCategories_CategodyId",
                        column: x => x.CategodyId,
                        principalTable: "CigarettesCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_СigarettesProducts_CigarettesManufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "CigarettesManufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CigarettesPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    СigarettesProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CigarettesPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CigarettesPhotos_СigarettesProducts_СigarettesProductId",
                        column: x => x.СigarettesProductId,
                        principalTable: "СigarettesProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_СigarettesProducts_CategodyId",
                table: "СigarettesProducts",
                column: "CategodyId");

            migrationBuilder.CreateIndex(
                name: "IX_СigarettesProducts_ManufacturerId",
                table: "СigarettesProducts",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_CigarettesPhotos_СigarettesProductId",
                table: "CigarettesPhotos",
                column: "СigarettesProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CigarettesPhotos");

            migrationBuilder.DropTable(
                name: "СigarettesProducts");

            migrationBuilder.DropTable(
                name: "CigarettesCategories");

            migrationBuilder.DropTable(
                name: "CigarettesManufacturer");
        }
    }
}
