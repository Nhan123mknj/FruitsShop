using FruitsShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace FruitsShop.Components
{
	[ViewComponent(Name = "RecentPost")]
	public class RecentPostComponent : ViewComponent
	{
		private readonly DataContext _context;
		public RecentPostComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofPost = (from p in _context.Blogs
							  where (p.IsActive == true) && (p.Status == 1)
							  orderby p.Blog_ID descending
							  select p).Take(6).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofPost));
		}
	}
}
