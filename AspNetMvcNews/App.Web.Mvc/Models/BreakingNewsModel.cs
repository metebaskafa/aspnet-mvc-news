using App.Data.Entity;

namespace App.Web.Mvc.Models
{
    public class BreakingNewsModel
    {
        public News News { get; set; }
        public NewsImage? NewsImage { get; set; }
    }
}
