using SenaHotelBookings.Domain.Models;

namespace SenaHotelBookings.Api.DTOs
{
    public class ReservationPostPutDto
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Customer { get; set; }
    }
}
