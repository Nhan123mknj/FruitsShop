using FruitsShop.Areas.Admin.Models;
using FruitsShop.Models;
using Microsoft.EntityFrameworkCore;


namespace FruitsShop.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Menu> Menu { get; set; }
		public DbSet<Blog> Blogs { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<view_Post_Menu> ViewPosts { get; set; }
		public DbSet<BlogComment>   BlogComments { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Fruits> Fruits { get; set; }
        public DbSet<CategoryFruit> Categories { get; set; }
        public DbSet<View_Category_Name> ViewCategories { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
    }
}