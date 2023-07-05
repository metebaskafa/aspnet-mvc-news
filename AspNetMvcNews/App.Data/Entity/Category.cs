using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entity
{
    public class Category: BaseClass
    {
        [Required]
        [Column(name:"Ad", Order =1, TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column(name: "Açıklama", Order = 2, TypeName = "nvarchar(200)")]
        public string? Description { get; set; }
        public List<CategoryNews>? News { get; set; }
    }
}
