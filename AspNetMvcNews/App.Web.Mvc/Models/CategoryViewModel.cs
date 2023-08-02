using App.Data.Entity;

namespace App.Web.Mvc.Models
{
    public class CategoryViewModel
    {
        public Category? Category { get; set; }
        public List<News>? News { get; set; }
    }
}
