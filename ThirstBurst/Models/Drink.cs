using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThirstBurst.Models
{
    public class Drink
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image_url { get; set; }
        [ForeignKey("Drink_Company")]
        public int CompanyId { get; set; }
        public virtual Company Drink_Company { get; set; }
        public List<Variants> Variantss { get; set; }


    }
}
