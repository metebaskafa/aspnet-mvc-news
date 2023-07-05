using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entity
{
    public class NewsImage:BaseClass
    {
        [ForeignKey("News")]
        public int? NewsId { get; set; }
       
        public News? News { get; set; } 
        [Column(name: "Resim Yolu", Order = 2, TypeName = "nvarchar(200)")]
        public string? ImagePath { get; set; }
    }
}
