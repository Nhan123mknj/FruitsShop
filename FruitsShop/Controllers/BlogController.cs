using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FruitsShop.Models;
using X.PagedList;

namespace FruitsShop.Controllers
{
	public class BlogController : Controller
	{
		private readonly DataContext _context;

		public BlogController(DataContext context)
		{
			_context = context;
		}

		public IActionResult Index(int? page)
		{
			int pageSize = 6;
			int pageNumber=page==null||page<0?1:page.Value;
			var items = _context.Blogs.Where(m => (bool)m.IsActive).OrderByDescending(i => i.Blog_ID).AsNoTracking();
			ViewBag.blogComment = _context.BlogComments.Where(m => (bool)m.IsActive).ToList();
			PagedList<Blog>lst=new PagedList<Blog>(items,pageNumber,pageSize);
			return View(lst);
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
		[HttpPost]
		public bool Create(int? id,string name,string email, string message)
		{
			try
			{
				BlogComment comment = new BlogComment();
				comment.Blog_Id= id;
				comment.Name = name;
				comment.Email = email;
				comment.Detail = message;
				comment.CreatedDate = DateTime.Now;
				_context.Add(comment);
				_context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}