using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entity
{
    public class Setting: BaseClass
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Column(name: "Ad", Order = 2, TypeName = "nvarchar(200)")]
        public string Name { get; set; }
        [Column(name: "Değeri", Order = 3, TypeName = "nvarchar(400)")]
        public string Value { get; set; }
    }
}
