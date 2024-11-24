using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class _32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_Users_UserId",
                table: "Configs");

            migrationBuilder.DropIndex(
                name: "IX_Configs_UserId",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Configs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Users",
                type: "TEXT",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Configs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Configs_UserId",
                table: "Configs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_Users_UserId",
                table: "Configs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
