using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reservation_APIs.Models
{
    public partial class UserType
    {
        public UserType()
        {
            AppUsers = new HashSet<AppUser>();
        }

        [Key]
        [Column("UserTypeID")]
        public int UserTypeId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("UserType")]
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
