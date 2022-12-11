using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThirstBurst.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
        [ForeignKey("Drink_ordered")]
        public int Drink_Id { get; set; }
        public virtual Drink Drink_ordered { get; set; }

        [ForeignKey("User_")]
        public string User_Id { get; set; }
        public virtual IdentityUser User_ { get; set; }
    }
}
