using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KLMPortfolio.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;
using BlogPosts.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using BlogPosts;

namespace KLMPortfolio.Controllers
{
    [RequireHttps]
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //public ActionResult Index()
        //{
        //    return View(db.Posts.ToList());
        //}

        public ActionResult BlogDetails(string Slug)
        {
            if (String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.FirstOrDefault(p => p.Slug == Slug);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);

        }

        // GET: BlogPosts

        public ActionResult Index(int? page)
        {
            int pageSize = 3;//the number of posts you want to display per page
            int pageNumber = (page ?? 1);
            return View(db.Posts.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));
        }

        // POST: BlogPosts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? page, string searchStr)
        {
            int pageSize = 3;//the number of posts you want to display per page
            int pageNumber = (page ?? 1);

            var result = db.Posts.Where(p => p.Body.Contains(searchStr))

        .Union(db.Posts.Where(p => p.Title.Contains(searchStr)))
        .Union(db.Posts.Where(p => p.Comments.Any(c => c.Body.Contains(searchStr))))
        .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.DisplayName.Contains(searchStr))))
        .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.FirstName.Contains(searchStr))))
        .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.LastName.Contains(searchStr))))
        .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.UserName.Contains(searchStr))))
        .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.Email.Contains(searchStr))))
        .Union(db.Posts.Where(p => p.Comments.Any(c => c.UpdateReason.Contains(searchStr))));


            return View(result.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));
            //return View(db.Posts.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));
            //}
        }


        //GET:BlogPosts/Details/6

        public ActionResult Details(string Slug)
        {
            if (String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.FirstOrDefault(p => p.Slug == Slug);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Create
        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Created,Updated,Title,Slug,Body,MediaUrl,Published")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                //check the file name to make sure its an image
                var ext = Path.GetExtension(image.FileName).ToLower();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp")
                    ModelState.AddModelError("image", "Invalid Format.");
            }


            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    //relative server path
                    var filePath = "/Uploads/";
                    // path on physical drive on server
                    var absPath = Server.MapPath("~" + filePath);
                    // media url for relative path
                    blogPost.MediaUrl = filePath + image.FileName;
                    //save image
                    image.SaveAs(Path.Combine(absPath, image.FileName));
                    var Slug = StringUtilities.URLFriendly(blogPost.Title);
                    if (String.IsNullOrWhiteSpace(Slug))
                    {
                        ModelState.AddModelError("Title", "Invalid title.");
                        return View(blogPost);
                    }
                    if (db.Posts.Any(p => p.Slug == Slug))
                    {
                        ModelState.AddModelError("Title", "The title must be unique.");
                        return View(blogPost);
                    }
                    blogPost.Slug = Slug;
                }

                blogPost.Created = DateTime.Now;
                db.Posts.Add(blogPost);
                db.SaveChanges();

                //return View(blogPost);
            }
            return RedirectToAction("Index");
        }



        // GET: BlogPosts/Edit/5
        [Authorize(Roles = "Admin")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Updated,Title,Slug,Body,MediaUrl,Published")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost.Updated = DateTime.Now;
                db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);

        }

        // GET: BlogPosts/Delete/5

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.Posts.Find(id);
            db.Posts.Remove(blogPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
    
   
