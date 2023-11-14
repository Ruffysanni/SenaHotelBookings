using Microsoft.EntityFrameworkCore;
using SenaHotelBookings.Dal;
using SenaHotelBookings.Domain.Contracts.Repositories;
using SenaHotelBookings.Domain.Contracts.Services;
using SenaHotelBookings.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaHotelBookings.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IHotelRepositories _hotelRepo;
        private readonly AppDataContext _dataContext;

        public ReservationService(IHotelRepositories hotelRepo, AppDataContext dataContext)
        {
            _hotelRepo = hotelRepo;
            _dataContext = dataContext;
        }

        public async Task<Reservation> CancelReservationAsync(int id)
        {
            var reservation = await _dataContext.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);
            if(reservation != null)
            {
                _dataContext.Reservations.Remove(reservation);
            }
            await _dataContext.SaveChangesAsync();
            return reservation;
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await _dataContext.Reservations
                .Include(h => h.Hotel)
                .Include(r=> r.Room)
                .ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            var reservedById = await _dataContext.Reservations
                .Include(h => h.Hotel)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.ReservationId == id);
            return reservedById;
        }

        //public async Task<Reservation> MakeReservation(int hotelId, int roomId, DateTime checkIn, DateTime checkout, string customer)
        public async Task<Reservation> MakeReservationAsync(Reservation reservation)
        {
            // step 2: Get all the hotels including all the rooms
            var hotel = await _hotelRepo.GetHotelByIdAsync(reservation.HotelId);
            //step 3: FInd the specified room
            var room = hotel?.Rooms.Where(r => r.RoomId == reservation.RoomId).FirstOrDefault();


            if (hotel == null || room == null) return null;
            //Step 4: Check the availability of the room within the duration
            bool isBusy = await _dataContext.Reservations.AnyAsync(r =>
                (reservation.CheckInDate >= r.CheckInDate && reservation.CheckInDate <= r.CheckOutDate)
                && (reservation.CheckOutDate >= r.CheckInDate && reservation.CheckOutDate <= r.CheckOutDate)
                );

            if (isBusy)
            {
                return null;
            }

            if(room.NeedsRepair)
            {
                return null;
            }
            //step 5:
            room.BusyFrom = reservation.CheckInDate;
            room.BusyTo = reservation.CheckOutDate;
            //Persist all changes to database
            _dataContext.Rooms.Update(room);
            _dataContext.Reservations.Add(reservation);

            await _dataContext.SaveChangesAsync();
            return reservation;

        }
    }
}
