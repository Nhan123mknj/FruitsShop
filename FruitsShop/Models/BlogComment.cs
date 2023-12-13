using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitsShop.Models
{
	[Table("BlogComment")]
	public class BlogComment
	{
		[Key]
		public int Comment_Id { get; set; }
		public string? Name { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? Detail { get; set; }
		public int? Blog_Id { get;set; }
		public bool? IsActive { get; set; }
		public virtual Blog? Blog { get; set; }
	}
}
