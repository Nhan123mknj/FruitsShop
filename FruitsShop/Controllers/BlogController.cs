using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FruitsShop.Models;
using PagedList;

namespace FruitsShop.Controllers
{
	public class BlogController : Controller
	{
		private readonly DataContext _context;

		public BlogController(DataContext context)
		{
			_context = context;
		}

		public IActionResult Index(int? page,int? Pagesize)
		{
			if(page == null)
			{
				page = 1;
			}
			if(Pagesize == null)
			{
				Pagesize = 10;
			}
			var items = _context.Blogs.Where(m => (bool)m.IsActive).OrderByDescending(i => i.Blog_ID).ToList();
			ViewBag.blogComment = _context.BlogComments.Where(m => (bool)m.IsActive).ToList();
			return View(items.ToPagedList((int)page,(int)Pagesize));
		}

		[Route("/blog/{link}-{id}.html")]
		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var post = _context.Blogs.Where(i => i.Blog_ID == id && (bool)i.IsActive).FirstOrDefault();
			if (post == null)
			{
				return NotFound();
			}
			ViewBag.blogComment = _context.BlogComments.Where(i => i.Blog_Id == id && (bool)i.IsActive).OrderByDescending(i => i.Comment_Id).ToList();
			return View(post);
		}
	}
}