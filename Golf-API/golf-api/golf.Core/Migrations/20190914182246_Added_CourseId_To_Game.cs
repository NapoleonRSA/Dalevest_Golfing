using Microsoft.EntityFrameworkCore.Migrations;

namespace golf.Core.Migrations
{
    public partial class Added_CourseId_To_Game : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Game",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_CourseId",
                table: "Game",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Course_CourseId",
                table: "Game",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Course_CourseId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_CourseId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Game");
        }
    }
}
