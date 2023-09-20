using Microsoft.EntityFrameworkCore;
using SenaHotelBookings.Domain.Contracts.Repositories;
using SenaHotelBookings.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaHotelBookings.Dal.Repositories
{
    public class HotelRepositories : IHotelRepositories
    {
        private readonly AppDataContext _dataContext;

        public HotelRepositories(AppDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Hotel> CreateHotelAsync(Hotel hotel)
        {
            _dataContext.Hotels.Add(hotel);
            await _dataContext.SaveChangesAsync();
            return hotel;
        }

        public async Task<Room> CreateRoomAsync(int hotelId, Room room)
        {
            var hotel = await _dataContext.Hotels.Include(h => h.Rooms)
                    .FirstOrDefaultAsync(h => h.HotelId == hotelId);
            hotel.Rooms.Add(room);

            await _dataContext.SaveChangesAsync();
            return room;
        }

        public async Task<Hotel> DeleteHotelAsync(int id)
        {
            var hotelToremove = await _dataContext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
            if(hotelToremove == null) 
            {
                return null;
            };
            _dataContext.Hotels.Remove(hotelToremove);
            await _dataContext.SaveChangesAsync();
            return hotelToremove;
        }

        public async Task<Room> DeleteHotelRoomAsync(int hotelId, int roomId)
        {
            var room = await _dataContext.Rooms.FirstOrDefaultAsync(r => r.HotelId == hotelId && r.RoomId == roomId);
            if (room == null)
            {
                return null;
            }
            _dataContext.Rooms.Remove(room);
            await _dataContext.SaveChangesAsync();
            return room;
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            var hotels = await _dataContext.Hotels.ToListAsync();
            return hotels;
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            var hotelById = await _dataContext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
            if (hotelById == null)
            {
                return null;
            }
            return hotelById;
        }

        public async Task<Room> GetHotelRoomByIdAsync(int hotelId, int roomId)
        {
            var room = await _dataContext.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId && r.HotelId == hotelId);
            if (room == null)
            {
                return null;
            }
            return room;
        }

        public async Task<List<Room>> GetHotelRoomsByHotelIdAsync(int hotelId)
        {
            var rooms = await _dataContext.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();
            return rooms;
        }

        public async Task<Hotel> UpdateHotelAsync(Hotel hotel)
        {
            //hotel.HotelId = id;
            _dataContext.Hotels.Update(hotel);
            await _dataContext.SaveChangesAsync(true);
            return hotel;
        }

        public async Task<Room> UpdateRoomAsync(int hotelId, Room updateroom)
        {
            _dataContext.Rooms.Update(updateroom);
            await _dataContext.SaveChangesAsync();
            return updateroom;
        }
    }
}
