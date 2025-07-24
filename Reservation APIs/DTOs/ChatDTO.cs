using System.ComponentModel.DataAnnotations.Schema;

namespace Reservation_APIs.DTOs
{
    public class ChatDTO
    {
        public int ChatId { get; set; }
        public bool? IsDeletedFromSender { get; set; }
        public bool? IsDeletedFromReceiver { get; set; }
        public int? ReceiverId { get; set; }
        public int? SenderId { get; set; }
    }
}
