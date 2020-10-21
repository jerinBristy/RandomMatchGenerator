using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudServiceChallenge2.Migrations
{
    public partial class ThirdCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PossessionTitleDataModel_TitleMasterModel_TitleId",
                table: "PossessionTitleDataModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PossessionTitleDataModel_UserDataModel_UserId",
                table: "PossessionTitleDataModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDataModel",
                table: "UserDataModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TitleMasterModel",
                table: "TitleMasterModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PossessionTitleDataModel",
                table: "PossessionTitleDataModel");

            migrationBuilder.RenameTable(
                name: "UserDataModel",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "TitleMasterModel",
                newName: "TitleMaster");

            migrationBuilder.RenameTable(
                name: "PossessionTitleDataModel",
                newName: "PossessionTitle");

            migrationBuilder.RenameIndex(
                name: "IX_PossessionTitleDataModel_TitleId",
                table: "PossessionTitle",
                newName: "IX_PossessionTitle_TitleId");

            migrationBuilder.AlterColumn<string>(
                name: "TitleName",
                table: "TitleMaster",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TitleMaster",
                table: "TitleMaster",
                column: "TitleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PossessionTitle",
                table: "PossessionTitle",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PossessionTitle_TitleMaster_TitleId",
                table: "PossessionTitle",
                column: "TitleId",
                principalTable: "TitleMaster",
                principalColumn: "TitleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PossessionTitle_Users_UserId",
                table: "PossessionTitle",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PossessionTitle_TitleMaster_TitleId",
                table: "PossessionTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_PossessionTitle_Users_UserId",
                table: "PossessionTitle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TitleMaster",
                table: "TitleMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PossessionTitle",
                table: "PossessionTitle");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserDataModel");

            migrationBuilder.RenameTable(
                name: "TitleMaster",
                newName: "TitleMasterModel");

            migrationBuilder.RenameTable(
                name: "PossessionTitle",
                newName: "PossessionTitleDataModel");

            migrationBuilder.RenameIndex(
                name: "IX_PossessionTitle_TitleId",
                table: "PossessionTitleDataModel",
                newName: "IX_PossessionTitleDataModel_TitleId");

            migrationBuilder.AlterColumn<string>(
                name: "TitleName",
                table: "TitleMasterModel",
                type: "varchar(8)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDataModel",
                table: "UserDataModel",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TitleMasterModel",
                table: "TitleMasterModel",
                column: "TitleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PossessionTitleDataModel",
                table: "PossessionTitleDataModel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PossessionTitleDataModel_TitleMasterModel_TitleId",
                table: "PossessionTitleDataModel",
                column: "TitleId",
                principalTable: "TitleMasterModel",
                principalColumn: "TitleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PossessionTitleDataModel_UserDataModel_UserId",
                table: "PossessionTitleDataModel",
                column: "UserId",
                principalTable: "UserDataModel",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
