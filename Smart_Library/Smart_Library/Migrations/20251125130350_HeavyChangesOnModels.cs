using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smart_Library.Migrations
{
    /// <inheritdoc />
    public partial class HeavyChangesOnModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentNumberOfBooksBorrowed",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TotalNumberOfBooksBorrowed",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CurrentNumberOfBooksBorrowed",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "PositionLevel",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "TotalNumberOfBooksBorrowed",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "NumberOfAvailableCopies",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "NumberOfCopies",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "RentCost",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<int>>(
                name: "book_id",
                table: "Loans",
                type: "integer[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Faculties",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isBorrowed",
                table: "Books",
                type: "boolean",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "book_id",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "isBorrowed",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "CurrentNumberOfBooksBorrowed",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalNumberOfBooksBorrowed",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentNumberOfBooksBorrowed",
                table: "Faculties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositionLevel",
                table: "Faculties",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TotalNumberOfBooksBorrowed",
                table: "Faculties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAvailableCopies",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfCopies",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "RentCost",
                table: "Books",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
