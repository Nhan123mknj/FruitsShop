using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitsShop.Models
{
	[Table("Fruits")]
	public class Fruits
	{
		[Key]
        public int Fruit_ID { get; set; }

        public string? NameFruit { get; set; }
        public string? Title { get; set; }

        public string? Link { get; set; }

        public int? Categories_id { get; set; }

        public string? Description { get; set; }

        public string? Detail { get; set; }

        public string? Image { get; set; }

        public int? Price { get; set; }

        public int? PriceSale { get; set; }

        public int? Quantity { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public bool IsNew { get; set; }

        public bool IsBestSeller { get; set; }


        public bool IsActive { get; set; }

        public int? Star { get; set; }


        [ForeignKey("Categories_id")]
        public virtual CategoryFruit? CategoryProduct { get; set; }
    }
}
