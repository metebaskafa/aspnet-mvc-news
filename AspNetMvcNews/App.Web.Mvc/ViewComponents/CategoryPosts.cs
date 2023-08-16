using App.Data;
using App.Data.Entity;
using App.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace App.Web.Mvc.ViewComponents
{
	public class CategoryPosts : ViewComponent
	{
		private readonly AppDbContext _context;

		public CategoryPosts(AppDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var model = new CategoryPostView()
			{
				NewsLeft=new List<News>(),
				NewsRight= new List<News>(),
				MiddleNews = new List<MiddleNews>()
			};
			var list = _context.CategoryNews.ToList();
			var categorynewsCounts = list
				.GroupBy(c => c.CategoryId)
				.Select(g => new
				{
					CategoryId = g.Key,
					NewsCount = g.Count()
				})
				.ToList();
			var toptwocat= categorynewsCounts.OrderByDescending(x=>x.NewsCount).Take(2).ToList();
			List<int> catids = toptwocat.Select(x=>x.CategoryId).ToList();
			model.CategoryLeft= _context.Categories.Where(x => x.Id == catids[0]).First();
			model.CategoryRight= _context.Categories.Where(x => x.Id == catids[1]).First();
			var AllIds= _context.News.Select(x=>x.Id).ToList();
			var LeftIds = _context.CategoryNews.Where(x=>x.CategoryId == catids[0]).Select(x=>x.NewsId).Distinct().ToList();
			var RightIds = _context.CategoryNews.Where(x=>x.CategoryId == catids[1]).Select(x=>x.NewsId).Distinct().ToList();
			foreach (var id in LeftIds)
			{
				var haber = _context.News.Where(x=>x.Id == id).FirstOrDefault();
				model.NewsLeft.Add(haber);
				if (id == LeftIds[0])
					model.ImageLeft = _context.Images.Where(x => x.NewsId == id).FirstOrDefault();
			}
			foreach (var id in RightIds)
			{
				var haber = _context.News.Where(x=>x.Id == id).FirstOrDefault();
				model.NewsRight.Add(haber);
				LeftIds.Add(id); 
				if (id == RightIds[0])
					model.ImageRight = _context.Images.Where(x => x.NewsId == id).FirstOrDefault();
			}
			foreach (var id in LeftIds)
			{
				AllIds.Remove(id);
			}
            foreach (var item in AllIds.Take(4))
            {
				var middlenews = new MiddleNews()
				{
					News = _context.News.Find(item),
					Category = _context.Categories.Where(x => x.Id == _context.CategoryNews.Where(y => y.NewsId == item).First().CategoryId).First(),
					NewsImage = _context.Images.Where(x => x.NewsId == item).FirstOrDefault()
				};
				model.MiddleNews.Add(middlenews);
            }
			model.NewsLeft=model.NewsLeft.OrderByDescending(x=>x.CreatedAt).Take(4).ToList();
			model.NewsRight=model.NewsRight.OrderBy(x=>x.CreatedAt).Take(4).ToList();
            return View(model);
		}
	}
}
