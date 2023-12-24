using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitsShop.Models
{
	[Table("Customer")]
	public class Customer
	{
		[Key]
		public int Customer_id { get; set; }
		public string? Name_customer { get; set; }
		public string? Email { get; set; }
		public string? Address { get; set; }
		public string? Phone_number { get; set; }
		public bool? Is_Active { get; set; }
	}
}
