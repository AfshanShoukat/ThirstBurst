using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThirstBurst.Models
{
    public class Variants
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Variant_Name  { get; set; }
        [Required]
        public string V_Image { get; set; }
        [Required]
        public DateTime Release_Date { get; set; }
        public List<Drink> Drinks { get; set; }
        
    }
}
