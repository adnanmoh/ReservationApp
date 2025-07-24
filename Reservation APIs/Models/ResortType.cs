using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reservation_APIs.Models
{
    public partial class ResortType
    {
        public ResortType()
        {
            Resorts = new HashSet<Resort>();
        }

        [Key]
        [Column("ResortTypeID")]
        public int ResortTypeId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("ResortType")]
        public virtual ICollection<Resort> Resorts { get; set; }
    }
}
