using FruitsShop.Models;
using Microsoft.EntityFrameworkCore;


namespace FruitsShop.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Menu> Menu { get; set; }
		public DbSet<Post> Posts { get; set; }
        public DbSet<Slide> Slides { get; set; }
	}
}