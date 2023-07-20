using App.Data.Entity.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entity
{
    public class NewsComment : BaseAuditEntity
    {
        [Required]
        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public virtual News? News { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        [Column(name: "Yorum", TypeName = "text")]
        public string? Comment { get; set; }

        [Column(name: "Aktif?", TypeName = "bit")]
        public bool IsActive { get; set; }
    }
}
