using App.Data;
using App.Data.Entity;
using App.Web.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Admin.Controllers
{
	[Authorize(Policy = "ModeratorPolicy")]
	public class NewsCommentController : Controller
	{
		private readonly AppDbContext _context;

		public NewsCommentController(AppDbContext context)
		{
			_context = context;
		}

		// GET: NewsCommentController
		public ActionResult Index()
		{
			var model=_context.Comments.Include(x=>x.User).Include(x=>x.News).OrderBy(x=>x.Id).ToList();
			return View(model);
		}

		// GET: NewsCommentController/Details/5
		public ActionResult Details(int id)
		{
			var model = _context.Comments.Include(x => x.User).Include(x => x.News).Where(x=>x.PostId==id).ToList();
			return View(model);
		}

		// GET: NewsCommentController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: NewsCommentController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: NewsCommentController/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id is null)
			{
				return BadRequest();
			}
			var model = _context.Comments.Include(x => x.User).Include(x => x.News).Where(x => x.Id == id.Value).FirstOrDefault(); 
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
		}

		// POST: NewsCommentController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, NewsComment collection)
		{
			try
			{
				var yorum = _context.Comments.Find(id);
				if (yorum == null)
				{
					return NotFound();
				}
				//if(ModelState.IsValid)
				//{
				//	ModelState.AddModelError("","Hata var kontrol ediniz!");
				//}
				//else
				//{
					yorum.UpdatedAt = DateTime.UtcNow;
					yorum.IsActive = collection.IsActive;
					_context.Comments.Update(yorum);
					_context.SaveChanges();
					return RedirectToAction(nameof(Index));
				//}
				
			}
			catch
			{
				
			}
			return View(collection);
		}

		// GET: NewsCommentController/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id is null)
			{
				return BadRequest();
			}
			var model = _context.Comments.Include(x => x.User).Include(x => x.News).Where(x => x.Id == id.Value).FirstOrDefault();
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
		}

		// POST: NewsCommentController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, NewsComment collection)
		{
			try
			{
				var yorum = _context.Comments.Find(id);
				if (yorum == null)
				{
					return NotFound();
				}
				yorum.DeletedAt = DateTime.UtcNow;
				yorum.IsActive = false;
				_context.Comments.Update(yorum);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));

			}
			catch
			{
				return View(collection);
			}
		}
	}
}
