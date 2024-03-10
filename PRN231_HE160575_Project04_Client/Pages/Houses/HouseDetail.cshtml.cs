using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN231_HE160575_Project04_Client.Models;
using PRN231_HE160575_Project04_Client.ModelsV2;

namespace PRN231_HE160575_Project04_Client.Pages.Houses
{
    public class HouseDetailModel : PageModel
    {
        public House HouseDetail = new House();
        public ErrorResponseModel MessageResponse { get; set; }
        public async Task<IActionResult> OnGet(int? id = 40)
        {
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = $"http://localhost:5059/api/House/getById?id={id}";
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
                            HouseDetail = JsonConvert.DeserializeObject<House>(data);
                            MessageResponse = new ErrorResponseModel();
                            MessageResponse.setSucessMessage($"Get Success !");
                            return Page();
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
            }
        }
    }
}
