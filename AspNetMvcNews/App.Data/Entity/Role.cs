using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Entity.Abstract;

namespace App.Data.Entity
{
    public class Role : BaseEntity
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        [Column(name:"Rol",TypeName = "nvarchar")]
        public string? Name { get; set; }
    }
}
