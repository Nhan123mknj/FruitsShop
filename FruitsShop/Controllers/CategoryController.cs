using FruitsShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace FruitsShop.Controllers
{
	public class CategoryController : Controller
	{
		private readonly DataContext _context;

		public CategoryController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index( int maloai)
		{
			List<CategoryFruit> LST = _context.Categories.Where(x=>x.Categories_id==maloai).ToList();
			return View(LST);
		}
	}
}
