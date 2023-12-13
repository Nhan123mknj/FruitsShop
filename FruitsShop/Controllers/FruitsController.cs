using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FruitsShop.Models;
using X.PagedList;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FruitsShop.Controllers
{
	public class FruitsController : Controller
	{
		private readonly DataContext _context;

		public FruitsController(DataContext context)
		{
			_context = context;
		}

		public IActionResult Index(int? page)
		{
			int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var items = _context.Fruits.Where(m => (bool)m.IsActive).OrderByDescending(i => i.Fruit_ID).AsNoTracking();
			PagedList<Fruits> lst = new PagedList<Fruits>(items, pageNumber, pageSize);
			return View(lst);
		}
		public ActionResult productcategory(int id, int? page)
		{
			int pageSize = 6;
			int pageNumber = page ?? 1;

			var fruitsInCategory = _context.Fruits
				.Where(f => f.Categories_id == id && f.IsActive==true)
				.OrderByDescending(i => i.Fruit_ID)
				.AsNoTracking();

			PagedList<Fruits> paginatedFruits = new PagedList<Fruits>(fruitsInCategory, pageNumber, pageSize);

			return View(paginatedFruits);
		}
		[Route("/Fruits/{Link}-{id}.html")]
		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var post = _context.Fruits.Where(i => i.Fruit_ID == id && (bool)i.IsActive).Include(f => f.CategoryProduct).FirstOrDefault();
			if (post == null)
			{
				return NotFound();
			}
			
			return View(post);
		}

		[Route("/addcart/{productid:int}", Name = "addcart")]
		public IActionResult AddToCart([FromRoute] int productid)
		{

			var product = _context.Fruits
				.Where(p => p.Fruit_ID == productid)
				.FirstOrDefault();
			if (product == null)
				return NotFound("Không có sản phẩm");

			// Xử lý đưa vào Cart ...
			var cart = GetCartItems();
			var cartitem = cart.Find(p => p.product.Fruit_ID == productid);
			if (cartitem != null)
			{
				// Đã tồn tại, tăng thêm 1
				cartitem.quantity++;
			}
			else
			{
				//  Thêm mới
				cart.Add(new CartItem() { quantity = 1, product = product });
			}

			// Lưu cart vào Session
			SaveCartSession(cart);
			// Chuyển đến trang hiện thị Cart
			//return RedirectToAction(nameof(Cart));
			return RedirectToAction("Index", "Home");
		}

		/// xóa item trong cart
		[Route("/removecart/{productid:int}", Name = "removecart")]
		public IActionResult RemoveCart([FromRoute] int productid)
		{
			var cart = GetCartItems();
			var cartitem = cart.Find(p => p.product.Fruit_ID == productid);
			if (cartitem != null)
			{
				// Đã tồn tại, tăng thêm 1
				cart.Remove(cartitem);
			}

			SaveCartSession(cart);
			return RedirectToAction(nameof(Cart));
		}

		/// Cập nhật
		[Route("/updatecart", Name = "updatecart")]
		[HttpPost]
		public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
		{
			// Cập nhật Cart thay đổi số lượng quantity ...
			var cart = GetCartItems();
			var cartitem = cart.Find(p => p.product.Fruit_ID == productid);
			if (cartitem != null)
			{
				// Đã tồn tại, tăng thêm 1
				cartitem.quantity = quantity;
			}
			SaveCartSession(cart);
			// Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
			return Ok();
		}


		// Hiện thị giỏ hàng
		[Route("Fruits/cart", Name = "cart")]
		public IActionResult Cart()
		{
			return View(GetCartItems());
		}

		[Route("/checkout")]
		public IActionResult CheckOut()
		{
			// Xử lý khi đặt hàng
			return View(GetCartItems());
		}
		public bool Order(string name, string phone, string address)
		{
			// Xử lý khi đặt hàng thành công
			try
			{
				var cart = GetCartItems();
				if (cart.IsNullOrEmpty())
				{
					return false;
				}
				int totalAmount = 0;
				foreach (var item in cart)
				{
					if (item.product.PriceSale == 0)
					{
						totalAmount += item.quantity * (int)item.product.Price;
					}
					else
					{
						totalAmount += item.quantity * (int)item.product.PriceSale;
					}
				}
				var order = new Orders();
				order.Customers.Name_customer = name;
				order.Customers.Phonen_number = phone;
				order.Customers.Address = address;
				order.Total_amount = totalAmount;
				order.Order_Date = DateTime.Now;
				_context.Orders.Add(order);
				_context.SaveChanges();
				int orderId = order.Orders_id;
				foreach (var item in cart)
				{
					var orderDetail = new OrderItems();
					orderDetail.Order_id = orderId;
					orderDetail.Fruit_id = item.product.Fruit_ID;
					orderDetail.Price = item.product.Price;
					orderDetail.Quantity = (int)item.quantity;
					_context.OrderItems.Add(orderDetail);
					_context.SaveChanges();
				}
				ClearCart();
				return true;
			}
			catch
			{
				return false;
			}
		}
		//------------------

		public const string CARTKEY = "cart";

		// Lấy cart từ Session (danh sách CartItem)
		List<CartItem> GetCartItems()
		{
			var session = HttpContext.Session;
			string jsoncart = session.GetString(CARTKEY);
			if (jsoncart != null)
			{
				return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
			}
			return new List<CartItem>();
		}

		// Xóa cart khỏi session
		void ClearCart()
		{
			var session = HttpContext.Session;
			session.Remove(CARTKEY);
		}

		// Lưu Cart (Danh sách CartItem) vào session
		void SaveCartSession(List<CartItem> ls)
		{
			var session = HttpContext.Session;
			string jsoncart = JsonConvert.SerializeObject(ls);
			session.SetString(CARTKEY, jsoncart);
		}
	}
}
