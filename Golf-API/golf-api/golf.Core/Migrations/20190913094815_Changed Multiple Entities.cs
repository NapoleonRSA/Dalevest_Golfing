using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace golf.Core.Migrations
{
    public partial class ChangedMultipleEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hole_Player_PlayerId",
                table: "Hole");

            migrationBuilder.DropIndex(
                name: "IX_Hole_PlayerId",
                table: "Hole");

            migrationBuilder.DropColumn(
                name: "Team_id",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Hole");

            migrationBuilder.RenameColumn(
                name: "Strokes",
                table: "Hole",
                newName: "Stroke");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Hole",
                newName: "Par");

            migrationBuilder.AddColumn<double>(
                name: "HandiCap",
                table: "Player",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Hole",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    HoleId = table.Column<int>(nullable: false),
                    GameScore = table.Column<int>(nullable: false),
                    GamePoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Score_Hole_HoleId",
                        column: x => x.HoleId,
                        principalTable: "Hole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Score_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hole_CourseId",
                table: "Hole",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_CourseId",
                table: "Game",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_GameId",
                table: "Score",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_HoleId",
                table: "Score",
                column: "HoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_PlayerId",
                table: "Score",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hole_Course_CourseId",
                table: "Hole",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hole_Course_CourseId",
                table: "Hole");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Hole_CourseId",
                table: "Hole");

            migrationBuilder.DropColumn(
                name: "HandiCap",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Hole");

            migrationBuilder.RenameColumn(
                name: "Stroke",
                table: "Hole",
                newName: "Strokes");

            migrationBuilder.RenameColumn(
                name: "Par",
                table: "Hole",
                newName: "Score");

            migrationBuilder.AddColumn<int>(
                name: "Team_id",
                table: "Player",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalScore",
                table: "Player",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Hole",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hole_PlayerId",
                table: "Hole",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hole_Player_PlayerId",
                table: "Hole",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
