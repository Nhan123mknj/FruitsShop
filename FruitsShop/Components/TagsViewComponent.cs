using FruitsShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace FruitsShop.Components
{
	[ViewComponent(Name = "Tags")]
	public class TagsViewComponent : ViewComponent
	{
		private readonly DataContext _context;
		public TagsViewComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listoftags = (from p in _context.Categories
							  where (p.Is_Active == true)
							  orderby p.Categories_id descending
							  select p).Take(6).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listoftags));
		}
	}
}
