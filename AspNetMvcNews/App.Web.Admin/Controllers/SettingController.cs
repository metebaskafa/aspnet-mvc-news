using App.Data;
using App.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Web.Admin.Controllers
{
    [Authorize(Policy ="AdminPolicy")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SettingController
        public ActionResult Index()
        {
            var model =_context.Settings.ToList();
            return View(model);
        }

        // GET: SettingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SettingController/Create
        public ActionResult Create()
        {
            ViewBag.UserId= new SelectList(_context.Users.ToList(),"Id","Email");
            return View();
        }

        // POST: SettingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Setting collection)
        {
            try
            {
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", "Hatalı girdiler var. Lütfen kontrol ediniz.");
				}
				else
				{
					Setting setting = new();
					setting.Name = collection.Name;
					setting.Value = collection.Value;
					setting.UserId = collection.UserId;
					setting.CreatedAt = DateTime.UtcNow;
					_context.Settings.Add(setting);
					_context.SaveChanges();

					return RedirectToAction(nameof(Index));
				}
			}
            catch
            {
                
            }
			ViewBag.UserId = new SelectList(_context.Users.ToList(), "Id", "Email");
			return View();
		}

        // GET: SettingController/Edit/5
        public ActionResult Edit(int? id)
        {
			if (id is null)
			{
				return BadRequest();
			}
			var model = _context.Settings.Find(id.Value);
			if (model == null)
			{
				return NotFound();
			}
			ViewBag.UserId = new SelectList(_context.Users.ToList(), "Id", "Email");
			return View(model);
        }

        // POST: SettingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Setting collection)
        {
            try
            {
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", "Hatalı girdiler var. Lütfen kontrol ediniz.");
				}
				else
				{
					Setting setting = _context.Settings.Find(collection.Id);
					setting.Name = collection.Name;
					setting.Value = collection.Value;
					setting.UserId = collection.UserId;
					setting.UpdatedAt = DateTime.UtcNow;
					_context.Settings.Update(setting);
					_context.SaveChanges();

					return RedirectToAction(nameof(Index));
				}
			}
            catch
            {
                
            }
			ViewBag.UserId = new SelectList(_context.Users.ToList(), "Id", "Email");
			return View();
		}

        // GET: SettingController/Delete/5
        public ActionResult Delete(int? id)
        {
			if (id is null)
			{
				return BadRequest();
			}
			var model = _context.Settings.Find(id.Value);
			if (model == null)
			{
				return NotFound();
			}

			return View(model);
		}

        // POST: SettingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Setting collection)
        {
            try
            {
				Setting setting = _context.Settings.Find(collection.Id);
				setting.DeletedAt = DateTime.UtcNow;
				_context.Settings.Update(setting);
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
