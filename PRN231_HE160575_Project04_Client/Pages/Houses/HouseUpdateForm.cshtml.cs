using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN231_HE160575_Project04_Client.ModelsV2;

namespace PRN231_HE160575_Project04_Client.Pages.Houses
{
    public class HouseUpdateFormModel : PageModel
    {
        public ErrorResponseModel MessageResponse { get; set; }
        public  List<House> houses = new List<House>();
        public  List<Province> provinces = new List<Province>();
        public  List<District> districts = new List<District>();
        public  List<Ward> wards = new List<Ward>();

        [BindProperty]
        public House house { get; set; } = new House();

        public async Task<IActionResult> OnGet(int? HouseId = -1)
        {
            await GetAllProvincesAsync();
            await OnPostGetDistrictsAsync();
            await OnPostGetAllWardsAsync();
            await GetHouseAsync();
            house = houses.SingleOrDefault(i => i.HouseId == HouseId);
            if (house == null)
            {
                MessageResponse = new ErrorResponseModel();
                MessageResponse.setFailMessage($"Not Found House To Update");
                return Page();
            }
            return Page();

        }

        public async Task<IActionResult> OnPostUpdateHouseAsync(int? HouseId = -1)
        {
           
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = "http://localhost:5059/api/House/UpdateHouse";
            using (HttpClient clien = new HttpClient())
            {
                clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
                using (HttpResponseMessage res = await clien.PutAsJsonAsync(link, house))
                {
                    using (HttpContent content = res.Content)
                    {
                        if (res.IsSuccessStatusCode == true)
                        {
                            string data = await content.ReadAsStringAsync();
                            MessageResponse = new ErrorResponseModel();
                            MessageResponse.setSucessMessage($"Update House Sucess");
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

            await GetAllProvincesAsync();
            await OnPostGetDistrictsAsync();
            await OnPostGetAllWardsAsync();
            await GetHouseAsync();
            house = houses.SingleOrDefault(i => i.HouseId == HouseId);
            if (house == null)
            {
                MessageResponse = new ErrorResponseModel();
                MessageResponse.setFailMessage($"Not Found House To Update");
                return Page();
            }
            return Page();
        }

       


        public async Task GetHouseAsync()
        {
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = "http://localhost:5059/api/House/GetAllHouses";
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
                            houses = JsonConvert.DeserializeObject<List<House>>(data);
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
        }
        public async Task GetAllProvincesAsync()
        {
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = "http://localhost:5059/api/House/GetAllProvinces";
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
                            provinces = JsonConvert.DeserializeObject<List<Province>>(data);
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
        }
        public async Task OnPostGetDistrictsAsync()
        {
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = $"http://localhost:5059/api/House/GetAllDistricts";
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
                            districts = JsonConvert.DeserializeObject<List<District>>(data);
                        }
                    }
                }
            }
        }
        public async Task OnPostGetAllWardsAsync()
        {
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = $"http://localhost:5059/api/House/GetAllWards";
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
                            wards = JsonConvert.DeserializeObject<List<Ward>>(data);
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
        }

    }
}

