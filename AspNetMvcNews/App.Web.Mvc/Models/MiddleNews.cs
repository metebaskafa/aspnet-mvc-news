using App.Data.Entity;

namespace App.Web.Mvc.Models
{
	public class MiddleNews
	{
		public Category Category { get; set; }
		public News News { get; set; }
		public NewsImage? NewsImage { get; set; }
	}
}
