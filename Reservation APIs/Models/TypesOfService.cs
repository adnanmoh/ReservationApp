using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reservation_APIs.Models
{
    public partial class TypesOfService
    {
        public TypesOfService()
        {
            ResortServices = new HashSet<ResortService>();
        }

        [Key]
        [Column("ServiceTypeID")]
        public int ServiceTypeId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("ServiceType")]
        public virtual ICollection<ResortService> ResortServices { get; set; }
    }
}
