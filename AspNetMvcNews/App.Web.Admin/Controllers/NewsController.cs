using App.Data;
using App.Data.Entity;
using App.Web.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Web;

namespace App.Web.Admin.Controllers
{
    [Authorize(Policy = "ModeratorPolicy")]
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;

        public NewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: NewsController
        public ActionResult Index()
        {
            var userMail = User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            var model = _context.News.ToList();
            var kullanici = _context.Users.Where(x => x.Email == userMail).FirstOrDefault();
            if (kullanici.RoleId == 2)
            {
                var modelmod = _context.News.Where(x => x.UserId == kullanici.Id).ToList();
                return View(modelmod);
            }
            return View(model);
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {
            var model = new NewsCRUDModel();
            List<int> ids = new List<int>();
            //model.Categories= _context.Categories.Select(x=> new SelectListItem { Text=x.Name, Value=x.Id.ToString() }).ToList();
            model.Categories = _context.Categories.ToList();
            return View(model);
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsCRUDModel collection)
        {
            try
            {
                var haber = new News();
                var UserMail = User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                var user = _context.Users.Where(x => x.Email == UserMail).FirstOrDefault();
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Lütfen girdileri kontrol ediniz!");
                }
                else
                {
                    haber.Title = collection.Title;
                    haber.Content = collection.Content;
                    haber.IsBreaking = collection.IsBreaking;
                    haber.CreatedAt = DateTime.UtcNow;
                    haber.UserId = user.Id;
                    _context.News.Add(haber);
                    _context.SaveChanges();
                    if (collection.CategoryIds is not null)
                    {
                        foreach (var item in collection.CategoryIds)
                        {
                            var catnews = new CategoryNews();
                            catnews.CategoryId = item;
                            catnews.NewsId = haber.Id;
                            _context.Add(catnews);
                        }
                        _context.SaveChanges();
                    }
                    return RedirectToAction(nameof(Index));
                }


            }
            catch
            {

            }
            collection.Categories = _context.Categories.ToList();
            return View(collection);
        }

        // GET: NewsController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var haber = _context.News.Find(id.Value);
            if (haber == null)
            {
                return NotFound();
            }
            var model = new NewsCRUDModel();
            model.IsBreaking = haber.IsBreaking;
            model.Content = haber.Content;
            model.Title = haber.Title;
            model.Categories = _context.Categories.ToList();
            List<int> ids = _context.CategoryNews.Where(x => x.NewsId == id.Value).Select(x => x.CategoryId).ToList();
            model.CategoryIds = ids.ToArray();
            return View(model);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NewsCRUDModel collection)
        {
            try
            {
                var haber = _context.News.Find(id);
                if (haber == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Lütfen girdileri kontrol ediniz!");
                }
                else
                {
                    haber.Title = collection.Title;
                    haber.Content = collection.Content;
                    haber.IsBreaking = collection.IsBreaking;
                    haber.UpdatedAt = DateTime.UtcNow;
                    _context.News.Update(haber);

                    List<int> ids = _context.CategoryNews.Where(x => x.NewsId == id).Select(x => x.Id).ToList();
                    foreach (var item in ids)
                    {
                        _context.CategoryNews.Remove(_context.CategoryNews.Find(item));
                    }
                    _context.SaveChanges();
                    if (collection.CategoryIds is not null)
                    {
                        foreach (var item in collection.CategoryIds)
                        {
                            var catnews = new CategoryNews();
                            catnews.CategoryId = item;
                            catnews.NewsId = haber.Id;
                            _context.Add(catnews);
                        }
                        _context.SaveChanges();
                    }
                    return RedirectToAction(nameof(Index));
                }


            }
            catch
            {

            }
            collection.Categories = _context.Categories.ToList();
            return View(collection);
        }

        // GET: NewsController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var haber = _context.News.Find(id.Value);
            if (haber == null)
            {
                return NotFound();
            }
            var model = new NewsCRUDModel();
            model.IsBreaking = haber.IsBreaking;
            model.Content = haber.Content;
            model.Title = haber.Title;
            model.Categories = _context.Categories.ToList();
            List<int> ids = _context.CategoryNews.Where(x => x.NewsId == id.Value).Select(x => x.CategoryId).ToList();
            model.CategoryIds = ids.ToArray();
            return View(model);
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, NewsCRUDModel collection)
        {
            try
            {
                var haber = _context.News.Find(id);
                if (haber == null)
                {
                    return NotFound();
                }
                haber.DeletedAt = DateTime.UtcNow;
                _context.News.Update(haber);

                List<int> ids = _context.CategoryNews.Where(x => x.NewsId == id).Select(x => x.Id).ToList();
                foreach (var item in ids)
                {
                    _context.CategoryNews.Remove(_context.CategoryNews.Find(item));
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));



            }
            catch
            {

            }
            collection.Categories = _context.Categories.ToList();
            return View(collection);
        }
    }
}
