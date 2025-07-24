using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Reservation_APIs.DTOs
{
    public class ResortAndServiceDTO
    {
        public int ResortId { get; set; }
        public int ServiceId { get; set; }
    }
}
