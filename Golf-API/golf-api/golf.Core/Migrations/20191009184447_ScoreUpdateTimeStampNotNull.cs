using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace golf.Core.Migrations
{
    public partial class ScoreUpdateTimeStampNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScoreUpdated",
                table: "PlayerHoleScore",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScoreUpdated",
                table: "PlayerHoleScore",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
