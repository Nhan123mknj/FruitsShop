using Microsoft.AspNetCore.Mvc;

namespace FruitsShop.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
