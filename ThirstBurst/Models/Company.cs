using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThirstBurst.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
    }
}
