using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenameOldTableToNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStateJsons_Configs_ConfigId",
                table: "GameStateJsons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameStateJsons",
                table: "GameStateJsons");

            migrationBuilder.RenameTable(
                name: "GameStateJsons",
                newName: "GameStates");

            migrationBuilder.RenameIndex(
                name: "IX_GameStateJsons_ConfigId",
                table: "GameStates",
                newName: "IX_GameStates_ConfigId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameStates",
                table: "GameStates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStates_Configs_ConfigId",
                table: "GameStates",
                column: "ConfigId",
                principalTable: "Configs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStates_Configs_ConfigId",
                table: "GameStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameStates",
                table: "GameStates");

            migrationBuilder.RenameTable(
                name: "GameStates",
                newName: "GameStateJsons");

            migrationBuilder.RenameIndex(
                name: "IX_GameStates_ConfigId",
                table: "GameStateJsons",
                newName: "IX_GameStateJsons_ConfigId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameStateJsons",
                table: "GameStateJsons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStateJsons_Configs_ConfigId",
                table: "GameStateJsons",
                column: "ConfigId",
                principalTable: "Configs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
