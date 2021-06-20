using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp2.Domain.Core;
using WebApp2.Domain.Interfaces;
using WebApp2.Infrastructure.Data;


namespace WebApp2.Controllers
{
    public class ArticlesController : ApiController
    {
        // private ArticleContext db = new ArticleContext();
        private ArticleRepository repo;

        public ArticlesController()
        {
            repo = new ArticleRepository();
        }
        // GET: api/Articles
        public List<Article> GetArticles()
        {
            return repo.GetList().ToList();
        }

        // GET: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult GetArticle(int id)
        {
            Article article = repo.Get(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        // PUT: api/Articles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticle(int id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.ID)
            {
                return BadRequest();
            }

            repo.Update(article);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Articles
        [ResponseType(typeof(Article))]
        public IHttpActionResult PostArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Create(article);

            return CreatedAtRoute("DefaultApi", new { id = article.ID }, article);
        }

        // DELETE: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult DeleteArticle(int id)
        {
            Article article = repo.Get(id);
            if (article == null)
            {
                return NotFound();
            }

            repo.Delete(id);

            return Ok(article);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool ArticleExists(int id)
        //{
        //    return db.Articles.Count(e => e.ID == id) > 0;
        //}
    }
}