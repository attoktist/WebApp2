using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp2.Domain.Core
{
    [Table("Articles")]
    public class ArticleBase
    {
        
        public  int ID { get; set; }
        public string Name { get; set; }        
        
       // [Required]
        public int? ParentArticle_ID { get; set; }
    }
}
