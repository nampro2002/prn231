using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_HE160575_Project04.Migrations
{
    public partial class AddUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
       table: "Users",
       columns: new[] { "UserId", "FullName", "Birthdate", "PhoneNumber", "Email", "Password", "Address", "Avatar" },
       values: new object[,]
       {
            { 1, "BachDuc", DateTime.Now.ToString("yyyy-MM-dd"), "0987654321", "bachduc@gmail.com", "Password", "55 Đại Mỗ", "BachDuc.jpg" },
            { 2, "John Doe", "1990-01-01", "1234567890", "john.doe@example.com", "password", "123 Main Street, City, Country", "https://picsum.photos/200/300" },
            { 3, "Back Land", "1995-02-15", "9876543210", "Back.Land@example.com", "securepassword", "456 Side Street, Town, Country", "https://picsum.photos/200/300" }
       });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValues: new object[] { 1, 2, 3 });
        }
    }
}
