using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reservation_APIs.Models
{
    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        [Column("AdminID")]
        public int AdminId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(100)]
        public string UserName { get; set; } = null!;
        [StringLength(100)]
        public string Password { get; set; } = null!;
    }
}
