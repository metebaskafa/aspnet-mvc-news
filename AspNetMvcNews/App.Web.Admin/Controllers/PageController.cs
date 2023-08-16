using App.Data;
using App.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Web.Admin.Controllers
{
    [Authorize(Policy ="AdminPolicy")]
    public class PageController : Controller
    {
        private readonly AppDbContext _context;

        public PageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PageController
        public ActionResult Index()
        {
            var model = _context.Pages.ToList();
            return View(model);
        }

        // GET: PageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Page collection)
        {
            try
            {
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", "Hatalı girdiler var. Lütfen kontrol ediniz.");
				}
				else
				{
					Page page = new();
					page.Title = collection.Title;
					page.Content = collection.Content;
                    page.IsActive = true;
					page.CreatedAt = DateTime.UtcNow;
					_context.Pages.Add(page);
					_context.SaveChanges();

					return RedirectToAction(nameof(Index));
				}
			}
            catch
            {
                
            }
			return View(collection);
		}

        // GET: PageController/Edit/5
        public ActionResult Edit(int? id)
        {
			if (id is null)
			{
				return BadRequest();
			}
			var model = _context.Pages.Find(id.Value);
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
		}

        // POST: PageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Page collection)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Hatalı girdiler var. Lütfen kontrol ediniz.");
                }
                else
                {
                    Page page = _context.Pages.Find(collection.Id);
                    page.Title = collection.Title;
                    page.Content = collection.Content;
                    page.UpdatedAt = DateTime.UtcNow;
                    _context.Pages.Update(page);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                
            }
			return View(collection);
		}

        // GET: PageController/Delete/5
        public ActionResult Delete(int? id)
        {
			if (id is null)
			{
				return BadRequest();
			}
			var model = _context.Pages.Find(id.Value);
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
		}

        // POST: PageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Page collection)
        {
            try
            {
				Page page = _context.Pages.Find(collection.Id);
                page.IsActive = false;
				page.DeletedAt= DateTime.UtcNow;
				_context.Pages.Update(page);
				_context.SaveChanges();

				return RedirectToAction(nameof(Index));
			}
            catch
            {
                return View();
            }
        }
    }
}
