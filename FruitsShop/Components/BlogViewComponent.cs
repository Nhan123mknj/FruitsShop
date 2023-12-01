
using FruitsShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitsShop.Components

{

	public class BlogViewComponent : ViewComponent
	{
		private readonly DataContext _Context;
		public BlogViewComponent(DataContext context)
		{
			_Context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var items = _Context.Blogs.Where(m => (bool)m.IsActive).OrderByDescending(i => i.Blog_ID).Take(3).ToList();
			ViewBag.blogComment = _Context.BlogComments.Where(m => (bool)m.IsActive).ToList();
			return await Task.FromResult<IViewComponentResult>(View(items));
		}
	}
}
