using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMedialityc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Vehicles");

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleInventoryId",
                table: "Vehicles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "VehicleInventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "integer", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false, computedColumnSql: "CASE WHEN \"AvailableQuantity\" > 0 THEN true ELSE false END", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleInventories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleInventoryId",
                table: "Vehicles",
                column: "VehicleInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleInventories_VehicleInventoryId",
                table: "Vehicles",
                column: "VehicleInventoryId",
                principalTable: "VehicleInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleInventories_VehicleInventoryId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleInventories");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleInventoryId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleInventoryId",
                table: "Vehicles");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
