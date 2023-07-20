using App.Data;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.ViewComponents
{
    public class LatestNewsMarquee : ViewComponent
    {
        private readonly AppDbContext _context;
        public LatestNewsMarquee(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(_context.News.OrderByDescending(x => x.CreatedAt).Take(4).ToList());
        }
    }
}
