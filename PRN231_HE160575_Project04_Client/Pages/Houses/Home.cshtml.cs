using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN231_HE160575_Project04_Client.ModelsV2;
namespace PRN231_HE160575_Project04_Client.Pages.Houses
{
    public class HomeModel : PageModel
    {

        [BindProperty]
        public HouseSearch SearchHouse { get; set; } = new HouseSearch();

        public ErrorResponseModel MessageResponse { get; set; }
        public List<House> houses = new List<House>();
        public List<Province> provinces = new List<Province>();
        public List<District> districts = new List<District>();
        public List<Ward> wards = new List<Ward>();


        public async Task<IActionResult> OnGet(string? message)
        {
            await GetAllProvincesAsync();
            await OnPostGetDistrictsAsync();
            await OnPostGetAllWardsAsync();
            await GetHouseAsync();
            return Page();

        }

        public async Task<IActionResult> OnPost(HouseSearch SearchHouse)
        {
            await GetAllProvincesAsync();
            await OnPostGetDistrictsAsync();
            await OnPostGetAllWardsAsync();
            //http://localhost:5059/api/House/GetAllHouses?$filter=Price ge 0 and Price le 100 and Area ge 0 and Area le 100 and DistrictCode eq 'your_district_code' and ProvinceCode eq 'your_province_code' and WardCode eq 'your_ward_code'
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = "http://localhost:5059/api/House/GetAllHouses";
            if (SearchHouse.MinPrice!=null && SearchHouse.MaxPrice != null&& SearchHouse.MinArea != null && SearchHouse.MaxArea != null)
            {
                link = $"http://localhost:5059/api/House/GetAllHouses?$filter=Price ge {SearchHouse.MinPrice} and Price le {SearchHouse.MaxPrice} and Area ge {SearchHouse.MinArea} and Area le {SearchHouse.MaxArea}";
            }
            if (SearchHouse.ProvinceCode != null)
            {
                link += $"and ProvinceCode eq '{SearchHouse.ProvinceCode}'";
            }
            if (SearchHouse.DistrictCode != null)
            {
                link += $"and DistrictCode eq '{SearchHouse.DistrictCode}'";
            }
            if (SearchHouse.WardCode != null)
            {
                link += $"and WardCode eq '{SearchHouse.WardCode}'";
            }
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
                            MessageResponse = new ErrorResponseModel();
                            MessageResponse.setSucessMessage($"Get Houses Success !");
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


        public partial class HouseSearch
        {
            public string Title { get; set; } = null!;
            public decimal Price { get; set; }
            public decimal MaxPrice { get; set; }
            public decimal MinPrice { get; set; }
            public int Area { get; set; }
            public int MaxArea { get; set; }
            public int MinArea { get; set; }
            public string Location { get; set; } = null!;
            public int DaysAgo { get; set; }
            public bool IsActive { get; set; }
            public int Type { get; set; }
            public int Quantity { get; set; }
            public string Description { get; set; } = null!;
            public int Rate { get; set; }
            public DateTime PublicDay { get; set; }
            public int UserId { get; set; }
            public string? DistrictCode { get; set; }
            public string? ProvinceCode { get; set; }
            public string? WardCode { get; set; }

        }



    }
}
