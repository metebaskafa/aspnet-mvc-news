using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Search(string q, int page)
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            return View();
        }
    }
}
