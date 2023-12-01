using FruitsShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FruitsShop.Areas.Admin.Controllers
{
             [Area("Admin")]
    public class MenuController : Controller
    {

            private readonly DataContext _context;
            public MenuController(DataContext context)
            {
                _context = context;
            }
            public IActionResult Index()
            {
            var mnList=_context.Menu.OrderBy(m=>m.Menu_Id).ToList();
            return View(mnList);
            }
        // GET: Hiển thị form để tạo menu mới
        public IActionResult Create()
        {
            var mnList = (from m in _context.Menu
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.Menu_Id.ToString(),
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
        public IActionResult Create(Menu mn)
        {
            if (ModelState.IsValid)
            {
                // Lưu menu vào cơ sở dữ liệu (ví dụ: sử dụng Entity Framework)
                _context.Menu.Add(mn);
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
            var mn = _context.Menu.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleMenu = _context.Menu.Find(id);
            if (deleMenu == null)
            {
                return NotFound();
            }
            _context.Menu.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var tbMenu = await _context.Menu.FirstOrDefaultAsync(m => m.Menu_Id == id);
            if (tbMenu == null)
            {
                return NotFound();
            }

            return View(tbMenu);
        }

    }
}
