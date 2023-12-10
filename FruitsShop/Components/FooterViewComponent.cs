using FruitsShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace FruitsShop.Components

{
	[ViewComponent(Name = "Footer")]
	public class FooterViewComponent : ViewComponent
	{
		private DataContext _Context;
		public FooterViewComponent(DataContext context)
		{
			_Context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofmenu = (from m in _Context.Menu
							  where (m.IsActive == true) && (m.Position == 1)
							  select m).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofmenu));
		}
	}
}