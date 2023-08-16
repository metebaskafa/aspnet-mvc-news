using App.Data;
using App.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var haber = _context.News.OrderByDescending(x => x.CreatedAt).First();
            var catid = _context.CategoryNews.Where(x=>x.NewsId==haber.Id).First().CategoryId;
            var model = new HomeNewsView()
            {
                News = haber,
                Category = _context.Categories.Find(catid),
                NewsImage = _context.Images.Where(x=>x.NewsId==haber.Id).FirstOrDefault()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}