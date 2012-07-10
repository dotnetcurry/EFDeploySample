using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EfDeploySample.Models;
using System.Configuration;

namespace EfDeploySample.Controllers
{
    public class BlogCommentsController : Controller
    {
        private EfDeploySampleContext db = new EfDeploySampleContext(
             ConfigurationManager
                        .ConnectionStrings["EfDeploySampleContext"]
                            .ConnectionString,
             ConfigurationManager
                        .AppSettings["schemaName"]);

        //
        // GET: /BlogComments/

        public ActionResult Index()
        {
            var blogcomments = db.BlogComments.Include(b => b.ParentPost);
            return View(blogcomments.ToList());
        }

        //
        // GET: /BlogComments/Details/5

        public ActionResult Details(int id = 0)
        {
            BlogComment blogcomment = db.BlogComments.Find(id);
            if (blogcomment == null)
            {
                return HttpNotFound();
            }
            return View(blogcomment);
        }

        //
        // GET: /BlogComments/Create

        public ActionResult Create()
        {
            ViewBag.BlogPostId = new SelectList(db.BlogPosts, "Id", "Title");
            return View();
        }

        //
        // POST: /BlogComments/Create

        [HttpPost]
        public ActionResult Create(BlogComment blogcomment)
        {
            if (ModelState.IsValid)
            {
                db.BlogComments.Add(blogcomment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogPostId = new SelectList(db.BlogPosts, "Id", "Title", blogcomment.BlogPostId);
            return View(blogcomment);
        }

        //
        // GET: /BlogComments/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BlogComment blogcomment = db.BlogComments.Find(id);
            if (blogcomment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogPostId = new SelectList(db.BlogPosts, "Id", "Title", blogcomment.BlogPostId);
            return View(blogcomment);
        }

        //
        // POST: /BlogComments/Edit/5

        [HttpPost]
        public ActionResult Edit(BlogComment blogcomment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogcomment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogPostId = new SelectList(db.BlogPosts, "Id", "Title", blogcomment.BlogPostId);
            return View(blogcomment);
        }

        //
        // GET: /BlogComments/Delete/5

        public ActionResult Delete(int id = 0)
        {
            BlogComment blogcomment = db.BlogComments.Find(id);
            if (blogcomment == null)
            {
                return HttpNotFound();
            }
            return View(blogcomment);
        }

        //
        // POST: /BlogComments/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogComment blogcomment = db.BlogComments.Find(id);
            db.BlogComments.Remove(blogcomment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}