using FruitsShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FruitsShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Product_FruitsController : Controller
    {
        private readonly DataContext _context;
        public Product_FruitsController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var pdList = _context.Fruits.Include(f => f.CategoryProduct).OrderBy(m => m.Fruit_ID).ToList();
            return View(pdList);
        }
        public IActionResult Create()
        {
            var pdList = _context.Categories
                .Select(m => new SelectListItem
                {
                    Text = m.Name_Category,
                    Value = m.Categories_id.ToString()
                })
                .ToList();

            pdList.Insert(0, new SelectListItem
            {
                Text= "--[Chọn danh mục]--",
                Value = "0"
            });

            ViewBag.pdList = pdList;
            return View();
        }

        // POST: Xử lý việc tạo menu mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Fruits pd)
        {
            if (ModelState.IsValid)
            {
                // Lưu menu vào cơ sở dữ liệu (ví dụ: sử dụng Entity Framework)
                _context.Fruits.Add(pd);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Chuyển hướng sau khi thêm thành công
            }
            return View(pd);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var pd = _context.Fruits.Find(id);
            if (pd == null)
            {
                return NotFound();
            }
            return View(pd);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleProduct = _context.Fruits.Find(id);
            if (deleProduct == null)
            {
                return NotFound();
            }
            _context.Fruits.Remove(deleProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fruits == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.Fruits.FirstOrDefaultAsync(m => m.Fruit_ID == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
        }
        //Start Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var pd = _context.Fruits.Find(id);
            if (pd == null)
            {
                return NotFound();
            }
            var pdList = _context.Categories
                .Select(m => new SelectListItem
                {
                    Text = m.Name_Category,
                    Value = m.Categories_id.ToString()
                })
                .ToList();

            pdList.Insert(0, new SelectListItem
            {
                Text = "--[Chọn danh mục]--",
                Value = "0"
            });

            ViewBag.pdList = pdList;
            return View(pd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Fruits pd)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật dữ liệu menu trong cơ sở dữ liệu (sử dụng Entity Framework hoặc phương thức truy cập dữ liệu của bạn)
                _context.Fruits.Update(pd);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách menu
            }
            return View(pd); // Hiển thị biểu mẫu chỉnh sửa với lỗi kiểm tra
        }
    }
}
