using App.Data;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.ViewComponents
{
    public class EditorialSlider : ViewComponent
    {
        private readonly AppDbContext _context;

        public EditorialSlider(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_context.News.Where(x => x.UserId == 2).Take(4).ToList()); 
        }
    }
}
