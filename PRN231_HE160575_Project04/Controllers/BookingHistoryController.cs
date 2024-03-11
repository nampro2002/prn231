using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using static PRN231_HE160575_Project04.Controllers.HouseController;
using PRN231_HE160575_Project04.ModelsV2;

namespace PRN231_HE160575_Project04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingHistoryController : ControllerBase
    {
        private readonly PRN231_ProjectContext _context;
        public BookingHistoryController(PRN231_ProjectContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllBookingHistories")]
        public IActionResult GetAllBookingHistories()
        {
            var options = new JsonSerializerOptions
            {
                // Sử dụng ReferenceHandler.Preserve để duy trì các tham chiếu
                ReferenceHandler = ReferenceHandler.Preserve,
                // Đặt mức độ sâu tối đa của đối tượng trong chuỗi JSON
                MaxDepth = 32 // Tùy chỉnh mức độ sâu theo nhu cầu của bạn
            };

            var data = _context.BookingHistories.Include(i => i.User)
                .Include(i => i.House).ToList();
            //string jsonString = JsonSerializer.Serialize(_context
            //    .BookingHistories
            //    .Include(i => i.User)
            //    .Include(i => i.House)
            //    .ToList(), options);
            return Ok(data);
        }
        [HttpPost]
        [Route("CalculatePrice")]
        public IActionResult CalculatePrice( RentalRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError("Error", "Invalid request");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
            if (request.StartDate >= request.EndDate)
            {
                ModelState.AddModelError("Error", "Invalid start or end date");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
            House house = _context.Houses.SingleOrDefault(i => i.HouseId == request.HouseId);
            if (house == null)
            {
                ModelState.AddModelError("Error", "Invalid house price");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
            int numberOfDays = (int)(request.EndDate - request.StartDate).TotalDays;

            // Tính số tiền
            decimal totalPrice = (house.Price / 30) * numberOfDays;

            return Ok(totalPrice);
        }

        [HttpPost("AddBookingHistory")]
        public IActionResult AddBookingHistory(RentalRequest request)
        {
            House house = _context.Houses.SingleOrDefault(i=>i.HouseId==request.HouseId);
            if (house == null)
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
            User user =  _context.Users.SingleOrDefault(i => i.UserId == request.UserId);
            if (user == null)
            {
                ModelState.AddModelError("Error", "User is Null");
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                return BadRequest(errorResponse);
            }
           
            if (request == null)
            {
                return BadRequest("Invalid request");
            }
            if (request.StartDate >= request.EndDate)
            {
                return BadRequest("Invalid start or end date");
            }
            if (house == null)
            {
                return BadRequest("Invalid house price");
            }
            int numberOfDays = (int)(request.EndDate - request.StartDate).TotalDays;
            int totalPrice = (int)((house.Price / 30) * numberOfDays);
            //if (user.Balance < (double)totalPrice)
            //{
            //    ModelState.AddModelError("Error", "User Balance is not enought");
            //    var errorResponse = new ErrorResponse
            //    {
            //        ErrorCode = 400,
            //        Errors = ModelState.ToDictionary(
            //            kvp => kvp.Key,
            //            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
            //    };
            //    return BadRequest(errorResponse);
            //}

            _context.BookingHistories.Add(new BookingHistory()
            {
                UserId = request.UserId,
                HouseId = request.HouseId,
                BookingDate = request.StartDate,
                Price = totalPrice.ToString(),
                ExpirationDate = request.EndDate
            }); 
            _context.SaveChanges();
            return Ok();
        }

        public class RentalRequest
        {
            public int UserId { get; set; }
            public int HouseId { get; set; }
            public double total {  get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }


    }
}
