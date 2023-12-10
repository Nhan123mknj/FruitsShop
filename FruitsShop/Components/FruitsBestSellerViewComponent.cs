using FruitsShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitsShop.Components

{

	public class FruitsBestSellerViewComponent : ViewComponent
	{
		private readonly DataContext _Context;
		public FruitsBestSellerViewComponent(DataContext context)
		{
			_Context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var item = (from m in _Context.Fruits
						where (m.IsActive == true && m.IsBestSeller)
						select m).Take(3).ToList();
			return await Task.FromResult<IViewComponentResult>(View(item));
		}
	}
}
