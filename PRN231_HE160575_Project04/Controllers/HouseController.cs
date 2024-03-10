using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_HE160575_Project04.ModelsV2;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.OData.Query;
using System.ComponentModel.DataAnnotations;

namespace PRN231_HE160575_Project04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {

        private readonly PRN231_ProjectContext _context;
        public HouseController(PRN231_ProjectContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllHouses")]
        [EnableQuery]
        public IActionResult GetAllHouses()
        {
            return Ok(_context
                .Houses
                .Include(i => i.ProvinceCodeNavigation)
                .Include(i => i.DistrictCodeNavigation)
                .Include(i => i.WardCodeNavigation)
                .Where(i => i.IsActive == true)
                .ToList());
        }
        [HttpGet("GetAllProvinces")]
        public IActionResult GetAllProvinces()
        {
            return Ok(_context.Provinces
                .Where(i => i.Code.Equals("01") || i.Code.Equals("04") || i.Code.Equals("06"))
                .ToList());
        }
        [HttpGet("GetAllDistricts")]
        public IActionResult GetDistrictbyID()
        {
            return Ok(_context.Districts
                .Where(i => i.ProvinceCode.Equals("01") || i.ProvinceCode.Equals("04") || i.ProvinceCode.Equals("06")).ToList());
            //.Where(i => i.ProvinceCode.Equals(Province_code)).ToList());
        }
        [HttpGet("GetAllWards")]
        public IActionResult GetAllWards()
        {
            //_context.Districts
            //    .Where(i => i.ProvinceCode.Equals("01") || i.ProvinceCode.Equals("04") || i.ProvinceCode.Equals("06")).ToList();
            //return Ok(_context.Wards.Where(i => i.DistrictCode.Equals(District_code)).ToList());

            var districts = _context.Districts
            .Where(i => i.ProvinceCode.Equals("01") || i.ProvinceCode.Equals("04") || i.ProvinceCode.Equals("06"))
            .ToList();
            // Tạo danh sách mã của các Districts đã lọc
            var districtCodes = districts.Select(d => d.Code).ToList();
            // Lấy danh sách các Wards dựa trên các Districts đã lọc
            var wards = _context.Wards
                .Include(w => w.DistrictCodeNavigation) // Include thông tin của Districts
                .Where(w => districtCodes.Contains(w.DistrictCode)) // Chỉ lấy các Wards có DistrictCode nằm trong danh sách đã lọc
                .ToList();

            return Ok(wards);
        }
       
        [HttpPost("AddHouse")]
        public IActionResult AddHouse(AddHouseRequest House)
        {
            House toUpdateHouse = new House();
            if (House == null)
            {
                ModelState.AddModelError("Error", "House is Null");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorCode = 400, Message = ModelState });
            }

            toUpdateHouse.Title = House.Title;
            toUpdateHouse.Image = House.Image;
            toUpdateHouse.Price = House.Price;
            toUpdateHouse.Area = House.Area;
            toUpdateHouse.Location = House.Location;
            toUpdateHouse.IsActive = true;
            toUpdateHouse.Type = House.Type;
            toUpdateHouse.Quantity = House.Quantity;
            toUpdateHouse.Description = House.Description;
            toUpdateHouse.Rate = House.Rate;
            toUpdateHouse.PublicDay = DateTime.Now;
            toUpdateHouse.UserId = 1;
            toUpdateHouse.DistrictCode = House.DistrictCode;
            toUpdateHouse.ProvinceCode = House.ProvinceCode;
            toUpdateHouse.WardCode = House.WardCode;
            _context.Houses.Add(toUpdateHouse);
            _context.SaveChanges();

            // Tạo JWT Token
            //string token = "Bearer " + JwtHelper.GenerateJwtToken(toUpdateHouse);
            string token = JwtHelper.GenerateToken(toUpdateHouse.HouseId);
            return Ok(token);
        }

        [HttpDelete("DeleteHouse")]
        public IActionResult DeleteHouse(int HouseId)
        {
            House toUpdateHouse = _context.Houses.SingleOrDefault(i => i.HouseId == HouseId);
            if (toUpdateHouse == null)
            {
                ModelState.AddModelError("Error", "House is not found !");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
            toUpdateHouse.IsActive = false;
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("UpdateHouse")]
        public IActionResult UpdateHouse(AddHouseRequest updatehouse)
        {
            House toUpdateHouse = _context.Houses.SingleOrDefault(i => i.HouseId == updatehouse.HouseId);
            if (toUpdateHouse == null)
            {
                ModelState.AddModelError("Error", "House is not found !");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorCode = 400, Message = ModelState });
            }
            toUpdateHouse.Title = updatehouse.Title;
            toUpdateHouse.Image = updatehouse.Image;
            toUpdateHouse.Price = updatehouse.Price;
            toUpdateHouse.Area = updatehouse.Area;
            toUpdateHouse.Location = updatehouse.Location;
            toUpdateHouse.IsActive = true;
            toUpdateHouse.Type = updatehouse.Type;
            toUpdateHouse.Quantity = updatehouse.Quantity;
            toUpdateHouse.Description = updatehouse.Description;
            toUpdateHouse.Rate = updatehouse.Rate;
            toUpdateHouse.PublicDay = DateTime.Now;
            toUpdateHouse.UserId = 1;
            toUpdateHouse.DistrictCode = updatehouse.DistrictCode;
            toUpdateHouse.ProvinceCode = updatehouse.ProvinceCode;
            toUpdateHouse.WardCode = updatehouse.WardCode;
            _context.SaveChanges();
            return Ok();
        }


        public class ErrorResponse
        {
            public int ErrorCode { get; set; }
            public Dictionary<string, string[]> Errors { get; set; }
        }

        public partial class AddHouseRequest
        {
            public int HouseId { get; set; }
            [Required]
            public string Title { get; set; } = null!;
            [Required]
            public string Image { get; set; } = null!;
            [Required]
            public decimal Price { get; set; }
            [Required]
            public int Area { get; set; }
            [Required]
            public string Location { get; set; } = null!;
            public int DaysAgo { get; set; }
            public bool IsActive { get; set; }
            [Required]
            public int Type { get; set; }
            [Required]
            public int Quantity { get; set; }
            [Required]
            public string Description { get; set; } = null!;
            public int Rate { get; set; }
            public DateTime PublicDay { get; set; }
            public int UserId { get; set; }
            [Required]
            public string DistrictCode { get; set; }
            [Required]
            public string ProvinceCode { get; set; }
            [Required]
            public string WardCode { get; set; }
        }

    }
}
