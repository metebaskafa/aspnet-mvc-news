using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entity.Abstract
{
    public abstract class BaseAuditEntity : BaseEntity, IAuiditEntity
    {
        [Required]
        [Column(name: "Yayınlanma Tarihi", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [Column(name: "Güncelleme Tarihi", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [Column(name: "Silinme Tarihi", TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
    }
}
