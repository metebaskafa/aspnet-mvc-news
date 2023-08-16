using App.Data.Entity.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entity
{
    public class NewsImage : BaseEntity
    {
        [Required]
        public int NewsId { get; set; }

        [ForeignKey(nameof(NewsId))]
        public virtual News? News { get; set; }

        [MaxLength(600)]
        [Column(name: "Resim", TypeName = "nvarchar")]
        public string? ImagePath { get; set; }
    }
}
