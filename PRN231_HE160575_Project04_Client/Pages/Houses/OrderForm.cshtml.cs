using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN231_HE160575_Project04_Client.ModelsV2;
using System.Globalization;
using System.Runtime.CompilerServices;
using static PRN231_HE160575_Project04_Client.Pages.Houses.OrderFormModel;

namespace PRN231_HE160575_Project04_Client.Pages.Houses
{
    public class OrderFormModel : PageModel
    {
        
        public static RentalRequest rentalRequest { get; set; } = new RentalRequest();
        public static double total { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public ErrorResponseModel MessageResponse { get; set; }

        public void OnGet( DateTime? StartDate, DateTime? EndDate, int? id = -1)
        {
            startDate = rentalRequest.StartDate;
            endDate = rentalRequest.EndDate;
            rentalRequest.HouseId = (int)id;
        }

        public async Task<IActionResult> OnPost(int button, DateTime startDate, DateTime endDate)
        {

            if (button == 1)
            {
                rentalRequest.StartDate = startDate;
                rentalRequest.EndDate = endDate;
                rentalRequest.total = total;
                return await OnPostCalculatePriceAsync();
            }
            if (button == 2)
            {
                return await OnPostSubmit();
            }
          
            return Content(startDate.ToLongDateString());
            
        }

        public async Task<IActionResult> OnPostCalculatePriceAsync()
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
                            RedirectToPage("/Houses/HouseUpdateForm", new { id = rentalRequest.HouseId, startDate = rentalRequest.StartDate, endDate = rentalRequest.EndDate });
                            //OnGet( rentalRequest.StartDate, rentalRequest.EndDate, rentalRequest.HouseId);
                        }
                        else
                        {
                            string data = await content.ReadAsStringAsync();
                            ErrorResponseModel errorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(data);
                            MessageResponse = errorResponse;
                            return Page();
                        }
                    }
                }
                return Page();
            }
        }
        public async Task<IActionResult> OnPostSubmit()
        {
            string retrievedToken = Request.Cookies["token"] ?? "token";
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
                            MessageResponse.setSucessMessage($"Order success");
                            RedirectToPage("/Houses/Home");
                            //OnGet( rentalRequest.StartDate, rentalRequest.EndDate, rentalRequest.HouseId);
                        }
                        else
                        {
                            string data = await content.ReadAsStringAsync();
                            ErrorResponseModel errorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(data);
                            MessageResponse = errorResponse;
                            return Page();
                        }
                    }
                }
                return Page();
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
