using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerBranchBack.Migrations
{
    public partial class AddingUrlInLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Logs");
        }
    }
}
