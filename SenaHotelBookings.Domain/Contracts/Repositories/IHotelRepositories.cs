using SenaHotelBookings.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaHotelBookings.Domain.Contracts.Repositories
{
    public interface IHotelRepositories
    {
        Task<List<Hotel>> GetAllHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int id);
        Task<Hotel> CreateHotelAsync(Hotel hotel);
        Task<Hotel> UpdateHotelAsync(Hotel hotel);
        //Task<Hotel> UpdateHotelAsync(Hotel hotel, int id);
        Task<Hotel> DeleteHotelAsync(int id);
        Task<List<Room>> GetHotelRoomsByHotelIdAsync(int hotelId);
        Task<Room> GetHotelRoomByIdAsync(int hotelId, int roomId);
        Task<Room> CreateRoomAsync(int hotelId, Room room);
        Task<Room> UpdateRoomAsync( int hotelId, Room updateroom);
        Task<Room> DeleteHotelRoomAsync( int hotelId, int roomId);
    }
}
