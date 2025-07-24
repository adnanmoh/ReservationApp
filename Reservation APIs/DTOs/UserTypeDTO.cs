using System.ComponentModel.DataAnnotations;

namespace Reservation_APIs.DTOs
{
    public class UserTypeDTO
    {
        public int UserTypeId { get; set; }
        public string Name { get; set; } = null!;
    }
}
