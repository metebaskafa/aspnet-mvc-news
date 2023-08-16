using App.Data;
using App.Data.Entity;
using App.Web.Mvc.Models;
using App.Web.Mvc.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                        Category = _context.Categories.Find(_context.CategoryNews.Where(c => c.NewsId == i).FirstOrDefault().CategoryId),
                        NewsImage = _context.Images.Where(x=>x.NewsId==i).FirstOrDefault()
                    });
                    
				}

			}
            
			return View(Results);
        }
        [HttpGet]
        public IActionResult Detail([FromRoute]int id,[FromRoute] string title)
        {
            News news = _context.News.Where(c=>c.Id == id).FirstOrDefault();
            if (news == null)
            {
                return NotFound();
            }
            List<NewsCommentView> list = new List<NewsCommentView>();
            foreach (var item in _context.Comments.Where(x => x.PostId == news.Id && x.IsActive).OrderBy(x=>x.CreatedAt).ToList())
            {
                var comview = new NewsCommentView()
                {
                    Comment = item,
                    User = _context.Users.Where(x => x.Id == item.UserId).FirstOrDefault()
                };
                list.Add(comview);
            }
            var model = new NewsDetailViewModel()
            {
                News=news,
                NewsCategory = _context.Categories.Find(_context.CategoryNews.Where(x=>x.NewsId==news.Id).First().CategoryId),
                NewsImage=_context.Images.Where(x=>x.NewsId==news.Id).FirstOrDefault(),
                NewsComments=list,
                NewsComment=null
            };
            if(title != UrlFriend.SeoName(news.Title))
            {
                return RedirectToAction("Detail", new {id = id, title=UrlFriend.SeoName(news.Title) });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Detail(NewsDetailViewModel collection, string Name, string Email, string Message, [FromRoute] int id, [FromRoute] string title)
        {
            News news = _context.News.Where(c => c.Id == id).FirstOrDefault();
            if (title != UrlFriend.SeoName(news.Title))
            {
                return RedirectToAction("Detail", new { id = id, title = UrlFriend.SeoName(news.Title) });
            }
			List<NewsCommentView> list = new List<NewsCommentView>();
			foreach (var item in _context.Comments.Where(x => x.PostId == news.Id && x.IsActive).OrderBy(x => x.CreatedAt).ToList())
			{
				var comview = new NewsCommentView()
				{
					Comment = item,
					User = _context.Users.Where(x => x.Id == item.UserId).FirstOrDefault()
				};
				list.Add(comview);
			}
			var model = new NewsDetailViewModel()
			{
				News = news,
				NewsCategory = _context.Categories.Find(_context.CategoryNews.Where(x => x.NewsId == news.Id).First().CategoryId),
				NewsImage = _context.Images.Where(x => x.NewsId == news.Id).FirstOrDefault(),
				NewsComments = list,
				NewsComment = null
			};
			if (Email is not null)
            {
                
                var kontrol = _context.Users.Where(x => x.Email == Email).FirstOrDefault();
                if (kontrol is not null)
                {
                    var comment = new NewsComment();
                    comment.UserId = kontrol.Id;
                    comment.Comment = Message;
                    comment.CreatedAt = DateTime.UtcNow;
                    comment.IsActive= true;
                    comment.PostId = news.Id;
                    _context.Comments.Add(comment);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Detail));
                }
                else
                {
                    var user = new User();
                    user.Name = Name;
                    user.Email = Email;
                    user.CreatedAt = DateTime.UtcNow;
                    user.Password = "1234";
                    user.RoleId = 3;
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
                var user1= _context.Users.Where(x=>x.Email== Email).FirstOrDefault();
                var comment1= new NewsComment();
                comment1.UserId = user1.Id;
                comment1.Comment = Message;
                comment1.CreatedAt = DateTime.UtcNow;
                comment1.IsActive= false;
                comment1.PostId = news.Id;
                _context.Comments.Add(comment1);
                _context.SaveChanges();
                return RedirectToAction(nameof(Detail));
            }
            var comment2 = new NewsComment();
			if (HttpContext.Session.GetString("UserId") == null)
			{
				ViewBag.Message = "Lutfen Mail ve ismi bos birakmadan message yaziniz!!!";
				return View(model);
			}
			var userMail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            
            var user2 = _context.Users.Where(x=>x.Email==userMail).First();
            comment2.UserId = user2.Id;
            comment2.Comment = Message;
            comment2.CreatedAt = DateTime.UtcNow;
            comment2.IsActive = true;
            comment2.PostId = news.Id;
            _context.Comments.Add(comment2);
            _context.SaveChanges();
            
            return RedirectToAction(nameof(Detail));
        }
    }
}
