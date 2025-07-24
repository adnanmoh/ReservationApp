using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Reservation_APIs.DTOs
{
    public class ResortDTO
    {
        public int ResortId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Details { get; set; } = null!;
        public int NumberPeople { get; set; }
        public bool? IsApprovedAdd { get; set; }
        public decimal Price { get; set; }
        public bool? IsApprovedEdit { get; set; }
        public bool? IsActive { get; set; }
        public int? ResortTypeId { get; set; }
        public int? UserId { get; set; }
    }
}
