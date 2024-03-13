using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PRN231_HE160575_Project04_Client.ModelsV2;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Text;
using static PRN231_HE160575_Project04_Client.Pages.Houses.OrderFormModel;

namespace PRN231_HE160575_Project04_Client.Pages.Houses
{
    public class OrderFormModel : PageModel
    {
        
        public  RentalRequest rentalRequest { get; set; } = new RentalRequest();
        public  double total { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public ErrorResponseModel MessageResponse { get; set; }

        public void OnGet(int HouseId, int? id = null, DateTime? startDate = null, DateTime? endDate = null,double? total = null)
        {
            rentalRequest.HouseId = HouseId;
            if (id != null && startDate != null && endDate != null)
            {
                rentalRequest.HouseId = (int)id;
                rentalRequest.StartDate = (DateTime)startDate;
                rentalRequest.EndDate = (DateTime)endDate;
                rentalRequest.total = (double)total;
            }
        }

        public async Task<IActionResult> OnPost(int button, RentalRequest rentalRequest)
        {

            if (button == 1)
            {
                string retrievedToken = Request.Cookies["token"] ?? "token";
                string link = "http://localhost:5059/api/BookingHistory/CalculatePrice";
                using (HttpClient clien = new HttpClient())
                {
                    clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
                    using (HttpResponseMessage res = await clien.PostAsJsonAsync(link, rentalRequest))
                    {
                        using (HttpContent content = res.Content)
                        {
                            if (res.IsSuccessStatusCode == true)
                            {
                                string data = await content.ReadAsStringAsync();
                                total = JsonConvert.DeserializeObject<double>(data);
                                MessageResponse = new ErrorResponseModel();
                                MessageResponse.setSucessMessage($"Get Total Rent Sucess");
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
            }
            if (button == 2)
            {
                 await OnPostSubmit(rentalRequest);
            }


            return  RedirectToPage("/Houses/OrderForm", new {
              id = rentalRequest.HouseId, 
              startDate = rentalRequest.StartDate, 
              endDate = rentalRequest.EndDate ,
              total = total
          });
        }

        public static int GetUserIdFromJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("BACHSONGDUCHE160575BACHSONGDUCHE160575");
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);

            return userId;
        }

        public async Task OnPostSubmit(RentalRequest rentalRequest)
        {
            string retrievedToken = Request.Cookies["token"] ?? "token";
            int id = GetUserIdFromJwtToken(retrievedToken);
            rentalRequest.UserId = id;
            string link = "http://localhost:5059/api/BookingHistory/AddBookingHistory";
            using (HttpClient clien = new HttpClient())
            {
                clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
                using (HttpResponseMessage res = await clien.PostAsJsonAsync(link, rentalRequest))
                {
                    using (HttpContent content = res.Content)
                    {
                        if (res.IsSuccessStatusCode == true)
                        {
                            MessageResponse = new ErrorResponseModel();
                            MessageResponse.setSucessMessage($"Add success");
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
        }
        public class RentalRequest
        {
            public int UserId { get; set; }
            public int HouseId { get; set; }
            public double total { get; set; }
            public DateTime StartDate { get; set; } = DateTime.Now;
            public DateTime EndDate { get; set; } = DateTime.Now;
        }
     
    }
}
