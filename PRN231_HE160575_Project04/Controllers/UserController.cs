using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PRN231_HE160575_Project04.ModelsV2;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Filters;
using PRN231_HE160575_Project04.ModelsV2.DTO;

namespace PRN231_HE160575_Project04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly PRN231_ProjectContext _context;
        public UserController(PRN231_ProjectContext context)
        {
            _context = context;
        }
        //[Authorize]
        [ServiceFilter(typeof(MyAuthorizationFilter))]
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is int userId)
            {
                return Ok(_context.Users.SingleOrDefault(i => i.UserId == userId));
            }
            return BadRequest("User not found");
        }

        [HttpGet("getById")]
        public IActionResult GetUserById(int id)
        {
            User user = _context.Users.SingleOrDefault(i => i.UserId == id);

            return Ok(user);
        }

        [HttpPost("Login")]
        public IActionResult LoginUsers(LoginRequestUser user)
        {
            User target = _context.Users.SingleOrDefault(i => i.Email.Equals(user.Email) && i.Password.Equals(user.Password));
            if (target == null)
            {
                ModelState.AddModelError("Error", " Email or PassWord is in correct !");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
            if (target.IsActived == false)
            {
                ModelState.AddModelError("Error", " Your Account has been De-Active !");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }

            //string token = "Bearer " + JwtHelper.GenerateJwtToken(target);
            string token = JwtHelper.GenerateToken(target.UserId);
            return Ok(token);
        }
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(UserUpdateDTO user)
        {
            User toUpdateUser = _context.Users.SingleOrDefault(i => i.UserId == user.UserId);
            //if (toUpdateUser == null)
            //{
            //    ModelState.AddModelError("Error", "User Not Found");
            //    return BadRequest(new { ErrorCode = 400, Message = ModelState });
            //}
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(new { ErrorCode = 400, Message = ModelState });
            //}

            //toUpdateUser.UserId = user.UserId;
            toUpdateUser.FullName = user.FullName;
            toUpdateUser.Birthdate = user.Birthdate;
            //toUpdateUser.PhoneNumber = user.PhoneNumber;
            toUpdateUser.Email = user.Email;
            //toUpdateUser.Password = user.Password;
            toUpdateUser.Address = user.Address;
            toUpdateUser.IsActived = user.IsActived;

            _context.SaveChanges();
            return Ok(toUpdateUser);
        }
        [HttpPut("BanUser")]
        public IActionResult BanUser(int id)
        {
            User user = _context.Users.SingleOrDefault(i => i.UserId == id);
            user.IsActived = !user.IsActived;
            _context.SaveChanges();
            return Ok(user);
        }
        

        [ServiceFilter(typeof(MyAuthorizationFilter))]
        [HttpPut("UpdateProfile")]
        public IActionResult UpdateProfile(UpdateRequestUser user)
        {
            User target = new User();
            if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is int userId)
            {
               target = _context.Users.SingleOrDefault(i => i.UserId == userId);
            }
            else
            {
                return BadRequest("User not found");
            }

            User toUpdateUser = _context.Users.SingleOrDefault(i => i.UserId == target.UserId);
            if (toUpdateUser == null)
            {
                ModelState.AddModelError("Error", "User Not Found");
                return BadRequest(new { ErrorCode = 400, Message = ModelState });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorCode = 400, Message = ModelState });
            }

            //toUpdateUser.UserId = user.UserId;
            toUpdateUser.FullName = user.FullName;
            toUpdateUser.Birthdate = user.Birthdate;
            toUpdateUser.PhoneNumber = user.PhoneNumber;
            //toUpdateUser.Email = user.Email;
            //toUpdateUser.Password = user.Password;
            toUpdateUser.Address = user.Address;
            toUpdateUser.Avatar = user.Avatar;

            _context.SaveChanges();
            return Ok(toUpdateUser);
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {
            User toUpdateUser = new User();
            if (user == null)
            {
                ModelState.AddModelError("Error", "User is Null");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
            if (_context.Users.SingleOrDefault(i => i.Email.Equals(user.Email)) != null)
            {
                ModelState.AddModelError("Error", "Email is Already SingIn");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorCode = 400, Message = ModelState });
            }

            toUpdateUser.FullName = user.FullName;
            toUpdateUser.Birthdate = user.Birthdate;
            toUpdateUser.PhoneNumber = user.PhoneNumber;
            toUpdateUser.Email = user.Email;
            toUpdateUser.Password = user.Password;
            toUpdateUser.Address = user.Address;
            toUpdateUser.Avatar = user.Avatar;
            toUpdateUser.IsVerified = false;
            toUpdateUser.Roll = user.Roll;
            _context.Users.Add(toUpdateUser);
            _context.SaveChanges();

            // Tạo JWT Token
            //string token = "Bearer " + JwtHelper.GenerateJwtToken(toUpdateUser);
            string token = JwtHelper.GenerateToken(toUpdateUser.UserId);
            return Ok(token);
        }

        [ServiceFilter(typeof(MyAuthorizationFilter))]
        [HttpPut("UpdateUserPassword")]
        public IActionResult UpdateUserPassword([FromBody] UpdatePasswordRequest request)
        {
            User target = new User();
            if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is int userId)
            {
                target = _context.Users.SingleOrDefault(i => i.UserId == userId);
            }
            else
            {
                return BadRequest("User not found");
            }

            if (request == null || string.IsNullOrEmpty(request.currentPassword) || string.IsNullOrEmpty(request.confirmPassword) || string.IsNullOrEmpty(request.newPassword))
            {
                ModelState.AddModelError("Error", "Form PassWord  incorrect format!");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
            var userToUpdate = _context.Users.SingleOrDefault(i => i.UserId == target.UserId);
            if (userToUpdate != null)
            {
                if (!request.newPassword.Equals(request.confirmPassword))
                {
                    ModelState.AddModelError("Error", "Confirm Password Not Match !");
                    var errorResponse = new ErrorResponse
                    {
                        ErrorCode = 400,
                        Errors = ModelState.ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                    };
                    return BadRequest(errorResponse);
                }
                if (!request.currentPassword.Equals(userToUpdate.Password))
                {
                    ModelState.AddModelError("Error", "Current PassWord Not Match !");
                    var errorResponse = new ErrorResponse
                    {
                        ErrorCode = 400,
                        Errors = ModelState.ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                    };
                    return BadRequest(errorResponse);
                }
                userToUpdate.Password = request.newPassword;
                _context.SaveChanges();
                return Ok(userToUpdate);
            }
            else
            {
                ModelState.AddModelError("Error", "Not Found UserId !");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpGet("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            // Kiểm tra xem có user nào có email này không
            var user = _context.Users.SingleOrDefault(u => u.Email == email);

            if (user == null)
            {
                ModelState.AddModelError("Error", " Email is not Found !");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }

            try
            {
                // create link confirm
                string token = JwtHelper.GenerateJwtToken(user);
                string currentHost = $"{Request.Scheme}://{Request.Host}";
                var currentControllerName = ControllerContext.ActionDescriptor.ControllerName;
                string confirmMailLink = $"{currentHost}/api/{currentControllerName}/ConfirmMail?JWTstring={token}&email={user.Email}";
                SendEmail(email, "Password Reset", confirmMailLink);
                ModelState.AddModelError("Error", "New password sent to your email.");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 200,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return Ok(errorResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = 500, Message = $"Error: {ex.Message}" });
            }
        }
        [HttpGet("ConfirmMail")]
        public IActionResult ConfirmMail(string JWTstring, string email)
        {
            try
            {
                // Thực hiện kiểm tra tính hợp lệ của token
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("BACHSONGDUCHE160575"))
                };

                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(JWTstring, validationParameters, out SecurityToken validatedToken);
                string newPassword = Guid.NewGuid().ToString().Substring(0, 8);
                User target = _context.Users.SingleOrDefault(i => i.Email.Equals(email));
                target.Password = newPassword;
                _context.SaveChanges();
                var identity = (ClaimsIdentity)claimsPrincipal.Identity;
                identity.AddClaim(new Claim("used", "true"));
                return Redirect("http://localhost:5279/login?message=" + newPassword);
            }
            catch (Exception ex)
            {
                return BadRequest($"Token validation failed: {ex.Message}");
            }
        }

        [ServiceFilter(typeof(MyAuthorizationFilter))]
        [HttpPost("VerifiAccount")]
        public IActionResult VerifiAccount(string email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError("Error", "Email not found");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
            if (user.IsVerified==true)
            {
                ModelState.AddModelError("Error", "Email has already verified");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
            try
            {
                // create link confirm
                string token = JwtHelper.GenerateJwtToken(user);
                string currentHost = $"{Request.Scheme}://{Request.Host}";
                var currentControllerName = ControllerContext.ActionDescriptor.ControllerName;
                string confirmMailLink = $"{currentHost}/api/{currentControllerName}/ConfirmVerifieAccount?JWTstring={token}&email={user.Email}";
                SendEmail(email, "Account Verify", confirmMailLink);
                ModelState.AddModelError("Ok", "New password sent to your email.");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 200,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return Ok(errorResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = 500, Message = $"Error: {ex.Message}" });
            }
        }
        [HttpGet("ConfirmVerifieAccount")]
        public IActionResult ConfirmVerifieAccount(string JWTstring, string email)
        {
            try
            {
                // Thực hiện kiểm tra tính hợp lệ của token
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("BACHSONGDUCHE160575"))
                };

                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(JWTstring, validationParameters, out SecurityToken validatedToken);
                User target = _context.Users.SingleOrDefault(i => i.Email.Equals(email));
                target.IsVerified = true;
                _context.SaveChanges();
                return Redirect("http://localhost:5279/profile?message=" + "Actived");
            }
            catch (Exception ex)
            {
                return BadRequest($"Token validation failed: {ex.Message}");
            }
        }


        //demo API
        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            return Ok(_context.Users.ToList());
        }
        [HttpGet("GenerateJwtToken")]
        public IActionResult GenerateJwtToken()
        {
            var user = _context.Users.First();
            // Tạo JWT Token
            string token = "Bearer " + JwtHelper.GenerateJwtToken(user);
            return Ok(token);
        }
        [Authorize]
        [HttpGet("SecureApi")]
        public IActionResult SecureApi()
        {
            var Claim_userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            int userId = Int32.Parse(Claim_userId);
            return Ok(new { ErrorCode = 200, Message = "Secure API accessed successfully", Data = "Success --> USER ID:" + userId });
        }
        [HttpGet("users")]
        public IActionResult GetList()
        {
            List<User> records = _context.Users.ToList();

            // Tính toán tổng số lượng bản ghi
            int totalItemCount = _context.Users.Count();

            // Thêm header Content-Range vào phản hồi
            Response.Headers.Add("Content-Range", $"items 0-{records.Count()}/{totalItemCount}");

            // Trả về danh sách bản ghi với định dạng mới, bao gồm trường 'total'
            return Ok(records);
        }


        public class UpdatePasswordRequest
        {
            public int UserId { get; set; }
            public string currentPassword { get; set; }
            public string newPassword { get; set; }
            public string confirmPassword { get; set; }
        }
        private void SendEmail(string to, string subject, string body)
        {
            // Địa chỉ email và mật khẩu của bạn
            var fromAddress = new MailAddress("bachjavaweb@gmail.com", "BACHDUC_ASP_NET_CORE");
            var fromPassword = "whko tplo hzno wtqi";

            // Địa chỉ email của người nhận
            var toAddress = new MailAddress(to);

            // Cấu hình thông tin email
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com", // Đổi theo dịch vụ email của bạn
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            // Tạo message
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };

            // Gửi email
            smtp.Send(message);
        }
        public class ErrorResponse
        {
            public int ErrorCode { get; set; }
            public Dictionary<string, string[]> Errors { get; set; }
        }
        public class LoginRequestUser
        {
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            public string Password { get; set; }

        }
        public class UpdateRequestUser
        {
            [Required(ErrorMessage = "Full Name is required")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Birthdate is required")]
            public string Birthdate { get; set; }

            [Required(ErrorMessage = "Phone Number is required")]
            [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid Phone Number")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Address is required")]
            public string Address { get; set; }
            [Required(ErrorMessage = "Avatar is required")]
            public string Avatar { get; set; }
        }

    }

}
