using App.Data;
using App.Data.Entity;
using App.Web.Mvc.Utils;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.Controllers
{
    public class PageController : Controller
    {
        private readonly AppDbContext _context;

        public PageController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Detail([FromRoute]int id, [FromRoute] string title)
        {
            Page model = _context.Pages.Find(id);
            if (title != UrlFriend.SeoName(model.Title))
            {
                return RedirectToAction("Detail", new { id = id, title = UrlFriend.SeoName(model.Title) });
            }
            return View(model);
        }
    }
}
