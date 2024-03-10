using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN231_HE160575_Project04_Client.Models;
using PRN231_HE160575_Project04_Client.ModelsV2;

namespace PRN231_HE160575_Project04_Client.Pages.Users
{
    public class userlistModel : PageModel
    {
		public ErrorResponse MessageResponse { get; set; }

		[BindProperty]
		public List<User> userList { get; set; } = new List<User>();


		public async Task<IActionResult> OnGet(string? message)
		{
			if (String.IsNullOrEmpty(message))
			{
				MessageResponse = new ErrorResponse();
				MessageResponse.setSucessMessage($"Message {message}");
			}
			return await GetAllAsync();
		}

		public async Task<IActionResult> GetAllAsync()
		{
			string retrievedToken = Request.Cookies["token"] ?? "token";
			string link = "http://localhost:5059/api/User/all";
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
							userList = JsonConvert.DeserializeObject<List<User>>(data);
							MessageResponse = new ErrorResponse();
							MessageResponse.setSucessMessage($"Get Profile Success !");
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

        public async Task<IActionResult> OnGetSetUpdateAsync(int UserId)
        {
            return RedirectToPage("/Users/edituser", new { UserId = UserId });
        }
        public async Task<IActionResult> OnPostBanUserAsync(int id)
        {        
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = $"http://localhost:5059/api/User/BanUser?id={id}";
            using (HttpClient clien = new HttpClient())
            {
                clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
                using (HttpResponseMessage res = await clien.PutAsJsonAsync(link, ""))
                {
                    using (HttpContent content = res.Content)
                    {
                        if (res.IsSuccessStatusCode == true)
                        {
                            string data = await content.ReadAsStringAsync();
                            MessageResponse = new ErrorResponse();
                            MessageResponse.setSucessMessage($"Update User Sucess");
                            return RedirectToPage("/Users/userlist");
                        }
                        else
                        {
                            string data = await content.ReadAsStringAsync();
                            ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(data);
                            MessageResponse = errorResponse;
                            return RedirectToPage("/Users/edituser", new { message = errorResponse.ToString(), code = -1 });
                        }
                    }
                }
            }
        }
        //public async Task<IActionResult> OnPostUpdateAsync()
        //{
        //	string retrievedToken = Request.Cookies["token"] ?? "token";
        //	string link = "http://localhost:5059/api/User/UpdateUser";
        //	using (HttpClient clien = new HttpClient())
        //	{
        //		clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
        //		using (HttpResponseMessage res = await clien.PutAsJsonAsync(link, user))
        //		{
        //			using (HttpContent content = res.Content)
        //			{
        //				if (res.IsSuccessStatusCode == true)
        //				{
        //					await GetProfileAsync();
        //					string data = await content.ReadAsStringAsync();
        //					MessageResponse = new ErrorResponse();
        //					MessageResponse.setSucessMessage($"Update Sucess");
        //					return Page();
        //				}
        //				else
        //				{
        //					await GetProfileAsync();
        //					string data = await content.ReadAsStringAsync();
        //					ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(data);
        //					MessageResponse = errorResponse;
        //					return Page();
        //				}
        //			}
        //		}
        //	}
        //}

        //public async Task<IActionResult> OnPostUpdatePasswordAsync()
        //{
        //	string retrievedToken = Request.Cookies["token"] ?? "token";
        //	string link = "http://localhost:5059/api/User/UpdateUserPassword";
        //	using (HttpClient clien = new HttpClient())
        //	{
        //		clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
        //		using (HttpResponseMessage res = await clien.PutAsJsonAsync(link, updatePasswordRequest))
        //		{
        //			using (HttpContent content = res.Content)
        //			{
        //				if (res.IsSuccessStatusCode == true)
        //				{
        //					await GetProfileAsync();
        //					string data = await content.ReadAsStringAsync();
        //					MessageResponse = new ErrorResponse();
        //					MessageResponse.setSucessMessage($"Update Password Sucess");
        //					return Page();
        //				}
        //				else
        //				{
        //					await GetProfileAsync();
        //					string data = await content.ReadAsStringAsync();
        //					ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(data);
        //					MessageResponse = errorResponse;
        //					return Page();
        //				}
        //			}
        //		}
        //	}
        //}

        //public async Task<IActionResult> OnPostVerifyAsync()
        //{
        //	string retrievedToken = Request.Cookies["token"] ?? "token";
        //	string link = $"http://localhost:5059/api/User/VerifiAccount?email={user.Email}";
        //	using (HttpClient clien = new HttpClient())
        //	{
        //		clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
        //		using (HttpResponseMessage res = await clien.PostAsJsonAsync(link, updatePasswordRequest))
        //		{
        //			using (HttpContent content = res.Content)
        //			{
        //				if (res.IsSuccessStatusCode == true)
        //				{
        //					await GetProfileAsync();
        //					string data = await content.ReadAsStringAsync();
        //					MessageResponse = new ErrorResponse();
        //					MessageResponse.setSucessMessage($"New password sent to your email");
        //					return Page();
        //				}
        //				else
        //				{
        //					await GetProfileAsync();
        //					string data = await content.ReadAsStringAsync();
        //					ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(data);
        //					MessageResponse = errorResponse;
        //					return Page();
        //				}
        //			}
        //		}
        //	}
        //}
        //public async Task<IActionResult> OnGetBookingHistory()
        //{
        //	string retrievedToken = Request.Cookies["token"] ?? "token";
        //	string link = $"http://localhost:5059/api/BookingHistory/GetAllBookingHistories";
        //	using (HttpClient clien = new HttpClient())
        //	{
        //		clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
        //		using (HttpResponseMessage res = await clien.GetAsync(link))
        //		{
        //			using (HttpContent content = res.Content)
        //			{
        //				if (res.IsSuccessStatusCode == true)
        //				{
        //					string data = await content.ReadAsStringAsync();
        //					bookingHistory = JsonConvert.DeserializeObject<List<BookingHistory>>(data);
        //					MessageResponse = new ErrorResponse();
        //					MessageResponse.setSucessMessage($"Get Booking History Success !");
        //					return Page();
        //				}
        //				else
        //				{
        //					string data = await content.ReadAsStringAsync();
        //					ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(data);
        //					MessageResponse = errorResponse;
        //					return Page();
        //				}
        //			}
        //		}
        //	}
        //}



        public class UpdatePasswordRequest
		{
			public int UserId { get; set; }
			public string currentPassword { get; set; }
			public string newPassword { get; set; }
			public string confirmPassword { get; set; }
		}

	}
}
