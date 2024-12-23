using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_DonNghiPhep.Migrations
{
    /// <inheritdoc />
    public partial class Updatedb_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role_ID",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role_ID",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
