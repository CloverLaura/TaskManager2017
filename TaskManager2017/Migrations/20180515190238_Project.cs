using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TaskManager2017.Migrations
{
    public partial class Project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Project_ProjectID",
                table: "Task");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "Task",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Project",
                table: "Task",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Project_ProjectID",
                table: "Task",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Project_ProjectID",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Project",
                table: "Task");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "Task",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Project_ProjectID",
                table: "Task",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
