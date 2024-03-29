﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaxiCrush.Infrastructure.Migrations
{
    public partial class Added_PermissionLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PermissionLevel",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionLevel",
                table: "Roles");
        }
    }
}
