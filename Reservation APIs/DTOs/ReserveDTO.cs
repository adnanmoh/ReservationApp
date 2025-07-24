using System.ComponentModel.DataAnnotations.Schema;

namespace Reservation_APIs.DTOs
{
    public class ReserveDTO
    {
        public int ReserveId { get; set; }
        public DateTime ReserveDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool? IsApproved { get; set; }
        public byte[]? ReceiptPhoto { get; set; }
        public bool? IsRejected { get; set; }
        public bool? IsReady { get; set; }
        public string? Reason { get; set; }
        public int? ResortId { get; set; }
        public int? AccountId { get; set; }
        public int? UserId { get; set; }
    }
}
