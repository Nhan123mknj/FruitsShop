using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitsShop.Models
{
	[Table("Review")]
	public class Review
	{
		[Key]
		public int Review_id { get; set; }
		public int Customer_id { get; set; }
		public int Fruit_id { get; set; }
		public float Ratting { get; set; }
		public string? Comment { get; set; }
		public int Comment_id { get; set; }
	}
}
