using App.Data.Entity.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entity
{
    public class CategoryNews : BaseEntity
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
