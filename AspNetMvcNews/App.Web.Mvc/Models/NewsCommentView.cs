using App.Data.Entity;

namespace App.Web.Mvc.Models
{
    public class NewsCommentView
    {
        public User User { get; set; }
        public NewsComment Comment { get; set; }
    }
}
