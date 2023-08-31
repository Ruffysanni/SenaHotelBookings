using AutoMapper;
using SenaHotelBookings.Api.DTOs;
using SenaHotelBookings.Domain.Models;

namespace SenaHotelBookings.Api.Mapper
{
    public class HotelMappingProfiles : Profile
    {
        public HotelMappingProfiles()
        {
            CreateMap<HotelCreateDto, Hotel>();
            CreateMap<Hotel, HotelGetDto>();
        }
    }
}
