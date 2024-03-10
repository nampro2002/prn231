using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN231_HE160575_Project04_Client.ModelsV2;

namespace PRN231_HE160575_Project04_Client.Pages.Houses
{
    public class HouseUpdateFormModel : PageModel
    {
        [BindProperty]
        public static House SearchHouse { get; set; }

        public ErrorResponseModel MessageResponse { get; set; }
        public static List<House> houses = new List<House>();
        public static List<Province> provinces = new List<Province>();
        public static List<District> districts = new List<District>();
        public static List<Ward> wards = new List<Ward>();

        [BindProperty]
        public House house { get; set; } = new House();

        public async Task<IActionResult> OnGet(int? HouseId = -1)
        {
            await GetAllProvincesAsync();
            await GetHouseAsync();
            house = houses.SingleOrDefault(i => i.HouseId == HouseId);
            if (house == null)
            {
                MessageResponse = new ErrorResponseModel();
                MessageResponse.setFailMessage($"Not Found House To Update");
                return Page();
            }
            if (SearchHouse==null)
            {
                SearchHouse = house;
            }
            return Page();

        }

        public async Task<IActionResult> OnPostUpdateHouseAsync()
        {
            house.ProvinceCode = SearchHouse.ProvinceCode;
            house.DistrictCode = SearchHouse.DistrictCode;
            house.WardCode = SearchHouse.WardCode;
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
                            return RedirectToPage("/Houses/HouseManager", new { message = "Update House Sucess", code = 1 });
                        }
                        else
                        {
                            string data = await content.ReadAsStringAsync();
                            ErrorResponseModel errorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(data);
                            MessageResponse = errorResponse;
                            return RedirectToPage("/Houses/HouseManager", new { message = errorResponse.ToString(), code = -1 });
                        }
                    }
                }
            }
        }

        public async Task<IActionResult> OnPostAddHouseAsync()
        {
            house.ProvinceCode = SearchHouse.ProvinceCode;
            house.DistrictCode = SearchHouse.DistrictCode;
            house.WardCode = SearchHouse.WardCode;
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = "http://localhost:5059/api/House/AddHouse";
            using (HttpClient clien = new HttpClient())
            {
                clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
                using (HttpResponseMessage res = await clien.PostAsJsonAsync(link, house))
                {
                    using (HttpContent content = res.Content)
                    {
                        if (res.IsSuccessStatusCode == true)
                        {
                            string data = await content.ReadAsStringAsync();
                            MessageResponse = new ErrorResponseModel();
                            MessageResponse.setSucessMessage($"Add House Sucess");
                            return RedirectToPage("/Houses/HouseManager", new { message = "Add House Sucess", code = 1 });
                        }
                        else
                        {
                            string data = await content.ReadAsStringAsync();
                            ErrorResponseModel errorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(data);
                            MessageResponse = errorResponse;
                            return RedirectToPage("/Houses/HouseManager", new { message = errorResponse.ToString(), code = -1 });
                        }
                    }
                }
            }
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
            await OnGet(SearchHouse.HouseId);
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
            await OnGet(SearchHouse.HouseId);
        }
        public async Task OnPostSetWardAsync(string SelectedWardCode)
        {
            SearchHouse.WardCode = SelectedWardCode;
            await OnPostGetDistrictsAsync(SearchHouse.ProvinceCode);
            await OnPostGetAllWardsAsync(SearchHouse.DistrictCode);
            await OnGet(SearchHouse.HouseId);
        }



    }
}

