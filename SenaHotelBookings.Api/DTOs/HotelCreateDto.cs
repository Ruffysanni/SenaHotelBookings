using Microsoft.Build.Framework;
using SenaHotelBookings.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SenaHotelBookings.Api.DTOs
{
    public class HotelCreateDto
    {
        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(50)]
        [MinLength(2)]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [Range(1,5)]
        public int Stars { get; set; }
        [Microsoft.Build.Framework.Required]
        public string Address { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string City { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Country { get; set; }
        public string HotelDescription { get; set; }
    }
}
