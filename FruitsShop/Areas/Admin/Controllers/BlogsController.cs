using FruitsShop.Models;
using FruitsShop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FruitsShop.Areas.Admin.Controllers
{

        [Area("Admin")]
        public class BlogsController : Controller
        {

            private readonly DataContext _context;
            public BlogsController(DataContext context)
            {
                _context = context;
            }
        public IActionResult Index()
        {
            var mnList = _context.Blogs.OrderBy(m => m.Blog_ID).ToList();


            return View(mnList);
        }
            // GET: Hiển thị form để tạo menu mới
            public IActionResult Create()
            {
                var mnList = (from m in _context.Blogs
                              select new SelectListItem()
                              {
                                  Text = m.Title,
                                  Value = m.Blog_ID.ToString(),
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
            public IActionResult Create(Blog mn)
            {
                if (ModelState.IsValid)
                {
                    // Lưu menu vào cơ sở dữ liệu (ví dụ: sử dụng Entity Framework)
                    _context.Blogs.Add(mn);
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
                var mn = _context.Blogs.Find(id);
                if (mn == null)
                {
                    return NotFound();
                }
                return View(mn);
            }

            [HttpPost]
            public IActionResult Delete(int id)
            {
                var deleBlog = _context.Blogs.Find(id);
                if (deleBlog == null)
                {
                    return NotFound();
                }
                _context.Blogs.Remove(deleBlog);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Blogs == null)
                {
                    return NotFound();
                }

                var tbMenu = await _context.Blogs.FirstOrDefaultAsync(m => m.Blog_ID == id);
            if (tbMenu == null)
                {
                    return NotFound();
                }

                return View(tbMenu);
            }
            //Start Edit
            public IActionResult Edit(int? id)
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var mn = _context.Blogs.Find(id);
                if (mn == null)
                {
                    return NotFound();
                }
                var mnList = (from m in _context.Blogs
                              select new SelectListItem()
                              {
                                  Text = m.Title,
                                  Value = m.Blog_ID.ToString(),
                              }).ToList();
                mnList.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
                ViewBag.mnList = mnList;
                return View(mn);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(Blog mn)
            {
                if (ModelState.IsValid)
                {
                    // Cập nhật dữ liệu menu trong cơ sở dữ liệu (sử dụng Entity Framework hoặc phương thức truy cập dữ liệu của bạn)
                    _context.Blogs.Update(mn);
                    _context.SaveChanges();
                    return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách menu
                }
                return View(mn); // Hiển thị biểu mẫu chỉnh sửa với lỗi kiểm tra
            }

        //End Edit
        public IActionResult ToggleIsActive(int id)
        {
            var blog = _context.Blogs.Find(id);

            if (blog != null)
            {

               blog.IsActive = !blog.IsActive;


                _context.SaveChanges();


                return Json(true);
            }


            return Json(false);
        }
    }
    }
