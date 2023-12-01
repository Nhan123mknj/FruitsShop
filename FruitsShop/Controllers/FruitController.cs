using Microsoft.AspNetCore.Mvc;

namespace FruitsShop.Controllers
{
	public class FruitController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
