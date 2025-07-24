using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reservation_APIs.Models
{
    public partial class ResortsPhoto
    {
        [Key]
        [Column("PhotoID")]
        public int PhotoId { get; set; }
        public byte[] Photo { get; set; } = null!;
        public bool? IsMain { get; set; }
        [Column("ResortID")]
        public int? ResortId { get; set; }

        [ForeignKey("ResortId")]
        [InverseProperty("ResortsPhotos")]
        public virtual Resort? Resort { get; set; }
    }
}
