using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;
using System.Linq;
using static PRN231_HE160575_Project04.Controllers.UserController;

namespace PRN231_HE160575_Project04
{
    public class MyAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string _secretKey;

        public MyAuthorizationFilter(string secretKey = "BACHSONGDUCHE160575")
        {
            _secretKey = secretKey;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            int userId = ValidateToken(authorizationHeader);
            if (userId == -1)
            {
                context.ModelState.AddModelError("Error", "Xác thực tài khoản thất bại !");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = context.ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                context.Result = new BadRequestObjectResult(errorResponse);
            }
            else
            {
                context.HttpContext.Items["UserId"] = userId;
            }
        }
        private int ValidateToken(string authorizationHeader)
        {
            try
            {
                // Kiểm tra xem Authorization header có tồn tại không
                if (string.IsNullOrEmpty(authorizationHeader))
                {
                    return -1;
                }
                var token = authorizationHeader.Replace("Bearer ", ""); // Loại bỏ "Bearer" từ header
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var tokenHandler = new JwtSecurityTokenHandler();
                // Thiết lập xác thực token
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                // Lấy ra userId từ claim
                var userIdClaim = principal.FindFirst("userId");
                // Kiểm tra thời gian hết hạn của token
                var expireDate = validatedToken.ValidTo;
                if (expireDate < DateTime.UtcNow)
                {
                    return -1;
                }
                if (userIdClaim != null)
                {
                    // Chuyển đổi userId từ chuỗi sang số nguyên
                    if (int.TryParse(userIdClaim.Value, out int userId))
                    {
                        return userId;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                // Có thể xảy ra lỗi trong quá trình xác thực token
                return -1;
            }
        }
    }
}
