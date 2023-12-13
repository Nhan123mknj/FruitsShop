using Microsoft.AspNetCore.Mvc;

namespace FruitsShop.Controllers
{
	public class About_UsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
