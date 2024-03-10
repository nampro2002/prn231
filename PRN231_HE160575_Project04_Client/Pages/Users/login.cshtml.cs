using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN231_HE160575_Project04_Client.Models;

namespace PRN231_HE160575_Project04_Client.Pages.Users
{
    public class loginModel : PageModel
    {
        public ErrorResponse MessageResponse { get; set; }

        [BindProperty]
        public User user { get; set; } = new User();
        public async Task<IActionResult> OnGet(string? message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                MessageResponse = new ErrorResponse();
                MessageResponse.setSucessMessage($"Message {message}");
            }
            return Page();  
        }

        public async Task<IActionResult> OnPost()
        {
            return Content("OnPost Normal");
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = "http://localhost:5059/api/User/Login";
            using (HttpClient clien = new HttpClient())
            {
                clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
                using (HttpResponseMessage res = await clien.PostAsJsonAsync(link, user))
                {
                    using (HttpContent content = res.Content)
                    {
                        if (res.IsSuccessStatusCode == true)
                        {
                            string token = await content.ReadAsStringAsync();
                            Response.Cookies.Append("token", token, new CookieOptions
                            {
                                Expires = DateTime.Now.AddHours(1),
                            });
                            MessageResponse = new ErrorResponse();
                            MessageResponse.setSucessMessage($"Login Sucess");
                        
                            return Page();
                        }
                        else
                        {
                            string data = await content.ReadAsStringAsync();
                            ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(data);
                            MessageResponse = errorResponse;
                            return Page();
                        }
                    }
                }
            }
        }

        public async Task<IActionResult> OnPostForgetPasswordAsync(string email)
        {
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = $"http://localhost:5059/api/User/ForgotPassword?email={email}";
            using (HttpClient clien = new HttpClient())
            {
                clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
                using (HttpResponseMessage res = await clien.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        if (res.IsSuccessStatusCode == true)
                        {
                            string data = await content.ReadAsStringAsync();
                            MessageResponse = new ErrorResponse();
                            MessageResponse.setSucessMessage($"New password sent to your email !");
                            return Page();
                        }
                        else
                        {
                            string data = await content.ReadAsStringAsync();
                            MessageResponse = new ErrorResponse();
                            MessageResponse.setFailMessage($"Không thể gửi password mới tới email bạn cung cấp !");
                            return Page();
                        }
                    }
                }
            }
        }



    }
}
