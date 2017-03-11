using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprilisJam.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "School",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "School",
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
    }
}
