using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_Users_PrimaryUserId",
                table: "Configs");

            migrationBuilder.DropForeignKey(
                name: "FK_Configs_Users_SecondaryUserId",
                table: "Configs");

            migrationBuilder.DropIndex(
                name: "IX_Configs_PrimaryUserId",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "PrimaryUserId",
                table: "Configs");

            migrationBuilder.RenameColumn(
                name: "SecondaryUserId",
                table: "Configs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Configs_SecondaryUserId",
                table: "Configs",
                newName: "IX_Configs_UserId");

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    PrimaryUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    SecondaryUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Users_PrimaryUserId",
                        column: x => x.PrimaryUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Users_SecondaryUserId",
                        column: x => x.SecondaryUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameStates",
                columns: table => new
                {
                    GameStateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameStateJson = table.Column<string>(type: "TEXT", maxLength: 10240, nullable: false),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStates", x => x.GameStateId);
                    table.ForeignKey(
                        name: "FK_GameStates_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_PrimaryUserId",
                table: "Games",
                column: "PrimaryUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_SecondaryUserId",
                table: "Games",
                column: "SecondaryUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_GameId",
                table: "GameStates",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_Users_UserId",
                table: "Configs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_Users_UserId",
                table: "Configs");

            migrationBuilder.DropTable(
                name: "GameStates");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Configs",
                newName: "SecondaryUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Configs_UserId",
                table: "Configs",
                newName: "IX_Configs_SecondaryUserId");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryUserId",
                table: "Configs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Configs_PrimaryUserId",
                table: "Configs",
                column: "PrimaryUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_Users_PrimaryUserId",
                table: "Configs",
                column: "PrimaryUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_Users_SecondaryUserId",
                table: "Configs",
                column: "SecondaryUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
