using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitsShop.Models
{
	[Table("Orders")]
	public class Orders
	{
		[Key]
		public int Orders_id { get; set; }
		public int Custormer_id { get; set; }
		public DateTime? Order_Date { get; set; }
		public int Total_amount { get; set; }
	}
}
