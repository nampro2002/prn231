using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_HE160575_Project04.Migrations
{
    public partial class Add_House_TableAndData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    HouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaysAgo = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    PublicDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.HouseId);
                    table.ForeignKey(
                        name: "FK_Houses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Houses_UserId",
                table: "Houses",
                column: "UserId");

            migrationBuilder.InsertData(
       table: "Houses",
       columns: new[] { "Title", "Image", "Price", "Area", "Location", "DaysAgo", "IsActive", "Type", "Quantity", "Description", "Rate", "PublicDay", "UserId" },
       values: new object[,]
       {
            { "Căn hộ 1", "https://picsum.photos/200/300", 5000000m, 60, "Quận 2, TP.HCM", 2, true, 1, 4, "Phòng đẹp lắm", 5, DateTime.Now, 1 },
            { "Căn hộ 2", "https://picsum.photos/200/300", 6000000m, 70, "Quận 3, TP.HCM", 3, true, 1, 3, "Phòng đẹp lung linh", 4, DateTime.Now, 1 },
            { "Căn hộ 3", "https://picsum.photos/200/300", 5500000m, 65, "Quận 4, TP.HCM", 4, true, 1, 5, "Phòng view đẹp", 4, DateTime.Now, 1 },
            { "Căn hộ 4", "https://picsum.photos/200/300", 5200000m, 55, "Quận 5, TP.HCM", 5, true, 1, 3, "Phòng mới xây", 5, DateTime.Now, 1 },
            { "Căn hộ 5", "https://picsum.photos/200/300", 5800000m, 70, "Quận 6, TP.HCM", 6, true, 1, 2, "Phòng đẹp, giá rẻ", 3, DateTime.Now, 1 },
            { "Căn hộ 6", "https://picsum.photos/200/300", 5300000m, 60, "Quận 7, TP.HCM", 7, true, 1, 4, "Phòng có nhiều tiện ích", 4, DateTime.Now, 1 },
            { "Căn hộ 7", "https://picsum.photos/200/300", 5400000m, 62, "Quận 8, TP.HCM", 8, true, 1, 5, "Phòng có view đẹp", 5, DateTime.Now, 1 },
            { "Căn hộ 8", "https://picsum.photos/200/300", 5700000m, 68, "Quận 9, TP.HCM", 9, true, 1, 3, "Phòng có tiện nghi", 4, DateTime.Now, 1 },
            { "Căn hộ 9", "https://picsum.photos/200/300", 5600000m, 66, "Quận 10, TP.HCM", 10, true, 1, 2, "Phòng mới xây, đẹp", 4, DateTime.Now, 1 },
            { "Căn hộ 10", "https://picsum.photos/200/300", 5900000m, 72, "Quận 11, TP.HCM", 11, true, 1, 4, "Phòng thoáng mát", 5, DateTime.Now, 1 }
       });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Houses");
        }
    }
}
