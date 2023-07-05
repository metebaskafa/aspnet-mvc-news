using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entity
{
    public class News: BaseClass, IAuiditEntity
    {
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Column(name: "Başlık", Order = 2, TypeName = "nvarchar(200)")]
        public string Title { get; set; }
        [Column(name: "İçerik", Order = 3, TypeName = "ntext")]
        public string Content { get; set; }
        [Required]
        [Column(name: "Yayınlanma Tarihi", Order = 4, TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(name: "Güncelleme Tarihi", Order = 5, TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        [Column(name: "Silinme Tarihi", Order = 6, TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
        public List<NewsImage>? Images { get; set; }
        public List<NewsComment>? Comments { get; set; }
        public List<CategoryNews>? Categories { get; set; }
    }
}
