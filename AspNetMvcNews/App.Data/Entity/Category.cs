using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Entity.Abstract;

namespace App.Data.Entity
{
    public class Category: BaseEntity
    {
        [MaxLength(100)]
        [Required]
        [Column(name:"Ad", TypeName = "nvarchar")]
        public string Name { get; set; }

        [MaxLength(200)]
        [Column(name: "Açıklama", TypeName = "nvarchar")]
        public string? Description { get; set; }
        //public ICollection<CategoryNews>? News { get; set; }
    }
}
