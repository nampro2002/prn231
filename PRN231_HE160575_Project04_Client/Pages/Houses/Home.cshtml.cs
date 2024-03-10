using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN231_HE160575_Project04_Client.ModelsV2;
namespace PRN231_HE160575_Project04_Client.Pages.Houses
{
    public class HomeModel : PageModel
    {

        [BindProperty]
        public static House SearchHouse { get; set; } = new House();

        public ErrorResponseModel MessageResponse { get; set; }
        public List<House> houses = new List<House>();
        public  List<Province> provinces = new List<Province>();
        public  List<District> districts = new List<District>();
        public List<Ward> wards = new List<Ward>();

       
        public async Task<IActionResult> OnGet(string? message)
        {

            
            
            await GetAllProvincesAsync();
            await GetHouseAsync();
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

        public async Task OnPostGetDistrictsAsync(string selectedProvinceCode)
        {
            SearchHouse.ProvinceCode = selectedProvinceCode;
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = $"http://localhost:5059/api/House/GetAllDistricts?Province_code={SearchHouse.ProvinceCode}";
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
            await OnGet(null);
        }

        public async Task OnPostGetAllWardsAsync(string SelectedDistrictCode)
        {
            SearchHouse.DistrictCode = SelectedDistrictCode;
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = $"http://localhost:5059/api/House/GetAllWards?District_code={SelectedDistrictCode}";
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
            await OnPostGetDistrictsAsync(SearchHouse.ProvinceCode);
            await OnGet(null);
        }

        public async Task OnPostSetWardAsync(string SelectedWardCode)
        {
            SearchHouse.WardCode = SelectedWardCode;
            await OnPostGetDistrictsAsync(SearchHouse.ProvinceCode);
            await OnPostGetAllWardsAsync(SearchHouse.DistrictCode);
            await OnGet(null);
        }




    }
}
