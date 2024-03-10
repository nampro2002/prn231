using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_HE160575_Project04.Migrations
{
    public partial class Add_Data_Location_House : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Xóa toàn bộ dữ liệu từ bảng Houses
            migrationBuilder.Sql("DELETE FROM Houses");
            migrationBuilder.InsertData(
           table: "Houses",
           columns: new[] { "Title", "Image", "Price", "Area", "Location", "DaysAgo", "IsActive", "Type", "Quantity", "Description", "Rate", "PublicDay", "UserId", "ProvinceCode", "DistrictCode", "WardCode" },
           values: new object[,]
           {
                    { "Căn hộ 1", "https://picsum.photos/200/300", 5000000m, 60, "Quận 2, TP.HCM", 2, true, 1, 4, "Phòng đẹp lắm", 5, DateTime.Now, 1, "01", "001", "00001" },
                    { "Căn hộ 2", "https://picsum.photos/200/300", 6000000m, 70, "Quận 3, TP.HCM", 3, true, 1, 3, "Phòng đẹp lung linh", 4, DateTime.Now, 1, "02", "002", "00001" },
                    { "Căn hộ 3", "https://picsum.photos/200/300", 5500000m, 65, "Quận 4, TP.HCM", 4, true, 1, 5, "Phòng view đẹp", 4, DateTime.Now, 1, "04", "003", "00001" },
                    { "Căn hộ 4", "https://picsum.photos/200/300", 5200000m, 55, "Quận 5, TP.HCM", 5, true, 1, 3, "Phòng mới xây", 5, DateTime.Now, 1, "06", "004", "00001" },
                    { "Căn hộ 5", "https://picsum.photos/200/300", 5800000m, 70, "Quận 6, TP.HCM", 6, true, 1, 2, "Phòng đẹp, giá rẻ", 3, DateTime.Now, 1, "08", "005", "00001" },
                    { "Căn hộ 6", "https://picsum.photos/200/300", 5300000m, 60, "Quận 7, TP.HCM", 7, true, 1, 4, "Phòng có nhiều tiện ích", 4, DateTime.Now, 1, "10", "006", "00001" },
                    { "Căn hộ 7", "https://picsum.photos/200/300", 5400000m, 62, "Quận 8, TP.HCM", 8, true, 1, 5, "Phòng có view đẹp", 5, DateTime.Now, 1, "11", "007", "00001" },
                    { "Căn hộ 8", "https://picsum.photos/200/300", 5700000m, 68, "Quận 9, TP.HCM", 9, true, 1, 3, "Phòng có tiện nghi", 4, DateTime.Now, 1, "12", "008", "00001" },
                    { "Căn hộ 9", "https://picsum.photos/200/300", 5600000m, 66, "Quận 10, TP.HCM", 10, true, 1, 2, "Phòng mới xây, đẹp", 4, DateTime.Now, 1, "14", "009", "00001" },
                    { "Căn hộ 10", "https://picsum.photos/200/300", 5900000m, 72, "Quận 11, TP.HCM", 11, true, 1, 4, "Phòng thoáng mát", 5, DateTime.Now, 1, "15", "016", "00001" }
           });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Houses");
        }
    }
}
