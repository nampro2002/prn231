using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN231_HE160575_Project04_Client.Models;

namespace PRN231_HE160575_Project04_Client.Pages.Users
{
    public class registerModel : PageModel
    {
        public ErrorResponse MessageResponse { get; set; }

        [BindProperty]
        public User UserInfo { get; set; } = new User();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostRegisterAsync()
        {
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = "http://localhost:5059/api/User/AddUser";
            using (HttpClient clien = new HttpClient())
            {
                clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
                using (HttpResponseMessage res = await clien.PostAsJsonAsync(link, UserInfo))
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
                            MessageResponse.setSucessMessage($"Register Sucess");
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


    }
}
