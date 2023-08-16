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
		public IActionResult Index([FromRoute] int id, [FromRoute] string title)
		{
			List<int> Ids = (
				from CategoryNews in _context.CategoryNews
				where CategoryNews.CategoryId == id
				select CategoryNews.NewsId
				).ToList();
			List<int> uniqueids = Ids.ToArray().Distinct().ToList();
			List<News> news = new List<News>();
			List<HomeNewsView> modelnews = new List<HomeNewsView>();
			foreach (int i in uniqueids)
			{

				var homeview = new HomeNewsView()
				{
					Category = _context.Categories.Find(id),
					News = _context.News.Where(x => x.Id == i).FirstOrDefault(),
					NewsImage = _context.Images.Where(x => x.NewsId == i).FirstOrDefault()
				};
				modelnews.Add(homeview);


			}
			var model = new CategoryViewModel()
			{
				Category = _context.Categories.Where(c => c.Id == id).FirstOrDefault(),
				News = modelnews
			};
			if (title != UrlFriend.SeoName(model.Category.Name))
			{
				return RedirectToAction("Index", new { id = id, title = UrlFriend.SeoName(model.Category.Name) });
			}
			return View(model);
		}
	}
}
