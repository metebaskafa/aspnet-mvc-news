using App.Data;
using App.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.ViewComponents
{
    public class BreakingNews : ViewComponent
    {
        private readonly AppDbContext _context;

        public BreakingNews(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BreakingNewsModel> model1 = new List<BreakingNewsModel>();
            foreach (var item in _context.News.Where(x=>x.IsBreaking==true).Take(2).ToList())
            {
                var model = new BreakingNewsModel()
                {
                    News = item,
                    NewsImage = _context.Images.Where(x => x.NewsId == item.Id).FirstOrDefault()
                };
                model1.Add(model);

            }
            

            return View(model1);
        }
    }
}
