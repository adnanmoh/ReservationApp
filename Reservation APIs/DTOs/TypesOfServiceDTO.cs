using System.ComponentModel.DataAnnotations;

namespace Reservation_APIs.DTOs
{
    public class TypesOfServiceDTO
    {
        public int ServiceTypeId { get; set; }
        public string Name { get; set; } = null!;
    }
}
