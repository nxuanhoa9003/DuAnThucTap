using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_DonNghiPhep.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_Department_id",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Title_Title_id",
                table: "Employee");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_Department_id",
                table: "Employee",
                column: "Department_id",
                principalTable: "Department",
                principalColumn: "Department_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Title_Title_id",
                table: "Employee",
                column: "Title_id",
                principalTable: "Title",
                principalColumn: "Title_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_Department_id",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Title_Title_id",
                table: "Employee");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_Department_id",
                table: "Employee",
                column: "Department_id",
                principalTable: "Department",
                principalColumn: "Department_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Title_Title_id",
                table: "Employee",
                column: "Title_id",
                principalTable: "Title",
                principalColumn: "Title_id");
        }
    }
}
