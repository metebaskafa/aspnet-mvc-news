using App.Data.Entity;

namespace App.Web.Mvc.Models
{
    public class TodayNewsViewModel
    {
        public List<News> News { get; set; }
        public List<int> CommentCount { get; set; }
        public List<Category> Categories { get; set; }
    }
}
