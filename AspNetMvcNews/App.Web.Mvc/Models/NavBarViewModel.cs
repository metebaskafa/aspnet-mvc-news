using App.Data.Entity;

namespace App.Web.Mvc.Models
{
    public class NavBarViewModel
    {
        public ICollection<News>? Today { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Page>? Pages { get; set; }
    }
}
