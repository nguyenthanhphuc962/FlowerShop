using System.ComponentModel.DataAnnotations;

namespace FlowersShop.Models
{
    public class ProductMeteData
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Photo { get; set; }
        public bool Status { get; set; }
        public bool Specials { get; set; }
        public string Description { get; set; }
    }

    [MetadataType(typeof(ProductMeteData))]
    public partial class Product
    {

    }
}