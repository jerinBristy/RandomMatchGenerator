using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudServiceChallenge2.Migrations
{
    public partial class SecondaryCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TitleMasterModel",
                columns: table => new
                {
                    TitleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleName = table.Column<string>(type: "varchar(8)", nullable: true),
                    NumberOfWinsAvailable = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleMasterModel", x => x.TitleId);
                });

            migrationBuilder.CreateTable(
                name: "PossessionTitleDataModel",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    TitleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PossessionTitleDataModel", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_PossessionTitleDataModel_TitleMasterModel_TitleId",
                        column: x => x.TitleId,
                        principalTable: "TitleMasterModel",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PossessionTitleDataModel_UserDataModel_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDataModel",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PossessionTitleDataModel_TitleId",
                table: "PossessionTitleDataModel",
                column: "TitleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PossessionTitleDataModel");

            migrationBuilder.DropTable(
                name: "TitleMasterModel");
        }
    }
}
