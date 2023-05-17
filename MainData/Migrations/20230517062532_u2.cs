using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainData.Migrations
{
    /// <inheritdoc />
    public partial class u2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Decks",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Decks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "LearningLength",
                table: "Decks",
                type: "time(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Decks",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "RecallStrength",
                table: "Decks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ReminderTime",
                table: "Decks",
                type: "time(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpacedRepetitionStrategyLevel",
                table: "Decks",
                type: "int",
                nullable: false,
                defaultValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "LearningLength",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "RecallStrength",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "ReminderTime",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "SpacedRepetitionStrategyLevel",
                table: "Decks");
        }
    }
}
