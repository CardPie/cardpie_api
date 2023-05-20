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
            migrationBuilder.RenameColumn(
                name: "FrontImage",
                table: "FlashCards",
                newName: "TitleBackTwo");

            migrationBuilder.RenameColumn(
                name: "Front",
                table: "FlashCards",
                newName: "TitleBackOne");

            migrationBuilder.RenameColumn(
                name: "BackImage",
                table: "FlashCards",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Back",
                table: "FlashCards",
                newName: "ImageUrlBack");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Folders",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "FlashCards",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ContentBackOne",
                table: "FlashCards",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ContentBackTwo",
                table: "FlashCards",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "FlashCards",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SoundUrl",
                table: "FlashCards",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SoundUrlBack",
                table: "FlashCards",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "StudySessionId",
                table: "FlashCards",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<bool>(
                name: "IsDailyRemind",
                table: "Decks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WeeklyReminderDays",
                table: "Decks",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeckId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Like = table.Column<int>(type: "int", nullable: false),
                    Report = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PostId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interactions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FlashCards_StudySessionId",
                table: "FlashCards",
                column: "StudySessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_PostId",
                table: "Interactions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_UserId",
                table: "Interactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_DeckId",
                table: "Posts",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCards_StudySessions_StudySessionId",
                table: "FlashCards",
                column: "StudySessionId",
                principalTable: "StudySessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCards_StudySessions_StudySessionId",
                table: "FlashCards");

            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_FlashCards_StudySessionId",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "ContentBackOne",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "ContentBackTwo",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "SoundUrl",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "SoundUrlBack",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "StudySessionId",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "IsDailyRemind",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "WeeklyReminderDays",
                table: "Decks");

            migrationBuilder.RenameColumn(
                name: "TitleBackTwo",
                table: "FlashCards",
                newName: "FrontImage");

            migrationBuilder.RenameColumn(
                name: "TitleBackOne",
                table: "FlashCards",
                newName: "Front");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "FlashCards",
                newName: "BackImage");

            migrationBuilder.RenameColumn(
                name: "ImageUrlBack",
                table: "FlashCards",
                newName: "Back");
        }
    }
}
