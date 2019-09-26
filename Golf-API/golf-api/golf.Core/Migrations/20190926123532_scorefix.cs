using Microsoft.EntityFrameworkCore.Migrations;

namespace golf.Core.Migrations
{
    public partial class scorefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHoleScore_Score_ScoreId",
                table: "PlayerHoleScore");

            migrationBuilder.RenameColumn(
                name: "ScoreId",
                table: "PlayerHoleScore",
                newName: "GameScoreId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerHoleScore_ScoreId",
                table: "PlayerHoleScore",
                newName: "IX_PlayerHoleScore_GameScoreId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHoleScore_Score_GameScoreId",
                table: "PlayerHoleScore",
                column: "GameScoreId",
                principalTable: "Score",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerHoleScore_Score_GameScoreId",
                table: "PlayerHoleScore");

            migrationBuilder.RenameColumn(
                name: "GameScoreId",
                table: "PlayerHoleScore",
                newName: "ScoreId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerHoleScore_GameScoreId",
                table: "PlayerHoleScore",
                newName: "IX_PlayerHoleScore_ScoreId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerHoleScore_Score_ScoreId",
                table: "PlayerHoleScore",
                column: "ScoreId",
                principalTable: "Score",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
