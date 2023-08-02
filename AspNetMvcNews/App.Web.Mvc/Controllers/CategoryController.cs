using App.Data;
using App.Data.Entity;
using App.Web.Mvc.Models;
using App.Web.Mvc.Utils;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index([FromRoute]int id, [FromRoute]string title)
        {
            List<int> Ids = (
                from CategoryNews in _context.CategoryNews
                where CategoryNews.CategoryId == id
                select CategoryNews.NewsId
                ).ToList();
            List<News> news = new List<News>();
            foreach (int i in Ids )
            {
                if (news.Where(c=>c.Id==i).FirstOrDefault() is null)
                {
					news.Add(_context.News.Where(c => c.Id == i).FirstOrDefault());
				}
                
            }
            var model = new CategoryViewModel()
            {
                Category= _context.Categories.Where(c=>c.Id==id).FirstOrDefault(),
                News=news
            };
            if (title != UrlFriend.SeoName(model.Category.Name))
            {
                return RedirectToAction("Index", new { id = id, title = UrlFriend.SeoName(model.Category.Name) });
            }
            return View(model);
        }
    }
}
