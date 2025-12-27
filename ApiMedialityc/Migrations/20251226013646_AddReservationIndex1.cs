using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMedialityc.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationIndex1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_VehicleId_StartDate_EndDate_UserId",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "Reservations_VehicleDateUser",
                table: "Reservations",
                columns: new[] { "VehicleId", "StartDate", "EndDate", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Reservations_VehicleDateUser",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_VehicleId_StartDate_EndDate_UserId",
                table: "Reservations",
                columns: new[] { "VehicleId", "StartDate", "EndDate", "UserId" });
        }
    }
}
