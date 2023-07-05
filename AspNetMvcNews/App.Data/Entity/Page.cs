using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entity
{
    public class Page:BaseClass, IAuiditEntity
    {
        [Required]
        [Column(name: "Başlık", Order = 1, TypeName = "nvarchar(200)")]
        public string Title { get; set; }
        [Column(name: "İçerik", Order = 2, TypeName = "text")]
        public string Content { get; set; }
        [Column(name: "Aktif?", Order = 3, TypeName = "bool")]
        public bool IsActive { get; set; }
        [Required]
        [Column(name: "Oluşturulma Tarihi", Order = 4, TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(name: "Güncelleme Tarihi", Order = 5, TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        [Column(name: "Silinme Tarihi", Order = 6, TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
    }
}
