using System.ComponentModel.DataAnnotations.Schema;

namespace Reservation_APIs.DTOs
{
    public class ResortsPhotoDTO
    {
        public int PhotoId { get; set; }
        public byte[] Photo { get; set; } = null!;
        public bool? IsMain { get; set; }
        public int? ResortId { get; set; }
    }
}
