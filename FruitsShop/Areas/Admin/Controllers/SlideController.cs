using FruitsShop.Models;
using FruitsShop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FruitsShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
	{
        private readonly DataContext _context;
        public SlideController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var slList = _context.Slides.OrderBy(m => m.SlideId).ToList();
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            return View(slList);
        }
        public IActionResult Create()
        {
            var slList = (from m in _context.Slides
                          select new SelectListItem()
                          {
                              Text = m.Title,
                              Value = m.SlideId.ToString(),
                          }).ToList();
            slList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });
            ViewBag.mnList = slList;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slide sl)
        {
            if (ModelState.IsValid)
            {
                // Lưu menu vào cơ sở dữ liệu (ví dụ: sử dụng Entity Framework)
                _context.Slides.Add(sl);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Chuyển hướng sau khi thêm thành công
            }
            return View(sl);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Slides == null)
            {
                return NotFound();
            }

            var tbslide = await _context.Slides
                .FirstOrDefaultAsync(m => m.SlideId == id);
            if (tbslide == null)
            {
                return NotFound();
            }

            return View(tbslide);
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public bool DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Slides == null)
                {
                    return false;
                }
                var tbCategory = _context.Slides.Find(id);
                if (tbCategory != null)
                {
                    _context.Slides.Remove(tbCategory);
                }

                _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Slides == null)
            {
                return NotFound();
            }

            var tbslide = await _context.Slides.FirstOrDefaultAsync(m => m.SlideId == id);
            if (tbslide == null)
            {
                return NotFound();
            }

            return View(tbslide);
        }
        //Start Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var slide = _context.Slides.Find(id);
            if (slide == null)
            {
                return NotFound();
            }
            var slList = (from m in _context.Slides
                          select new SelectListItem()
                          {
                              Text = m.Title,
                              Value = m.SlideId.ToString(),
                          }).ToList();
            slList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.mnList = slList;
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật dữ liệu menu trong cơ sở dữ liệu (sử dụng Entity Framework hoặc phương thức truy cập dữ liệu của bạn)
                _context.Slides.Update(slide);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách menu
            }
            return View(slide); // Hiển thị biểu mẫu chỉnh sửa với lỗi kiểm tra
        }
    }
}
