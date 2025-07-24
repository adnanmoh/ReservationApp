using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reservation_APIs.Models
{
    [Table("AppUser")]
    [Index("Phone", Name = "UQ__AppUser__5C7E359ED38AADDB", IsUnique = true)]
    [Index("Email", Name = "UQ__AppUser__A9D105349E9D4EEA", IsUnique = true)]
    public partial class AppUser
    {
        public AppUser()
        {
            ChatReceivers = new HashSet<Chat>();
            ChatSenders = new HashSet<Chat>();
            Reserves = new HashSet<Reserve>();
            Resorts = new HashSet<Resort>();
            ResortServices = new HashSet<ResortService>();
        }

        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(255)]
        public string? Address { get; set; }
        [StringLength(20)]
        public string Phone { get; set; } = null!;
        [StringLength(100)]
        public string Email { get; set; } = null!;
        [StringLength(100)]
        public string Password { get; set; } = null!;
        public bool? IsOnline { get; set; }
        public bool? IsApproved { get; set; }
        public byte[]? Photo { get; set; }
        [Column("UserTypeID")]
        public int? UserTypeId { get; set; }

        [ForeignKey("UserTypeId")]
        [InverseProperty("AppUsers")]
        public virtual UserType? UserType { get; set; }
        [InverseProperty("Receiver")]
        public virtual ICollection<Chat> ChatReceivers { get; set; }
        [InverseProperty("Sender")]
        public virtual ICollection<Chat> ChatSenders { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Reserve> Reserves { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Resort> Resorts { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<ResortService> ResortServices { get; set; }
    }
}
