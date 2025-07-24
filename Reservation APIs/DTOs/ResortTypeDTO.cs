using System.ComponentModel.DataAnnotations;

namespace Reservation_APIs.DTOs
{
    public class ResortTypeDTO
    {
        public int ResortTypeId { get; set; }
        public string Name { get; set; } = null!;
    }
}
