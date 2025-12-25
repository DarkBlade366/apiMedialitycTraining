using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMedialityc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEmails_Users_userId",
                table: "UserEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhones_Users_userId",
                table: "UserPhones");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "fullName",
                table: "Users",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "rol",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserPhones",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "UserPhones",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserPhones",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserPhones_userId",
                table: "UserPhones",
                newName: "IX_UserPhones_UserId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserEmails",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "UserEmails",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserEmails",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserEmails_userId",
                table: "UserEmails",
                newName: "IX_UserEmails_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEmails_Users_UserId",
                table: "UserEmails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhones_Users_UserId",
                table: "UserPhones",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEmails_Users_UserId",
                table: "UserEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhones_Users_UserId",
                table: "UserPhones");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Users",
                newName: "fullName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "rol");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserPhones",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "UserPhones",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserPhones",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_UserPhones_UserId",
                table: "UserPhones",
                newName: "IX_UserPhones_userId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserEmails",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "UserEmails",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserEmails",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_UserEmails_UserId",
                table: "UserEmails",
                newName: "IX_UserEmails_userId");

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
    }
}
