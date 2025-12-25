using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMedialityc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableUser0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rol",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserPhones_userId",
                table: "UserPhones",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmails_userId",
                table: "UserEmails",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEmails_Users_userId",
                table: "UserEmails",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhones_Users_userId",
                table: "UserPhones",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEmails_Users_userId",
                table: "UserEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhones_Users_userId",
                table: "UserPhones");

            migrationBuilder.DropIndex(
                name: "IX_UserPhones_userId",
                table: "UserPhones");

            migrationBuilder.DropIndex(
                name: "IX_UserEmails_userId",
                table: "UserEmails");

            migrationBuilder.DropColumn(
                name: "rol",
                table: "Users");
        }
    }
}
