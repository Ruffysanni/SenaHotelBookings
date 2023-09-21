using AutoMapper;
using SenaHotelBookings.Api.DTOs;
using SenaHotelBookings.Domain.Models;

namespace SenaHotelBookings.Api.Mapper
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<ReservationPostPutDto, Reservation>();
            CreateMap<Reservation, ReservationGetDto>();
        }
    }
}
