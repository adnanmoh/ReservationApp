using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reservation_APIs.Models
{
    public partial class ResortService
    {
        public ResortService()
        {
            Resorts = new HashSet<Resort>();
            ResortAndServices = new HashSet<ResortAndService>();
        }

        [Key]
        [Column("ServiceID")]
        public int ServiceId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [Column("ServiceTypeID")]
        public int? ServiceTypeId { get; set; }

        [Column("UserID")]
        public int UserId { get; set; }

        [ForeignKey("ServiceTypeId")]
        [InverseProperty("ResortServices")]
        public virtual TypesOfService? ServiceType { get; set; }

        [ForeignKey("ServiceId")]
        [InverseProperty("Services")]
        public virtual ICollection<Resort> Resorts { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("ResortServices")]
        public virtual AppUser? User { get; set; }

        [InverseProperty(nameof(ResortAndService.Services))]
        public virtual ICollection<ResortAndService> ResortAndServices { get; set; }
    }
}
