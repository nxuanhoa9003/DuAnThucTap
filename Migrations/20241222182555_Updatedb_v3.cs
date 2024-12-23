using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_DonNghiPhep.Migrations
{
    /// <inheritdoc />
    public partial class Updatedb_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_User_UserId",
                table: "Log");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Log",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_User_UserId",
                table: "Log",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_User_UserId",
                table: "Log");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Log",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Log_User_UserId",
                table: "Log",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
