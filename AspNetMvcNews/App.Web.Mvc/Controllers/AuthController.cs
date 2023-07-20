using App.Data;
using App.Data.Entity;
using App.Web.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace App.Web.Mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Boş geçilemeyecek alanları lütfen doldurun!");
                }
                else
                {
                    var kullanici = _context.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                    if (kullanici != null)
                    {
                        ModelState.AddModelError("", "Bu maili kullanan bir kullanıcı zaten var");
                        return RedirectToAction(nameof(ForgotPassword));
                    }
                    else
                    {
                        var kullanici1 = new User
                        {
                            Email = user.Email,
                            Name = user.Name,
                            Password = user.Password,
                            City = user.City,
                            RoleId = 3,
                            CreatedAt = DateTime.Now
                        };
                        await _context.Users.AddAsync(kullanici1);
                        await _context.SaveChangesAsync();
                        return Redirect("/Auth/Login");
                    }

                }
            }
            catch (Exception)
            {

            }
            return View();
        }
        public IActionResult Login(string returnUrl)
        {

            var model = new LoginViewModel() { redirectUrl = returnUrl };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel user)
        {
            try
            {
                if (user == null)
                {
                    ModelState.AddModelError("", "Boş geçilemez!");
                }
                if (ModelState.IsValid)
                {
                    var kullanici = _context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
                    if (kullanici == null)
                    {
                        ModelState.AddModelError("", "Hatalı giriş yaptınız.");
                    }
                    else
                    {
                        var kullaniciyetkileri = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email,kullanici.Email),

                        };
                        if (kullanici.RoleId == 1)
                            kullaniciyetkileri.Add(new Claim("Role", "Admin"));
                        else if (kullanici.RoleId == 2)
                            kullaniciyetkileri.Add(new Claim("Role", "Moderator"));
                        else if (kullanici.RoleId == 3)
                            kullaniciyetkileri.Add(new Claim("Role", "Visitor"));
                        var kullanicikimligi = new ClaimsIdentity(kullaniciyetkileri, "Login");
                        ClaimsPrincipal claimsPrincipal = new(kullanicikimligi);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        HttpContext.Session.SetInt32("UserId", kullanici.Id);

                        return Redirect(string.IsNullOrEmpty(user.redirectUrl) ? "/Home/Index" : user.redirectUrl);
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Oluştu!");

            }
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Geçersiz email...");
                return View(forgotPassword);
            }
            var kullanici = _context.Users.Where(x => x.Email == forgotPassword.Email).FirstOrDefault();
            if (kullanici == null)
            {
                ModelState.AddModelError("", "Bu mailde bir kullanıcı yok!");
                return View(forgotPassword);
            }
            else
            {
                SmtpClient smtpClient = new("burak.alanyali@yahoo.com", 25);
                smtpClient.Credentials = new NetworkCredential("burak.alanyali@yahoo.com", "Alanya6861");
                MailMessage message = new();
                message.From = new MailAddress("burak.alanyali@yahoo.com");
                message.To.Add(kullanici.Email);
                message.Subject = "Şifre Sıfırlama Talebi";
                message.Body = $"Kullanıcı Bilgileri : <hr /> " +
                    $"Ad : {kullanici.Name} <hr /> " +
                    $"Email : {kullanici.Email} <hr /> " +
                    $"Bilgilerine sahip kullanıcı şifre sıfırlama talebiniz alınmıştır.  <hr />" +
                    $"Devam etmek için <a href='https://localhost:7276/Auth/UpdatePassword?newPassword={kullanici.Email}' >Buraya</a> tıklayınız.";
                message.IsBodyHtml = true;
                //smtpClient.Send(message);
                return RedirectToAction(nameof(UpdatePassword), kullanici);

            }
        }
        public IActionResult UpdatePassword(App.Data.Entity.User user)
        {
            var kullanici = _context.Users.Where(x => x.Email == user.Email).FirstOrDefault();
            var model = new UpdatePasswordViewModel()
            {
                User = kullanici,
                Password = null
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePasswordAsync(UpdatePasswordViewModel model)
        {
            var kullanici = _context.Users.Where(x=>x.Email==model.User.Email).FirstOrDefault();
            kullanici.Password=model.Password;
            kullanici.UpdatedAt = DateTime.Now;
            _context.Update(kullanici);
            await _context.SaveChangesAsync();
            return Redirect("/Auth/Login");
        }
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("UserId");
            }
            catch (Exception)
            {
                HttpContext.Session.Clear();

            }
            return Redirect("/Home/Index");
        }
    }
}
