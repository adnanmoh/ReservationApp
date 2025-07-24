using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reservation_APIs.Models
{
    [Table("Reserve")]
    public partial class Reserve
    {
        [Key]
        [Column("ReserveID")]
        public int ReserveId { get; set; }
        [Column(TypeName = "date")]
        public DateTime ReserveDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime DepartureDate { get; set; }
        public bool? IsApproved { get; set; }
        public byte[]? ReceiptPhoto { get; set; }
        public bool? IsRejected { get; set; }
        public bool? IsReady { get; set; }
        public string? Reason { get; set; }
        [Column("ResortID")]
        public int? ResortId { get; set; }
        [Column("AccountID")]
        public int? AccountId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }

        [ForeignKey("AccountId")]
        [InverseProperty("Reserves")]
        public virtual FinancialAccount? Account { get; set; }
        [ForeignKey("ResortId")]
        [InverseProperty("Reserves")]
        public virtual Resort? Resort { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Reserves")]
        public virtual AppUser? User { get; set; }
    }
}
