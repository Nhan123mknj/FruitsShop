using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FruitsShop.Areas.Admin.Models;
using FruitsShop.Models;
using FruitsShop.Utilities;
using Microsoft.Extensions.Logging;

namespace FruitsShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class LoginController : Controller
	{
		private readonly DataContext _context;
        private readonly ILogger<LoginController> _logger;

        public LoginController(DataContext context, ILogger<LoginController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(Account account)
		{
			if (account == null)
			{
				return NotFound();
			}
			string password = HashMD5.GetHash(account.Password);
			var check = _context.Accounts.Where(m => (m.Email == account.Email && m.Password == password)).FirstOrDefault();
            if (check == null)
			{
				Functions._Message = "Invalid Email or Password";
				return RedirectToAction("Index", "Login");
			}
			Functions._Message = string.Empty;
			Functions._AccountId = check.Account_Id;
			Functions._Username = string.IsNullOrEmpty(check.Username) ? string.Empty : check.Username;
            Functions._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
            return RedirectToAction("Index", "Home");
		}
	}
}