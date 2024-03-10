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
        
        public  RentalRequest rentalRequest { get; set; } = new RentalRequest();
        public  double total { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public ErrorResponseModel MessageResponse { get; set; }

        public void OnGet(int HouseId = -1, int? id = null, DateTime? startDate = null, DateTime? endDate = null,double? total = null)
        {
            rentalRequest.HouseId = HouseId;
            if (id != null && startDate != null && endDate != null)
            {
                this.rentalRequest.HouseId = (int)id;
                this.rentalRequest.StartDate = (DateTime)startDate;
                this.rentalRequest.EndDate = (DateTime)endDate;
                this.rentalRequest.total = (double)total;
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
            //if (button == 2)
            //{
            //    return await OnPostSubmit();
            //}


            return  RedirectToPage("/Houses/OrderForm", new {
              id = rentalRequest.HouseId, 
              startDate = rentalRequest.StartDate, 
              endDate = rentalRequest.EndDate ,
              total = total
          });
        }

        

        public async Task OnPostSubmit()
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
