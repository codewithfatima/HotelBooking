

using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Api.Dtos.Room
{
    public class ResponseRoomDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string RoomNumber { get; set; } = string.Empty;

        [Range(1, 20)]
        public int Capacity { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomType { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Range(0.001, 100000)]
        public decimal Price { get; set; }

        [Url]
        [StringLength(500)]
        public string Image { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int HotelId { get; set; }


    }
}
