using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitsShop.Models
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        public int Menu_Id { get; set; }
        public string? MenuName { get; set; }

        public bool? IsActive { get; set; }
        public string? Alias { get; set; }
        public string? Description { get; set; }
        public int MenuOrder { get; set; }
        public int Levels { get; set; }
        public int ParentId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Position { get; set; }




    }
}