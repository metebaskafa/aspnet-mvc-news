using App.Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Web.Admin.Models
{
	public class NewsCRUDModel
	{
		[MaxLength(200)]
		[MinLength(1)]
		[Display(Name = "Başlık")]
		public string Title { get; set; }

		[MinLength(200)]
		[MaxLength(2000)]
		[Display(Name = "İçerik")]
		public string Content { get; set; }

		[Display(Name = "FlaşHaberMi?")]
		public bool IsBreaking { get; set; }

		[Display(Name = "Kategoriler")]
		public List<Category>? Categories { get; set; }
		public int[]? CategoryIds { get; set; }
	}
}
