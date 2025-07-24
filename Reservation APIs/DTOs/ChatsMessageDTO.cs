using System.ComponentModel.DataAnnotations.Schema;

namespace Reservation_APIs.DTOs
{
    public class ChatsMessageDTO
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; } = null!;
        public bool? IsReaded { get; set; }
        public int? ChatId { get; set; }
    }
}
