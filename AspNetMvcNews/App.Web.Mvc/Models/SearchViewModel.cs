using App.Data.Entity;

namespace App.Web.Mvc.Models
{
    public class SearchViewModel
    {
        public Category Category { get; set; }
        public News News { get; set; }
    }
}
