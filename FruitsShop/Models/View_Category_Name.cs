using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;


namespace FruitsShop.Models
{
	[Table("View_Category_Name")]
	public class View_Category_Name
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
		public bool? IsActive { get; set; }

		public string? Link { get; set; }
		public string? Name_Category { get; set; }
	}
}
