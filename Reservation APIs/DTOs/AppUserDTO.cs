namespace Reservation_APIs.DTOs
{
    public class AppUserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool? IsOnline { get; set; }
        public bool? IsApproved { get; set; }
        public byte[]? Photo { get; set; }
        public int? UserTypeId { get; set; }
    }
}
