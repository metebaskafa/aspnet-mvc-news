using App.Data;
using App.Data.Entity;
using App.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            List<NewsComment> comments = _context.Comments.ToList();


            // Her haberin yorum sayısını içeren bir liste oluşturuyoruz.
            var postCommentCounts = comments
                .GroupBy(c => c.PostId)
                .Select(g => new
                {
                    PostId = g.Key,
                    CommentCount = g.Count()
                })
                .ToList();

            // Yorum sayısına göre sıralıyoruz.
            var topTwoPosts = postCommentCounts
                .OrderByDescending(p => p.CommentCount)
                .Take(2)
                .ToList();

            // İlk iki haberi çekmek için postId'leri kullanabilirsiniz.
            List<int> topTwoPostIds = topTwoPosts.Select(p => p.PostId).ToList();
            List<int> topTwoCategoryIds = new List<int>();

            // topTwoPostIds listesi, en çok yorum alan ilk iki haberi içerecektir.

            // Ardından, bu postId'leri kullanarak haberleri göstermek için uygun şekilde işlem yapabilirsiniz.
            // Örnek olarak:
           List<NewsImage> images = new List<NewsImage>();
            List<News> todayNews = new List<News>();

            foreach (int postId in topTwoPostIds)
            {
                todayNews.Add(_context.News.Where(x => x.Id == postId).FirstOrDefault());
                images.Add(_context.Images.Where(x => x.NewsId== postId).FirstOrDefault());
                topTwoCategoryIds.Add(_context.CategoryNews.Where(x => x.NewsId == postId).FirstOrDefault().CategoryId);
            }
            List<int> ints = new List<int>();
            foreach (var item in topTwoPosts)
            {
                ints.Add(item.CommentCount);
            }
            List<Category> categories = new List<Category>();
            foreach (var item in topTwoCategoryIds)
            {
                categories.Add(_context.Categories.Where(x => x.Id == item).FirstOrDefault());
            }

            TodayNewsViewModel model = new() { News = todayNews, CommentCount = ints, Categories = categories, Images=images };

            return View(model);


        }
        public static void Main()
        {
            // NewsComment listesi oluşturuyoruz ve bazı örnek yorumlar ekliyoruz.
            
        }
    }
}
