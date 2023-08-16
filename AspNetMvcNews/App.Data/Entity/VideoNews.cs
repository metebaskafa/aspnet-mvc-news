using App.Data.Entity.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entity
{
	public class VideoNews : BaseAuditEntity
	{
		[MaxLength(200)]
		[Column(name: "Başlık", TypeName = "nvarchar")]
		public string Title { get; set; }

		[MaxLength(500)]
		[Column(name: "Video Linki", TypeName = "nvarchar")]
		public string VideoLink { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
    }
}
