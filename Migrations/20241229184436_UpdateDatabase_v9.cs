using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_DonNghiPhep.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase_v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_Department_id",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequest_Department_DepartmentId",
                table: "LeaveRequest");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Department_id",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Department_id",
                table: "Employee");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequest_Department_DepartmentId",
                table: "LeaveRequest",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Department_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequest_Department_DepartmentId",
                table: "LeaveRequest");

            migrationBuilder.AddColumn<string>(
                name: "Department_id",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Department_id",
                table: "Employee",
                column: "Department_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_Department_id",
                table: "Employee",
                column: "Department_id",
                principalTable: "Department",
                principalColumn: "Department_id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequest_Department_DepartmentId",
                table: "LeaveRequest",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Department_id");
        }
    }
}
