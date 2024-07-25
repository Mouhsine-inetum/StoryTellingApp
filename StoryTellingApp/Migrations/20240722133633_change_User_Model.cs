using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryTellingApp.Migrations
{
    /// <inheritdoc />
    public partial class change_User_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Users",
                newName: "user_id");

            migrationBuilder.AddColumn<string>(
                name: "avatar",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "username",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Users",
                newName: "userId");
        }
    }
}
