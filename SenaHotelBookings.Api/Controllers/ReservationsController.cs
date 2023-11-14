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
            var result = await _reservationService.MakeReservationAsync(reservation);
            
            if(result == null)
            {
                return BadRequest("Cannot make reservation");
            }
            var mappedReservation = _mapper.Map<ReservationGetDto>(result);
            return Ok(mappedReservation);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            var mappedReservations = _mapper.Map<List<ReservationGetDto>>(reservations);
            return Ok(mappedReservations);
        }

        [HttpGet]
        [Route("{reservationId}")]
        public async Task<IActionResult> GetReservationByIdAsync(int reservationId)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
            if(reservation == null)
            {
                return NotFound($"Reservation not found for the id: {reservationId}");
            }
            var mappedReservationById = _mapper.Map<ReservationGetDto>(reservation);
            return Ok(mappedReservationById);
        }

        [HttpDelete]
        [Route("{reservationId}")]
        public async Task<IActionResult>CancelReservation(int reservationId)
        {
            var cancelledReservation = await _reservationService.CancelReservationAsync(reservationId);
            if(cancelledReservation == null)
            {
                return NotFound("Id not found");
            }
            return NoContent();
        }
    }
}
