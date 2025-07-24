using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reservation_APIs.Models
{
    public partial class Chat
    {
        public Chat()
        {
            ChatsMessages = new HashSet<ChatsMessage>();
        }

        [Key]
        [Column("ChatID")]
        public int ChatId { get; set; }
        public bool? IsDeletedFromSender { get; set; }
        public bool? IsDeletedFromReceiver { get; set; }
        [Column("ReceiverID")]
        public int? ReceiverId { get; set; }
        [Column("SenderID")]
        public int? SenderId { get; set; }

        [ForeignKey("ReceiverId")]
        [InverseProperty("ChatReceivers")]
        public virtual AppUser? Receiver { get; set; }
        [ForeignKey("SenderId")]
        [InverseProperty("ChatSenders")]
        public virtual AppUser? Sender { get; set; }
        [InverseProperty("Chat")]
        public virtual ICollection<ChatsMessage> ChatsMessages { get; set; }
    }
}
