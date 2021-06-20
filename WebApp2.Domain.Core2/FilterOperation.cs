using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp2.Domain.Core
{
    public class FilterOperation
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int[] Contractors { get; set; }
        public int[] Articles { get; set; }

        public static List<Article> getAllParentArticle(List<Article> articles, List<Article> idArticles)
        {
            List<Article> filterArt = new List<Article>();
            List<Article> id = new List<Article>();
            for (int i = 0; i < idArticles.Count; i++)
            {
                filterArt.AddRange(articles.Where(a => a.ID == idArticles[i].ID).ToList());
                id = articles.Where(a => a.ParentArticle!=null && a.ParentArticle.ID == idArticles[i].ID).ToList();
                filterArt.AddRange(getAllParentArticle(articles, id));               
            }

            return filterArt;
        }

        public static List<Contractor> getAllContractors(List<Contractor> contractors, List<Contractor> idContractors)
        {
            List<Contractor> filterCont = new List<Contractor>();
            for (int i = 0; i < idContractors.Count; i++)
            {
                filterCont.AddRange(contractors.Where(c => c.ID == idContractors[i].ID).ToList());
            }

            return filterCont;
        }
    }
}