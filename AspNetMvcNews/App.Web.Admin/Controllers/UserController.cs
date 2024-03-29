﻿using App.Data;
using App.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace App.Web.Admin.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserController
        public ActionResult Index()
        {
            var model = _context.Users.ToList();
            return View(model);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(_context.Roles.ToList(), "Id", "Name");
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Hatalı girdiler var. Lütfen kontrol ediniz.");
                }
                else
                {
                    if (_context.Users.Any(x => x.Email == collection.Email))
                    {
                        ModelState.AddModelError("", "Bu Emailde kullanıcı var.");
                    }
                    else
                    {
                        User user = new();
                        user.Name = collection.Name;
                        user.Email = collection.Email;
                        user.Password = collection.Password;
                        user.City = collection.City;
                        user.RoleId = collection.RoleId;
                        user.CreatedAt = DateTime.UtcNow;
                        _context.Users.Add(user);
                        _context.SaveChanges();

                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            catch
            {

            }
            ViewBag.RoleId = new SelectList(_context.Roles.ToList(), "Id", "Name");
            return View();
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var model = _context.Users.Find(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.RoleId = new SelectList(_context.Roles.ToList(), "Id", "Name");
            return View(model);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Hatalı girdiler var. Lütfen kontrol ediniz.");
                }
                else
                {
                    if (_context.Users.Where(x => x.Id == collection.Id).FirstOrDefault().Email!=collection.Email && _context.Users.Any(x=>x.Email==collection.Email))
                    {
                        ModelState.AddModelError("", "Bu Emailde kullanıcı var.");
                    }
                    else
                    {
                        User user = _context.Users.Find(collection.Id);
                        user.Name = collection.Name;
                        user.Email = collection.Email;
                        user.Password = collection.Password;
                        user.City = collection.City;
                        user.RoleId = collection.RoleId;
                        user.UpdatedAt = DateTime.UtcNow;
                        _context.Users.Update(user);
                        _context.SaveChanges();

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {

            }
            ViewBag.RoleId = new SelectList(_context.Roles.ToList(), "Id", "Name");
            return View();
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var model = _context.Users.Find(id.Value);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, User collection)
        {
            try
            {
                User user = _context.Users.Find(collection.Id);
                user.DeletedAt = DateTime.UtcNow;
                _context.Users.Update(user);
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
