using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainData.Migrations
{
    /// <inheritdoc />
    public partial class u4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "ContentBackOne",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "FlashCards");

            migrationBuilder.RenameColumn(
                name: "TitleBackTwo",
                table: "FlashCards",
                newName: "FrontContent");

            migrationBuilder.RenameColumn(
                name: "TitleBackOne",
                table: "FlashCards",
                newName: "BackContent");

            migrationBuilder.RenameColumn(
                name: "SoundUrlBack",
                table: "FlashCards",
                newName: "FrontSoundUrl");

            migrationBuilder.RenameColumn(
                name: "SoundUrl",
                table: "FlashCards",
                newName: "FrontImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageUrlBack",
                table: "FlashCards",
                newName: "FrontDescription");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "FlashCards",
                newName: "BackSoundUrl");

            migrationBuilder.RenameColumn(
                name: "ContentBackTwo",
                table: "FlashCards",
                newName: "BackImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "BackDescription",
                table: "FlashCards",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackDescription",
                table: "FlashCards");

            migrationBuilder.RenameColumn(
                name: "FrontSoundUrl",
                table: "FlashCards",
                newName: "SoundUrlBack");

            migrationBuilder.RenameColumn(
                name: "FrontImageUrl",
                table: "FlashCards",
                newName: "SoundUrl");

            migrationBuilder.RenameColumn(
                name: "FrontDescription",
                table: "FlashCards",
                newName: "ImageUrlBack");

            migrationBuilder.RenameColumn(
                name: "FrontContent",
                table: "FlashCards",
                newName: "TitleBackTwo");

            migrationBuilder.RenameColumn(
                name: "BackSoundUrl",
                table: "FlashCards",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "BackImageUrl",
                table: "FlashCards",
                newName: "ContentBackTwo");

            migrationBuilder.RenameColumn(
                name: "BackContent",
                table: "FlashCards",
                newName: "TitleBackOne");

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
                name: "Title",
                table: "FlashCards",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
