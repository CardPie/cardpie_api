using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainData.Migrations
{
    /// <inheritdoc />
    public partial class u3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SavedDecks");

            migrationBuilder.AddColumn<Guid>(
                name: "EditorId",
                table: "Users",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "EditorId",
                table: "Tokens",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "EditorId",
                table: "StudySessions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "EditorId",
                table: "SavedDecks",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "EditorId",
                table: "Posts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "EditorId",
                table: "Interactions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "EditorId",
                table: "Folders",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "EditorId",
                table: "FlashCards",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "EditorId",
                table: "Decks",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditorId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EditorId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "EditorId",
                table: "StudySessions");

            migrationBuilder.DropColumn(
                name: "EditorId",
                table: "SavedDecks");

            migrationBuilder.DropColumn(
                name: "EditorId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "EditorId",
                table: "Interactions");

            migrationBuilder.DropColumn(
                name: "EditorId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "EditorId",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "EditorId",
                table: "Decks");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SavedDecks",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }
    }
}
