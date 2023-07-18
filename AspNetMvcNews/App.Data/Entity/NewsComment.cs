using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Entity.Abstract;

namespace App.Data.Entity
{
    public class NewsComment: BaseAuditEntity
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
