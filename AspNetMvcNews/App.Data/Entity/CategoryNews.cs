using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entity
{
    public class CategoryNews: BaseClass
    {
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [ForeignKey("News")]
        public int NewsId { get; set; }
        public News? News { get; set; }
    }
}
