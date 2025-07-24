using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reservation_APIs.Models
{
    public partial class Resort
    {
        public Resort()
        {
            Reserves = new HashSet<Reserve>();
            ResortsPhotos = new HashSet<ResortsPhoto>();
            Services = new HashSet<ResortService>();
            ResortAndServices = new HashSet<ResortAndService>();
        }

        [Key]
        [Column("ResortID")]
        public int ResortId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(255)]
        public string Address { get; set; } = null!;
        public string Details { get; set; } = null!;
        public int NumberPeople { get; set; }
        public bool? IsApprovedAdd { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public bool? IsApprovedEdit { get; set; }
        public bool? IsActive { get; set; }
        [Column("ResortTypeID")]
        public int? ResortTypeId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }

        [ForeignKey("ResortTypeId")]
        [InverseProperty("Resorts")]
        public virtual ResortType? ResortType { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Resorts")]
        public virtual AppUser? User { get; set; }
        [InverseProperty("Resort")]
        public virtual ICollection<Reserve> Reserves { get; set; }
        [InverseProperty("Resort")]
        public virtual ICollection<ResortsPhoto> ResortsPhotos { get; set; }

        [ForeignKey("ResortId")]
        [InverseProperty("Resorts")]
        public virtual ICollection<ResortService> Services { get; set; }

        
        [InverseProperty(nameof(ResortAndService.Resorts))]
        public virtual ICollection<ResortAndService> ResortAndServices { get; set; }
    }
}
