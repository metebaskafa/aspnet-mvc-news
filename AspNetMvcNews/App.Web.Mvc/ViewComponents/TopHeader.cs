using App.Data;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.ViewComponents
{
    public class TopHeader : ViewComponent
    {
        private readonly AppDbContext _context;
        public TopHeader(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_context.News.OrderByDescending(x => x.CreatedAt).Take(6).ToList());
        }
    }
}
