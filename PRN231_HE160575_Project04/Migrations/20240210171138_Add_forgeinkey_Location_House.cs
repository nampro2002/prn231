using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_HE160575_Project04.Migrations
{
    public partial class Add_forgeinkey_Location_House : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistrictCode",
                table: "Houses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceCode",
                table: "Houses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WardCode",
                table: "Houses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Houses_DistrictCode",
                table: "Houses",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_ProvinceCode",
                table: "Houses",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_WardCode",
                table: "Houses",
                column: "WardCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Districts_DistrictCode",
                table: "Houses",
                column: "DistrictCode",
                principalTable: "Districts",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Provinces_ProvinceCode",
                table: "Houses",
                column: "ProvinceCode",
                principalTable: "Provinces",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Wards_WardCode",
                table: "Houses",
                column: "WardCode",
                principalTable: "Wards",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Districts_DistrictCode",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Provinces_ProvinceCode",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Wards_WardCode",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_DistrictCode",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_ProvinceCode",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_WardCode",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "WardCode",
                table: "Houses");
        }
    }
}
