using Azure;
using FruitsShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace FruitsShop.Controllers
{

	public class HomeController : Controller
		{
			private readonly ILogger<HomeController> _logger;
		private readonly DataContext _context;

		public HomeController(ILogger<HomeController> logger, DataContext context)
			{
				_logger = logger;
				_context = context;
			}

			public IActionResult Index()
			{
				return View();
			}
		
			public IActionResult Privacy()
			{
				return View();
			}

			[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
			public IActionResult Error()
			{
				return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
			}
			public  IActionResult NewFruits(int? page)
		{
			int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var items = _context.Fruits.Where(m => (bool)m.IsActive && m.IsNew).OrderByDescending(i => i.Fruit_ID).AsNoTracking();
			PagedList<Fruits> lst = new PagedList<Fruits>(items, pageNumber, pageSize);
			return View(lst);
		}
		public IActionResult BestSeller(int? page)
		{
			int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var items = _context.Fruits.Where(m => (bool)m.IsActive && m.IsBestSeller).OrderByDescending(i => i.Fruit_ID).AsNoTracking();
			PagedList<Fruits> lst = new PagedList<Fruits>(items, pageNumber, pageSize);
			return View(lst);
		}
	}
	}