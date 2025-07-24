namespace Reservation_APIs.DTOs
{
    public class ReservationsdetailsDTO
    {
        public int ReserveId { get; set; }
        public DateTime ReserveDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsRejected { get; set; }
        public bool? IsReady { get; set; }
        public string? Reason { get; set; }
        public string? ResortName { get; set; }
        public int? UserId { get; set; }
        public int? ResortId { get; set; }
    }
}
