using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SenaHotelBookings.Api.DTOs;
using SenaHotelBookings.Dal;
using SenaHotelBookings.Domain.Models;

namespace SenaHotelBookings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly AppDataContext _context;
        private readonly ILogger<HotelsController> _logger;
        private readonly IMapper _mapper;

        public HotelsController(AppDataContext context, ILogger<HotelsController>logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            var hotelsGet = _mapper.Map<List<HotelGetDto>>(hotels);
            return Ok(hotelsGet);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotelById = await _context.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
            if(hotelById == null)
            {
                return NotFound();
            }
            var hotelGetById = _mapper.Map<HotelGetDto>(hotelById);
            return Ok(hotelById);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDto hotel)
        {
            var domainHotel = _mapper.Map<Hotel>(hotel);

            _context.Hotels.Add(domainHotel);

            var hotelGet = _mapper.Map<HotelGetDto>(domainHotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId }, hotel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody]HotelCreateDto update, int id)
        {
            //var hotel = _context.Hotels.FirstOrDefault(x => x.HotelId == id);
            var hotelUpdate = _mapper.Map<Hotel>(update);
            hotelUpdate.HotelId = id;
            
            _context.Hotels.Update(hotelUpdate);
            await _context.SaveChangesAsync(true);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHote(int id)
        {
            var hotelToremove = await _context.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
            if (hotelToremove == null)
            {
                return NotFound();
            }
            _context.Hotels.Remove(hotelToremove);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
