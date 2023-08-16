using App.Data;
using App.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace App.Web.Admin.Controllers
{
    [Authorize(Policy = "ModeratorPolicy")]
    public class VideoNewsController : Controller
    {
        private readonly AppDbContext _context;

        public VideoNewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: VideoNewsController
        public ActionResult Index()
        {
            var userMail = User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            var model = _context.VideoNews.ToList();
            var kullanici = _context.Users.Where(x => x.Email == userMail).FirstOrDefault();
            if (kullanici.RoleId == 2)
            {
                var modelmod = _context.VideoNews.Where(x => x.UserId == kullanici.Id).ToList();
                return View(modelmod);
            }
            return View(model);
        }

        // GET: VideoNewsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VideoNewsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VideoNewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VideoNews collection)
        {
            try
            {
                var vidnews = new VideoNews();
                var userMail = User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                var kullanici = _context.Users.Where(x => x.Email == userMail).FirstOrDefault();
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Girişinizde bir hata var lütfen kontrol edip düzeltin.");
                }
                else
                {
                    vidnews.Title = collection.Title;
                    vidnews.VideoLink = collection.VideoLink;
                    vidnews.UserId = kullanici.Id;
                    vidnews.CreatedAt = DateTime.UtcNow;
                    _context.VideoNews.Add(vidnews);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));

                }
            }
            catch
            {
            }
            return View(collection);

        }

        // GET: VideoNewsController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = _context.VideoNews.FirstOrDefault(x=> x.Id == id.Value);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: VideoNewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VideoNews collection)
        {
            try
            {
                var video = _context.VideoNews.Find(id);
                if (video == null)
                {
                    return NotFound();
                }
                video.Title = collection.Title;
                video.VideoLink = collection.VideoLink;
                video.UpdatedAt = DateTime.UtcNow;
                _context.VideoNews.Update(video);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: VideoNewsController/Delete/5
        public ActionResult Delete(int? id)
        {
			if (id == null)
			{
				return BadRequest();
			}
			var model = _context.VideoNews.FirstOrDefault(x => x.Id == id.Value);
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
		}

        // POST: VideoNewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VideoNews collection)
        {
			try
			{
				var video = _context.VideoNews.Find(id);
				if (video == null)
				{
					return NotFound();
				}
				video.DeletedAt = DateTime.UtcNow;
				_context.VideoNews.Update(video);
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
