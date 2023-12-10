
using FruitsShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitsShop.Components

{

	public class CategoryViewComponent : ViewComponent
	{
		private readonly DataContext _Context;
		public CategoryViewComponent(DataContext context)
		{
			_Context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			// Assuming _Context.Categories is an asynchronous queryable DbSet
			var listofCategory = (from m in _Context.Categories
							  where (m.Is_Active == true) 
							  select m).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofCategory));
		}
	}
}
