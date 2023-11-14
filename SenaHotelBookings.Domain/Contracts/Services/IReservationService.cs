using SenaHotelBookings.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaHotelBookings.Domain.Contracts.Services
{
    public interface IReservationService
    {
        //Task<Reservation> MakeReservation(int hotelId, int roomId, DateTime checkIn, DateTime checkout, string customer);
        Task<Reservation> MakeReservationAsync(Reservation reservation);
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<Reservation> CancelReservationAsync(int id);
    }
}
