using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorTarefas.Migrations
{
    /// <inheritdoc />
    public partial class ajusteRoleusuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UserRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name", "Slug" },
                values: new object[] { new Guid("e8197eed-c5dc-47f6-9643-7e27009f2691"), "Usuario", "usuario" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("d2f1f799-09b6-44b0-91a4-13d5cd3640b1"),
                column: "PasswordHash",
                value: "10000.UVcGG6SBYPfze7itts7wDg==.TtF8UWOXM9d6yfqijqN8vCP4CKmmZ92mSzd9hp5qLp8=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("e8197eed-c5dc-47f6-9643-7e27009f2691"));

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRole");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("d2f1f799-09b6-44b0-91a4-13d5cd3640b1"),
                column: "PasswordHash",
                value: "10000.AhbXIee0l+Ayxta1JzFQiQ==.W19FoGMZKyXrmeNXVxuSRxQrm7sMZYzKb6oB2qoZpgk=");
        }
    }
}
