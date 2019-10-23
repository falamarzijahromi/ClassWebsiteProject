using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Efcore.Migrations
{
    public partial class NationalCodeAddedToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalCode",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalCode",
                table: "Users");
        }
    }
}
