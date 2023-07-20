using App.Data;
using App.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace App.Web.Mvc.ViewComponents
{
    public class Slider : ViewComponent
    {
        private readonly AppDbContext _context;
        public Slider(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_context.News.Take(5).ToList());
        }
    }
}
