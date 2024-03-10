using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN231_HE160575_Project04.ModelsV2.DTO;
using PRN231_HE160575_Project04_Client.Models;
using PRN231_HE160575_Project04_Client.ModelsV2;

namespace PRN231_HE160575_Project04_Client.Pages.Users
{
    public class edituserModel : PageModel
    {
        public ErrorResponseModel MessageResponse { get; set; }
        [BindProperty]
        public User User { get; set; }
   

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //await _userService.UpdateUserAsync(User);
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGet(int? UserId = -1)
        {
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = $"http://localhost:5059/api/User/getById?id={UserId}";
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
                            User = JsonConvert.DeserializeObject<User>(data);
                            //MessageResponse = new ErrorResponse();
                            //MessageResponse.setSucessMessage($"Get Houses Success !");
                        }
                        else
                        {
                            string data = await content.ReadAsStringAsync();
                            ErrorResponseModel errorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(data);
                            MessageResponse = errorResponse;
                        }
                    }
                }
            }
            return Page();

        }
        public async Task<IActionResult> OnPostUpdateUserAsync()
        {
            UserUpdateDTO u = new UserUpdateDTO()
            {
                 UserId = User.UserId,
                 FullName = User.FullName,
                 Birthdate = User.Birthdate,
        PhoneNumber  = User.PhoneNumber,
       Email = User.Email,
        Address = User.Address,
        IsActived = User.IsActived,
    };
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = "http://localhost:5059/api/User/UpdateUser";
            using (HttpClient clien = new HttpClient())
            {
                clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
                using (HttpResponseMessage res = await clien.PutAsJsonAsync(link, User))
                {
                    using (HttpContent content = res.Content)
                    {
                        if (res.IsSuccessStatusCode == true)
                        {
                            string data = await content.ReadAsStringAsync();
                            MessageResponse = new ErrorResponseModel();
                            MessageResponse.setSucessMessage($"Update User Sucess");
                            return RedirectToPage("/Users/edituser", new { UserId = User.UserId });
                        }
                        else
                        {
                            string data = await content.ReadAsStringAsync();
                            ErrorResponseModel errorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(data);
                            MessageResponse = errorResponse;
                            return RedirectToPage("/Users/edituser", new { message = errorResponse.ToString(), code = -1 });
                        }
                    }
                }
            }
        }
    }
}
