using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Reservation_APIs.Models
{
    public class ResortAndService
    {
        [Key]
        [Column("ResortID")]
        public int ResortId { get; set; }
        [Key]
        [Column("ServiceID")]
        public int ServiceId { get; set; }



        [ForeignKey(nameof(ResortId))]
        [InverseProperty(nameof(Resort.ResortAndServices))]
        public virtual Resort? Resorts { get; set; } = null!;


        [ForeignKey(nameof(ServiceId))]
        [InverseProperty(nameof(ResortService.ResortAndServices))]
        public virtual ResortService? Services { get; set; } = null!;
    }
}
