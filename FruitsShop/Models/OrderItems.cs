using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitsShop.Models
{
	[Table("OrderItems")]
	public class OrderItems
	{
		[Key]
		public int Order_item_id { get; set; }
		public int Order_id { get; set; }
		public int Fruit_id { get;set; }
		public int? Quantity { get; set; }
		public int? Price { get; set; }
	}
}
