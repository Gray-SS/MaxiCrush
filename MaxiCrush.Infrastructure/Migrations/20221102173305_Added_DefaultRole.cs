using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaxiCrush.Infrastructure.Migrations
{
    public partial class Added_DefaultRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultRole",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefaultRole",
                table: "Roles");
        }
    }
}
