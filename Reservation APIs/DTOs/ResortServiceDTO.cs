namespace Reservation_APIs.DTOs
{
    public class ResortServiceDTO
    {
        public int ServiceId { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public int? ServiceTypeId { get; set; }
    }
}
