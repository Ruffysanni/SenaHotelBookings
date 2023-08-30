using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaHotelBookings.Domain.Models;

namespace SenaHotelBookings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        public HotelsController()
        {

        }


        [HttpGet]
        public IActionResult GetAllHotels()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHotelById(int id)
        {
            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId }, hotel);
        }

        [HttpPut(nameof(GetHotelById))]
        [Route("{id}")]
        public IActionResult UpdateHote(int id)
        {
            return Ok(id);
        }

        [HttpDelete(nameof(GetHotelById))]
        [Route("{id}")]
        public IActionResult DeleteHote(int id)
        {
            return Ok();
        }
    }
}
