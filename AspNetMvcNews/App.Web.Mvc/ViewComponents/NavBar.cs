using App.Data;
using App.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.ViewComponents
{
    public class NavBar : ViewComponent
    {
        private readonly AppDbContext _context;
        public NavBar(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new NavBarViewModel()
            {
                Today = _context.News.Where(x => x.CreatedAt == DateTime.Now).ToList(),
                Categories = _context.Categories.ToList(),
                Pages = _context.Pages.ToList()
            };
            return View(model);
        }
    }
}

