using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_DonNghiPhep.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_V7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "LeaveRequest");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "LeaveRequest",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentId",
                table: "LeaveRequest",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextApproverId",
                table: "LeaveRequest",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequest_ApprovedById",
                table: "LeaveRequest",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequest_DepartmentId",
                table: "LeaveRequest",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequest_NextApproverId",
                table: "LeaveRequest",
                column: "NextApproverId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequest_Department_DepartmentId",
                table: "LeaveRequest",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Department_id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequest_Employee_ApprovedById",
                table: "LeaveRequest",
                column: "ApprovedById",
                principalTable: "Employee",
                principalColumn: "Employee_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequest_Employee_NextApproverId",
                table: "LeaveRequest",
                column: "NextApproverId",
                principalTable: "Employee",
                principalColumn: "Employee_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequest_Department_DepartmentId",
                table: "LeaveRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequest_Employee_ApprovedById",
                table: "LeaveRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequest_Employee_NextApproverId",
                table: "LeaveRequest");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequest_ApprovedById",
                table: "LeaveRequest");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequest_DepartmentId",
                table: "LeaveRequest");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequest_NextApproverId",
                table: "LeaveRequest");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "LeaveRequest");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "LeaveRequest");

            migrationBuilder.DropColumn(
                name: "NextApproverId",
                table: "LeaveRequest");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "LeaveRequest",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
