using SenaHotelBookings.Domain.Models;

namespace SenaHotelBookings.Api.DTOs
{
    public class HotelCreateDto
    {
        public string Name { get; set; }
        public int Stars { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string HotelDescription { get; set; }
    }
}
