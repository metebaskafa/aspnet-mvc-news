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
    public class Page : BaseAuditEntity
    {
        [Required]
        [MaxLength(200)]
        [Column(name: "Başlık", TypeName = "nvarchar")]
        public string Title { get; set; }

        [Required]
        [Column(name: "İçerik", TypeName = "text")]
        public string Content { get; set; }


        [Column(name: "Aktif?", TypeName = "bit")]
        public bool IsActive { get; set; }
    }
}
