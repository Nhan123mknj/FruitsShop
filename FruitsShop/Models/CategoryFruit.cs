using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitsShop.Models
{
	[Table("CategoryFruit")]
	public class CategoryFruit
	{
		[Key]
		public int? Categories_id { get; set; }
		public string? Name_Category { get; set; }
		public string? Title { get; set; }
		public bool Is_Active { get; set; }

		public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
        public List<Fruits> Products { get; set; }
    }
}
