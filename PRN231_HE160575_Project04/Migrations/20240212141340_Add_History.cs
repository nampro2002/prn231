using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_HE160575_Project04.Migrations
{
    public partial class Add_History : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_AdministrativeUnits_Administrative_unit_id",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Districts_DistrictCode",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Provinces_ProvinceCode",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Wards_WardCode",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_AdministrativeRegions_Administrative_region_id",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_AdministrativeUnits_Administrative_unit_id",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_Wards_AdministrativeUnits_Administrative_unit_id",
                table: "Wards");

            migrationBuilder.AlterColumn<int>(
                name: "Administrative_unit_id",
                table: "Wards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValueSql: "(CONVERT([bit],(0)))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActived",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValueSql: "(CONVERT([bit],(0)))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Administrative_unit_id",
                table: "Provinces",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Administrative_region_id",
                table: "Provinces",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "WardCode",
                table: "Houses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ProvinceCode",
                table: "Houses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "DistrictCode",
                table: "Houses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "Administrative_unit_id",
                table: "Districts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_AdministrativeUnits_Administrative_unit_id",
                table: "Districts",
                column: "Administrative_unit_id",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Districts_DistrictCode",
                table: "Houses",
                column: "DistrictCode",
                principalTable: "Districts",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Provinces_ProvinceCode",
                table: "Houses",
                column: "ProvinceCode",
                principalTable: "Provinces",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Wards_WardCode",
                table: "Houses",
                column: "WardCode",
                principalTable: "Wards",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_AdministrativeRegions_Administrative_region_id",
                table: "Provinces",
                column: "Administrative_region_id",
                principalTable: "AdministrativeRegions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_AdministrativeUnits_Administrative_unit_id",
                table: "Provinces",
                column: "Administrative_unit_id",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wards_AdministrativeUnits_Administrative_unit_id",
                table: "Wards",
                column: "Administrative_unit_id",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_AdministrativeUnits_Administrative_unit_id",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Districts_DistrictCode",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Provinces_ProvinceCode",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Wards_WardCode",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_AdministrativeRegions_Administrative_region_id",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_AdministrativeUnits_Administrative_unit_id",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_Wards_AdministrativeUnits_Administrative_unit_id",
                table: "Wards");

            migrationBuilder.AlterColumn<int>(
                name: "Administrative_unit_id",
                table: "Wards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "(CONVERT([bit],(0)))");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActived",
                table: "Users",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "(CONVERT([bit],(0)))");

            migrationBuilder.AlterColumn<int>(
                name: "Administrative_unit_id",
                table: "Provinces",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Administrative_region_id",
                table: "Provinces",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WardCode",
                table: "Houses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProvinceCode",
                table: "Houses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DistrictCode",
                table: "Houses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Administrative_unit_id",
                table: "Districts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_AdministrativeUnits_Administrative_unit_id",
                table: "Districts",
                column: "Administrative_unit_id",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Districts_DistrictCode",
                table: "Houses",
                column: "DistrictCode",
                principalTable: "Districts",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Provinces_ProvinceCode",
                table: "Houses",
                column: "ProvinceCode",
                principalTable: "Provinces",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Wards_WardCode",
                table: "Houses",
                column: "WardCode",
                principalTable: "Wards",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_AdministrativeRegions_Administrative_region_id",
                table: "Provinces",
                column: "Administrative_region_id",
                principalTable: "AdministrativeRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_AdministrativeUnits_Administrative_unit_id",
                table: "Provinces",
                column: "Administrative_unit_id",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wards_AdministrativeUnits_Administrative_unit_id",
                table: "Wards",
                column: "Administrative_unit_id",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
