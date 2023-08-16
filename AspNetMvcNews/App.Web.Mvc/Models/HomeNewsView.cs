using App.Data.Entity;

namespace App.Web.Mvc.Models
{
    public class HomeNewsView
    {
        public News News { get; set; }
        public Category Category { get; set; }
        public NewsImage? NewsImage { get; set; }
    }
}
