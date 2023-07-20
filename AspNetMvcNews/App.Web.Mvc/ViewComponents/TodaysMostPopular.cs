using App.Data;
using App.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Mvc.ViewComponents
{
    public class TodaysMostPopular : ViewComponent
    {
        private readonly AppDbContext _context;

        public TodaysMostPopular(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ////var model = _context.News.Include("NewsComment").ToList();

            ////var comment = _context.Comments.ToList();

            //List<NewsComment> comments = _context.Comments.ToList();
            //var groupedPostIds = comments.GroupBy(c => c.PostId)
            //                         .Select(group => new { PostId = group.Key, Count = group.Count() })
            //                         .OrderByDescending(item => item.Count)
            //                         .Take(2)
            //                         .ToList();

            //List<News> haberler = new List<News>();
            //var 

            return View();


        }
    }
}
