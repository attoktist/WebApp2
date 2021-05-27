using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp2.Models
{
    public class Article
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Article ParentArticle { get; set; }
    }
}
