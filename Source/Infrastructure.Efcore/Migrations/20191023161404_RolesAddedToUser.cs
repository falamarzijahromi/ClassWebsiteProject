﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Efcore.Migrations
{
    public partial class RolesAddedToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Roles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserId",
                table: "Roles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UserId",
                table: "Roles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Roles");
        }
    }
}
