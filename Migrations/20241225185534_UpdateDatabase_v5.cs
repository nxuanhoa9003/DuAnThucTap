using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_DonNghiPhep.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase_v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Employee_id",
                table: "LeaveBalance",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Department",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_ParentId",
                table: "Department",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Department_ParentId",
                table: "Department",
                column: "ParentId",
                principalTable: "Department",
                principalColumn: "Department_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Department_ParentId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_ParentId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Department");

            migrationBuilder.AlterColumn<string>(
                name: "Employee_id",
                table: "LeaveBalance",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);
        }
    }
}
