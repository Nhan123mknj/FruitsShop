using FruitsShop.Models;
using FruitsShop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FruitsShop.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ContactController : Controller
    {

        private readonly DataContext _context;
        public ContactController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var mnList = _context.Contacts.OrderBy(m => m.ContactId).ToList();
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            return View(mnList);

        }
        // GET: Hiển thị form để tạo menu mới
        public IActionResult Create()
        {
            var mnList = (from m in _context.Contacts
                          select new SelectListItem()
                          {
                              Text = m.Name,
                              Value = m.ContactId.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });
            ViewBag.mnList = mnList;
            return View();

        }

        // POST: Xử lý việc tạo menu mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact mn)
        {
            if (ModelState.IsValid)
            {
                // Lưu menu vào cơ sở dữ liệu (ví dụ: sử dụng Entity Framework)
                _context.Contacts.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Chuyển hướng sau khi thêm thành công
            }
            return View(mn);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Contacts.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleBlog = _context.Contacts.Find(id);
            if (deleBlog == null)
            {
                return NotFound();
            }
            _context.Contacts.Remove(deleBlog);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var tbMenu = await _context.Contacts.FirstOrDefaultAsync(m => m.ContactId == id);
            if (tbMenu == null)
            {
                return NotFound();
            }

            return View(tbMenu);
        }
        //Start Edit
        
        public IActionResult ToggleIsActive(int id)
        {
            var blog = _context.Contacts.Find(id);

            if (blog != null)
            {

                blog.Is_Read = !blog.Is_Read;


                _context.SaveChanges();


                return Json(true);
            }


            return Json(false);
        }
    }
}
