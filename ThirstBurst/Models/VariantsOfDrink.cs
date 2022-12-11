using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThirstBurst.Models
{
    public class VariantsOfDrink
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Drinks")]
        public int DrinkId { get; set; }
        public virtual Drink Drinks { get; set; }
        [ForeignKey("Variantss")]
        public int VariantId { get; set; }
        public virtual Variants Variantss { get; set; }
    }
}
