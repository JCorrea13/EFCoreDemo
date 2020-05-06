﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace EfCore.Data.Migrations
{
    public partial class addingHorseNickName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "Horses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NickName",
                table: "Horses");
        }
    }
}
