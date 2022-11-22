using System.ComponentModel.DataAnnotations;

namespace ThirstBurst.Models
{
    public class Drink
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image_url { get; set; }
    
    }
}
