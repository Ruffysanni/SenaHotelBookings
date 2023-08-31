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
            return Ok(hotels);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotelById = await _context.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
            return Ok(hotelById);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDto hotel)
        {
            Hotel domainHotel = new Hotel();
            domainHotel.Name = hotel.Name;
            domainHotel.Address = hotel.Address;
            domainHotel.HotelDescription = hotel.HotelDescription;
            domainHotel.City = hotel.City;
            domainHotel.Country = hotel.Country;
            domainHotel.Stars = hotel.Stars;

            _context.Hotels.Add(domainHotel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId }, hotel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody]Hotel update, int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(x => x.HotelId == id);
            hotel.Name = update.Name;
            hotel.Address = update.Address;
            hotel.HotelDescription = update.HotelDescription;

            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync(true);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHote(int id)
        {
            var hotelToremove = await _context.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
            _context.Hotels.Remove(hotelToremove);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
