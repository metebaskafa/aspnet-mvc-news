using App.Data.Entity;

namespace App.Web.Mvc.Models
{
	public class CategoryPostView
	{
		public Category CategoryLeft { get; set; }
		public List<News> NewsLeft { get; set; }
		public NewsImage? ImageLeft { get; set; }
		public Category CategoryRight { get; set; }
		public List<News> NewsRight { get;set; }
		public NewsImage? ImageRight { get; set; }
		public List<MiddleNews> MiddleNews { get; set; }
	}
}
