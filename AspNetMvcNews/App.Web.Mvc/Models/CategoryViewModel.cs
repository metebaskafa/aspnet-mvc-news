using App.Data.Entity;

namespace App.Web.Mvc.Models
{
    public class CategoryViewModel
    {
        public Category? Category { get; set; }
        public List<HomeNewsView>? News { get; set; }
    }
}
