using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprilisJam.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "UserApplications",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "School",
                table: "UserApplications",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "UserApplications",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserApplications",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserApplications",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "UserApplications",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "AprilisQuestion",
                table: "UserApplications",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalNotes",
                table: "UserApplications",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "UserApplications",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "School",
                table: "UserApplications",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "UserApplications",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserApplications",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserApplications",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "UserApplications",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AprilisQuestion",
                table: "UserApplications",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalNotes",
                table: "UserApplications",
                nullable: true);
        }
    }
}
