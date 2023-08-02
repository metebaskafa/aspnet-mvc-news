using App.Data;
using App.Data.Entity;
using App.Web.Mvc.Models;
using App.Web.Mvc.Utils;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.Controllers
{
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;

        public NewsController(AppDbContext context)
        {
            _context = context;
        }
		[HttpGet]
		public IActionResult Search([FromQuery]string q)
        {
			List<int> Ids = (
				from CategoryNews in _context.CategoryNews
				where CategoryNews.Category.Name.Contains(q) || CategoryNews.News.Content.Contains(q) || CategoryNews.News.Title.Contains(q)
				select CategoryNews.NewsId
				).ToList();
            List<SearchViewModel> Results = new List<SearchViewModel>();
            List<News> news = new List<News>();
			foreach (int i in Ids)
			{
				if (news.Where(c => c.Id == i).FirstOrDefault() is null)
				{
					news.Add(_context.News.Where(c => c.Id == i).FirstOrDefault());
                    Results.Add(new SearchViewModel()
                    {
                        News = _context.News.Where(c => c.Id == i).FirstOrDefault(),
                        Category = _context.Categories.Find(_context.CategoryNews.Where(c => c.NewsId == i).FirstOrDefault().CategoryId)
                    });
                    
				}

			}
            
			return View(Results);
        }
        [HttpGet]
        public IActionResult Detail([FromRoute]int id,[FromRoute] string title)
        {
            News news = _context.News.Where(c=>c.Id == id).FirstOrDefault();
            if(title != UrlFriend.SeoName(news.Title))
            {
                return RedirectToAction("Detail", new {id = id, title=UrlFriend.SeoName(news.Title) });
            }
            return View(news);
        }
    }
}
