using App.Data;
using App.Data.Entity;
using App.Web.Admin.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Admin.Controllers
{
    [Authorize(Policy = "ModeratorPolicy")]
    public class NewsImageController : Controller
    {
        private readonly AppDbContext _context;

        public NewsImageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: NewsImageController
        public ActionResult Index()
        {
            var model = _context.Images.Include("News").ToList();
            return View(model);
        }

        // GET: NewsImageController/Details/5
        public ActionResult Details(int id)
        {
            var model = _context.Images.Include("News").Where(x=>x.NewsId == id).ToList();
            return View(model);
        }

        // GET: NewsImageController/Create
        public ActionResult Create()
        {
			ViewBag.NewsId = new SelectList(_context.News.ToList(), "Id", "Title");
			return View();
        }

        // POST: NewsImageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewsImage collection, IFormFile ImagePath)
        {
            try
            {
                var image = new NewsImage();
                image.NewsId = collection.NewsId;
                image.ImagePath = await FileController.FileLoaderAsync(ImagePath);
                _context.Images.Add(image);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
			}
            catch
            {
                
            }
            ViewBag.NewsId = new SelectList(_context.News.ToList(),"Id","Title");
			return View(collection);
		}

        // GET: NewsImageController/Edit/5
        public ActionResult Edit(int? id)
        {
			if (id is null)
			{
				return BadRequest();
			}
			var model = _context.Images.Find(id.Value);
			if (model == null)
			{
				return NotFound();
			}
			ViewBag.NewsId = new SelectList(_context.News.ToList(), "Id", "Title");
			return View(model);
        }

        // POST: NewsImageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, NewsImage collection, IFormFile ImagePath)
        {
            try
            {
                var image = _context.Images.Find(id);
                if (image == null)
                {
                    return NotFound();
                }
                if (ImagePath is not null)
                {
                    image.ImagePath= await FileController.FileLoaderAsync(ImagePath);
                }
                image.NewsId = collection.NewsId;
                _context.Images.Update(image);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
            }
			ViewBag.NewsId = new SelectList(_context.News.ToList(), "Id", "Title");
			return View(collection);
		}

        // GET: NewsImageController/Delete/5
        public ActionResult Delete(int? id)
        {
			if (id is null)
			{
				return BadRequest();
			}
			var model = _context.Images.Include("News").Where(x=>x.Id==id.Value).FirstOrDefault();
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
		}

        // POST: NewsImageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, NewsImage collection)
        {
            try
            {
                var image = _context.Images.Find(id);
                if (image == null)
                {
                    return NotFound();
                }
                _context.Images.Remove(image);
                FileController.FileRemover(collection.ImagePath);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
            }
			return View(collection);
		}
    }
}
