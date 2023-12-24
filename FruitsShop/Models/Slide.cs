using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FruitsShop.Models
{
    [Table("Slide")]
    public class Slide
    {
        [Key]
        public int SlideId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Images { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool Is_Active { get; set; }
		public int? OrderSlide { get; set; }
		public string? button1_text { get; set; }
		public string? button2_text { get; set; }
		public string? button1_link { get; set; }
        public string? button2_link { get;set; }
		public int? Status { get; set; }
        public string? Contents { get; set; }


	}
}