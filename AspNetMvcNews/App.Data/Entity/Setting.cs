using App.Data.Entity.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entity
{
    public class Setting : BaseAuditEntity
    {

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        [Required]
        [MaxLength(200)]
        [Column(name: "Ad", TypeName = "nvarchar")]
        public string Name { get; set; }

        [Required]
        [MaxLength(400)]
        [Column(name: "Değeri", TypeName = "nvarchar")]
        public string Value { get; set; }
    }
}
