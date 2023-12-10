
using FruitsShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitsShop.Components

{

	public class Product_MenuViewComponent : ViewComponent
	{
		private readonly DataContext _Context;
		public Product_MenuViewComponent(DataContext context)
		{
			_Context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var item = (from m in _Context.Fruits
							  where (m.IsActive == true && m.IsNew==true)
							  select m).Take(3).ToList();
			return await Task.FromResult<IViewComponentResult>(View(item));
		}
	}
}
