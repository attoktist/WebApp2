
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp2.Domain.Core
{
    
    public class Article : ArticleBase
    {
        [Write(false)]
        [ForeignKey("ParentArticle_ID")]
        public virtual  Article ParentArticle { get; set; }
    }
}
