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
    public class CategoryNews: BaseEntity
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int NewsId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }

        [ForeignKey(nameof(NewsId))]
        public virtual News? News { get; set; }
    }
}
