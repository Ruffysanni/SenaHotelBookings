using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SenaHotelBookings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        public RoomsController()
        {
            
        }

        [HttpGet]
        public IActionResult GetResult()
        {
            return Ok("Welcome...");
        }
    }
}
