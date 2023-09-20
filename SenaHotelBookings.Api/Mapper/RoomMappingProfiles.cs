using AutoMapper;
using SenaHotelBookings.Api.DTOs;
using SenaHotelBookings.Domain.Models;

namespace SenaHotelBookings.Api.Mapper
{
    public class RoomMappingProfiles : Profile
    {
        public RoomMappingProfiles()
        {
            CreateMap<Room, RoomGetDto>();
            CreateMap<RoomPostPutDto, Room>();
        }
    }
}
