using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Entity.Abstract;

namespace App.Data.Entity
{
    public class User: BaseAuditEntity
    {
        [Required]
        [Column(TypeName = "nvarchar")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(200)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(100)]
        [Required]
        [Column(name: "Şifre", TypeName = "nvarchar")]
        public string Password { get; set; }

        [MaxLength(100)]
        [Required]
        [Column(name: "Ad", TypeName = "nvarchar")]
        public string Name { get; set; }

        [MaxLength(100)]
        [Column(name: "Şehir", TypeName = "nvarchar")]
        public string? City { get; set; }

        [Required]
        public int RoleId { get; set; }

        // Relations
        //public List<NewsComment>? NewsComments { get; set; }
        //public List<Setting>? Settings { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role? Role { get; set; }
    }
}
