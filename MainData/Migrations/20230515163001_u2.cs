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
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCards_StudySessions_StudySessionId",
                table: "FlashCards");

            migrationBuilder.DropIndex(
                name: "IX_FlashCards_StudySessionId",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "StudySessionId",
                table: "FlashCards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudySessionId",
                table: "FlashCards",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlashCards_StudySessionId",
                table: "FlashCards",
                column: "StudySessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCards_StudySessions_StudySessionId",
                table: "FlashCards",
                column: "StudySessionId",
                principalTable: "StudySessions",
                principalColumn: "Id");
        }
    }
}
