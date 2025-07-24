using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reservation_APIs.Models
{
    public partial class ChatsMessage
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }
        public string Message { get; set; } = null!;
        public bool? IsReaded { get; set; }
        [Column("ChatID")]
        public int? ChatId { get; set; }

        [ForeignKey("ChatId")]
        [InverseProperty("ChatsMessages")]
        public virtual Chat? Chat { get; set; }
    }
}
