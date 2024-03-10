using Microsoft.IdentityModel.Tokens;
using PRN231_HE160575_Project04;
using PRN231_HE160575_Project04.ModelsV2;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PRN231_HE160575_Project04
{
    public static class JwtHelper
    {
        //Gi do
        public static string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("BACHSONGDUCHE160575BACHSONGDUCHE160575");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userId", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(30), // Sửa đổi giá trị này để token hết hạn sau ....
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateToken(int userId)
        {
            const string SecretKey = "BACHSONGDUCHE160575BACHSONGDUCHE160575";
            // Tạo các claim cho token, bao gồm cả userId
            var claims = new[]
                {
                new Claim("userId",  userId.ToString())
                // Các claim khác nếu cần
            };
            // Tạo chữ ký số từ khóa bí mật
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            // Tạo mô tả cho token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10), // Thời gian hết hạn của token
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            // Tạo token sử dụng tokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // Chuyển đổi token thành chuỗi và trả về
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }





    }
}
