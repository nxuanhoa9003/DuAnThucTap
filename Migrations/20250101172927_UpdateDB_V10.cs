using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_DonNghiPhep.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_V10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApprovedById",
                table: "ApprovalHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "ApprovalHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Employee_ID",
                table: "ApprovalHistories",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalHistories_Employee_ID",
                table: "ApprovalHistories",
                column: "Employee_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalHistories_Employee_Employee_ID",
                table: "ApprovalHistories",
                column: "Employee_ID",
                principalTable: "Employee",
                principalColumn: "Employee_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalHistories_Employee_Employee_ID",
                table: "ApprovalHistories");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalHistories_Employee_ID",
                table: "ApprovalHistories");

            migrationBuilder.DropColumn(
                name: "Employee_ID",
                table: "ApprovalHistories");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedById",
                table: "ApprovalHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "ApprovalHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
