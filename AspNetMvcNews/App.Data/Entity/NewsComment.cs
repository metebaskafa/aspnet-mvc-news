using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entity
{
    public class NewsComment: BaseClass, IAuiditEntity
    {
        [ForeignKey("News")]
        public int PostId { get; set; }
        public News? News { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Column(name: "Yorum", Order = 3, TypeName = "text")]
        public string? Comment { get; set; }
        [Column(name: "Aktif?", Order = 4, TypeName = "bool")]
        public bool IsActive { get; set; }
        [Required]
        [Column(name: "Oluşturulma Tarihi", Order = 5, TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(name: "Güncelleme Tarihi", Order = 6, TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        [Column(name: "Silinme Tarihi", Order = 7, TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
    }
}
