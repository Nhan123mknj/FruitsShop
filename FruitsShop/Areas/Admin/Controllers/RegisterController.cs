using FruitsShop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FruitsShop.Models;
using System.Security.Cryptography;

namespace FruitsShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RegisterController : Controller
    {
        private readonly DataContext _context;

        public RegisterController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Account account)
        {
            if (account == null) { return NotFound(); }

            var check = _context.Accounts.Where(m => m.Email == account.Email).FirstOrDefault();
            if (check != null)
            {
                Functions._Message = "Trùng Email";
                return RedirectToAction("Index", "Register");
            }
            Functions._Message = string.Empty;
            account.Password = HashMD5.GetHash(account.Password);
            _context.Add(account);
            _context.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}