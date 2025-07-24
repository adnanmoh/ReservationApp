using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reservation_APIs.Models
{
    public partial class FinancialAccount
    {
        public FinancialAccount()
        {
            Reserves = new HashSet<Reserve>();
        }

        [Key]
        [Column("AccountID")]
        public int AccountId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(100)]
        public string BankName { get; set; } = null!;
        [StringLength(10)]
        public string Currency { get; set; } = null!;
        [StringLength(100)]
        public string AccountNumber { get; set; } = null!;

        [InverseProperty("Account")]
        public virtual ICollection<Reserve> Reserves { get; set; }
    }
}
