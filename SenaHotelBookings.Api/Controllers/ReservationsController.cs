using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaHotelBookings.Api.DTOs;
using SenaHotelBookings.Domain.Contracts.Services;
using SenaHotelBookings.Domain.Models;

namespace SenaHotelBookings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation([FromBody] ReservationPostPutDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            var result = await _reservationService.MakeReservation(reservation);
            if(result == null)
            {
                return BadRequest("Cannot make reservation.");
            }
            var mappedReservation = _mapper.Map<ReservationGetDto>(result);
            return Ok(mappedReservation);
        }
    }
}
