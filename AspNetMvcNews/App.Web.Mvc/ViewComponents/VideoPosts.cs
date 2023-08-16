using App.Data;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.ViewComponents
{
    public class VideoPosts : ViewComponent
    {
        private readonly AppDbContext _context;

        public VideoPosts(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_context.VideoNews.ToList());
        }
    }
}
