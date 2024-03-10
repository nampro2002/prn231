using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_HE160575_Project04.Migrations
{
    public partial class Add_Location_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdministrativeRegions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code_name_en = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrativeRegions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdministrativeUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Full_name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Short_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Short_name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code_name_en = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrativeUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Full_name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Administrative_unit_id = table.Column<int>(type: "int", nullable: true),
                    Administrative_region_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Provinces_AdministrativeRegions_Administrative_region_id",
                        column: x => x.Administrative_region_id,
                        principalTable: "AdministrativeRegions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Provinces_AdministrativeUnits_Administrative_unit_id",
                        column: x => x.Administrative_unit_id,
                        principalTable: "AdministrativeUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Full_name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province_code = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Administrative_unit_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Districts_AdministrativeUnits_Administrative_unit_id",
                        column: x => x.Administrative_unit_id,
                        principalTable: "AdministrativeUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_Province_code",
                        column: x => x.Province_code,
                        principalTable: "Provinces",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Full_name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District_code = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Administrative_unit_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Wards_AdministrativeUnits_Administrative_unit_id",
                        column: x => x.Administrative_unit_id,
                        principalTable: "AdministrativeUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Wards_Districts_District_code",
                        column: x => x.District_code,
                        principalTable: "Districts",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Administrative_unit_id",
                table: "Districts",
                column: "Administrative_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Province_code",
                table: "Districts",
                column: "Province_code");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_Administrative_region_id",
                table: "Provinces",
                column: "Administrative_region_id");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_Administrative_unit_id",
                table: "Provinces",
                column: "Administrative_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_Administrative_unit_id",
                table: "Wards",
                column: "Administrative_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_District_code",
                table: "Wards",
                column: "District_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "AdministrativeRegions");

            migrationBuilder.DropTable(
                name: "AdministrativeUnits");
        }
    }
}
