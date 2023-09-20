using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SenaHotelBookings.Api.DTOs;
using SenaHotelBookings.Dal;
using SenaHotelBookings.Dal.Repositories;
using SenaHotelBookings.Domain.Contracts.Repositories;
using SenaHotelBookings.Domain.Models;
using System.Runtime.InteropServices;

namespace SenaHotelBookings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepositories _hotelRepositories;
        private readonly ILogger<HotelsController> _logger;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRepositories hotelRepositories, ILogger<HotelsController> logger, IMapper mapper)
        {
            _hotelRepositories = hotelRepositories;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelRepositories.GetAllHotelsAsync();
            var hotelsGet = _mapper.Map<List<HotelGetDto>>(hotels);
            return Ok(hotelsGet);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotelById = await _hotelRepositories.GetHotelByIdAsync(id);
            return Ok(hotelById);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDto hotel)
        {
            var domainHotel = _mapper.Map<Hotel>(hotel);
            await _hotelRepositories.CreateHotelAsync(domainHotel);
            var hotelGet = _mapper.Map<HotelGetDto>(domainHotel);

            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId }, hotel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelCreateDto update, int id)
        {
            //var hotel = _context.Hotels.FirstOrDefault(x => x.HotelId == id);
            var hotelUpdate = _mapper.Map<Hotel>(update);
            hotelUpdate.HotelId = id;

            await _hotelRepositories.UpdateHotelAsync(hotelUpdate);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHote(int id)
        {
            var hotelToremove = await _hotelRepositories.DeleteHotelAsync(id);
            if (hotelToremove == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        [HttpGet]
        [Route("{hotelId}/rooms")]
        public async Task<IActionResult> GetAllHotelRooms(int hotelId)
        {
            var rooms = await _hotelRepositories.GetHotelByIdAsync(hotelId);
            var mappedRooms = _mapper.Map<RoomGetDto>(rooms);
            return Ok(mappedRooms);
        }

        [HttpGet]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> GetHotelByRoomId(int hotelId, int roomId)
        {
            var room = await _hotelRepositories.GetHotelRoomByIdAsync(hotelId, roomId);
            var mappedRoom = _mapper.Map<RoomGetDto>(room);

            if(room == null)
            {
                return NotFound("Room not found!");
            }
            return Ok(mappedRoom);
        }

        [HttpPost]
        [Route("{hotelId}/rooms")]
        public async Task<IActionResult> AddHotelRoom(int hotelId, [FromBody]RoomPostPutDto newRoom)
        {
            var room = _mapper.Map<Room>(newRoom);

            //First approach
            //room.HotelId = hotelId;

            //_context.Rooms.Add(room);

            //Second Approach
            var hotel = await _hotelRepositories.CreateRoomAsync(hotelId, room);
            if (hotel == null)
            {
                return NotFound("Hotel not found");
            }
            var mappedRoom = _mapper.Map<RoomGetDto>(room);
            //return Ok(room);
            return CreatedAtAction(nameof(GetHotelByRoomId), new {hotelId = hotelId, roomId = mappedRoom.RoomId}, mappedRoom);
        }

        [HttpPut]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> UpdateHotelRoom(int hotelId, int roomId, [FromBody] RoomPostPutDto updatedRoom)
        {
            var mappedUpdatedRoom = _mapper.Map<Room>(updatedRoom);
            mappedUpdatedRoom.HotelId = hotelId;
            mappedUpdatedRoom.RoomId = roomId;

            await _hotelRepositories.UpdateRoomAsync(hotelId, mappedUpdatedRoom);

            return NoContent();
        }

        [HttpDelete]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> RemoveRoomFromHotelById(int hotelId, int roomId)
        {
            var room = await _hotelRepositories.DeleteHotelRoomAsync(hotelId, roomId);
            if (room == null)
            {
                return NotFound("Room to remove not found!");
            }
            return Ok();
        }
    }
}
