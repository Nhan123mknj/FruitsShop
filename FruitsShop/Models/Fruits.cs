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
		public string? Description { get; set; }
		public string? Price { get; set; }
		public int Category_id { get; set; }
		public string? Title { get; set; }
		public string? Detail { get; set; }
		public string? Image { get; set; }

	}
}
