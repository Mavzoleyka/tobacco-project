using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
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
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
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
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
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
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CigarettesPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CigarettesPhotos_СigarettesProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "СigarettesProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    PriceAtBooking = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationItems_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationItems_СigarettesProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "СigarettesProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CigarettesPhotos_ProductId",
                table: "CigarettesPhotos",
                column: "ProductId");

            

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItems_ProductId",
                table: "ReservationItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItems_ReservationId",
                table: "ReservationItems",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientId",
                table: "Reservations",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CigarettesPhotos");

            

            migrationBuilder.DropTable(
                name: "ReservationItems");

            

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "СigarettesProducts");

           

            migrationBuilder.DropTable(
                name: "CigarettesCategories");

            migrationBuilder.DropTable(
                name: "CigarettesManufacturer");
        }
    }
}
