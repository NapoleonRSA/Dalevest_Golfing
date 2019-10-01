using Microsoft.EntityFrameworkCore.Migrations;

namespace golf.Core.Migrations
{
    public partial class teamChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaptainId",
                table: "Team",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamName",
                table: "Team",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HandiCap",
                table: "Player",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<int>(
                name: "TeamSize",
                table: "GameType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Team_CaptainId",
                table: "Team",
                column: "CaptainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Player_CaptainId",
                table: "Team",
                column: "CaptainId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Player_CaptainId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_CaptainId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "CaptainId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "TeamName",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "TeamSize",
                table: "GameType");

            migrationBuilder.AlterColumn<double>(
                name: "HandiCap",
                table: "Player",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
