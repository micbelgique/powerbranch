using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerBranchBack.Migrations
{
    public partial class addStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Staff",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Staff",
                table: "AspNetUsers");
        }
    }
}
