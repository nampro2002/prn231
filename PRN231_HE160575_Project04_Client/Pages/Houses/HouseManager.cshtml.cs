using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN231_HE160575_Project04_Client.ModelsV2;
using System.Data.Common;
using System.Reflection.Metadata;

namespace PRN231_HE160575_Project04_Client.Pages.Houses
{
    public class HouseManagerModel : PageModel
    {
        [BindProperty]
        public static House SearchHouse { get; set; } = new House();

        public ErrorResponseModel MessageResponse { get; set; }
        public static List<House> houses = new List<House>();
        public static List<Province> provinces = new List<Province>();
        public static List<District> districts = new List<District>();
        public static List<Ward> wards = new List<Ward>();

        [BindProperty]
        public House house { get; set; } = new House();

        public async Task<IActionResult> OnGet(string? message, int? code = 0)
        {
            if (!String.IsNullOrEmpty(message))
            {
                MessageResponse = new ErrorResponseModel();
                if (code == 1)
                {
                    MessageResponse.setSucessMessage(message);
                }
                else
                {
                    MessageResponse.setFailMessage(message);
                }
            }
            await GetAllProvincesAsync();
            await GetHouseAsync();
            
            return Page();

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

        public async Task<IActionResult> OnGetDeleteHouseAsync(int HouseId)
        {
            house.ProvinceCode = SearchHouse.ProvinceCode;
            house.DistrictCode = SearchHouse.DistrictCode;
            house.WardCode = SearchHouse.WardCode;
            string retrievedToken = Request.Cookies["token"] ?? "token";
            string link = $"http://localhost:5059/api/House/DeleteHouse?HouseId={HouseId}";
            using (HttpClient clien = new HttpClient())
            {
                clien.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(retrievedToken);
                using (HttpResponseMessage res = await clien.DeleteAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        if (res.IsSuccessStatusCode == true)
                        {
                            string data = await content.ReadAsStringAsync();
                            MessageResponse = new ErrorResponseModel();
                            MessageResponse.setSucessMessage($"Delete House Sucess");
                            return RedirectToPage("/Houses/HouseManager", new { message = "Delete House Sucess", code = 1 });
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

        public async Task<IActionResult> OnGetSetUpdateHouseAsync(int HouseId)
        {
            return RedirectToPage("/Houses/HouseUpdateForm", new { HouseId = HouseId });
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
            await OnGet(null, null);
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
            await OnGet(null, null);
        }
        public async Task OnPostSetWardAsync(string SelectedWardCode)
        {
            SearchHouse.WardCode = SelectedWardCode;
            await OnPostGetDistrictsAsync(SearchHouse.ProvinceCode);
            await OnPostGetAllWardsAsync(SearchHouse.DistrictCode);
            await OnGet(null, null);
        }



    }
}
