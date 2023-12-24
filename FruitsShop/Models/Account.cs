using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitsShop.Models
{
	[Table("Account")]
	public class Account
	{
		[Key]
		public int Account_Id { get; set; }
		public string? Username { get; set; }
		public string Password { get; set; }
		public string? FullName { get; set; }
		public int? Phone { get; set; }
		public string? Email { get; set; }
		public bool? IsActive { get; set; }
	}
}
