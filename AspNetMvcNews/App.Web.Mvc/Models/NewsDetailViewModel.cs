using App.Data.Entity;

namespace App.Web.Mvc.Models
{
    public class NewsDetailViewModel
    {
        public News News { get; set; }
        public Category NewsCategory { get; set; }
        public NewsImage? NewsImage { get; set; }
        public List<NewsCommentView>? NewsComments { get; set; }
        public NewsComment? NewsComment { get; set; }
    }
}
