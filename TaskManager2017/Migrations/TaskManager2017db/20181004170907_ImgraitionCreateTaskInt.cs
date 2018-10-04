using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TaskManager2017.Migrations.TaskManager2017db
{
    public partial class ImgraitionCreateTaskInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Task");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByInt",
                table: "Task",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByInt",
                table: "Task");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Task",
                nullable: true);
        }
    }
}
