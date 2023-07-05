using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entity
{
    public class User: BaseClass, IAuiditEntity
    {
        [Required]
        [Column(name: "Email", Order = 1, TypeName = "varchar(200)")]
        public string Email { get; set; }
        [Required]
        [Column(name: "Şifre", Order = 2, TypeName = "nvarchar(100)")]
        public string Password { get; set; }
        [Required]
        [Column(name: "Ad", Order = 3, TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column(name: "Şehir", Order = 4, TypeName = "nvarchar(100)")]
        public string? City { get; set; }
        [Required]
        [Column(name: "Oluşturulma Tarihi", Order = 5, TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(name: "Güncelleme Tarihi", Order = 6, TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        [Column(name: "Silinme Tarihi", Order = 7, TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
        public List<NewsComment>? NewsComments { get; set; }
        public List<Setting>? Settings { get; set; }


    }
}
