using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudServiceChallenge2.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDataModel",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(8)", nullable: true),
                    NumberOfWins = table.Column<int>(nullable: false),
                    NumberOfDefeats = table.Column<int>(nullable: false),
                    NumberOfDraws = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDataModel", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDataModel");
        }
    }
}
