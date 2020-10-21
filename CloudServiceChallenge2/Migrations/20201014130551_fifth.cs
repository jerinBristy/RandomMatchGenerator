using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudServiceChallenge2.Migrations
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PossessionTitle",
                table: "PossessionTitle");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PossessionTitle",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PossessionTitle",
                table: "PossessionTitle",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PossessionTitle_UserId",
                table: "PossessionTitle",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PossessionTitle",
                table: "PossessionTitle");

            migrationBuilder.DropIndex(
                name: "IX_PossessionTitle_UserId",
                table: "PossessionTitle");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PossessionTitle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PossessionTitle",
                table: "PossessionTitle",
                column: "UserId");
        }
    }
}
