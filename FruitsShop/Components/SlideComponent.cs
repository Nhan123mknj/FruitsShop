
using FruitsShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitsShop.Components

{
    [ViewComponent(Name ="Slide")]
    public class SlideComponent : ViewComponent
    {
        private readonly DataContext _Context;
        public SlideComponent(DataContext context)
        {
            _Context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofslide = (from p in _Context.Slides
							   where (p.Is_Active == true) 
							   orderby p.SlideId descending
                              select p).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofslide));
        }
    }
}