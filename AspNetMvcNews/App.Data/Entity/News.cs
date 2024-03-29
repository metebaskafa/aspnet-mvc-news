﻿using App.Data.Entity.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entity
{
    public class News : BaseAuditEntity
    {
        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        [MaxLength(200)]
        [Column(name: "Başlık", TypeName = "nvarchar")]
        public string Title { get; set; }

        [MinLength(200)]
        [MaxLength(2000)]
        [Column(name: "İçerik", TypeName = "nvarchar")]
        public string Content { get; set; }

        [Column(name: "FlaşHaber?", TypeName = "bit")]
        public bool IsBreaking { get; set; }


        // Relations
        //public List<NewsImage>? Images { get; set; }
        //public List<NewsComment>? Comments { get; set; }
        //public List<CategoryNews>? Categories { get; set; }

        // how to use inner join in entity framework core (using linq)?
    }
}
