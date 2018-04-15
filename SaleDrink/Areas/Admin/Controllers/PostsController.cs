using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;
using SaleDrink.Areas.Admin.Models.BusinessModel;
using SaleDrink.Common;

namespace SaleDrink.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class PostsController : Controller
    {
        private Drinkdbcontext db = new Drinkdbcontext();
        private static string pic;

        // GET: Admin/Posts
        public async Task<ActionResult> Index()
        {
            var posts = db.Posts.Include(p => p.PostCategory);
            return View(await posts.ToListAsync());
        }

        // GET: Admin/Posts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.PostCategories, "ID", "Name");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Alias,CategoryID,Image,Description,Content,HomeFlag,HotFlag,ViewCount,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status")] Post post)
        {
            post.Name = StringHelper.UpperCase(post.Name);
            post.Alias = StringHelper.ToUnsignString(post.Alias);
            post.Description = HttpUtility.HtmlDecode(post.Description);
            post.Content = HttpUtility.HtmlDecode(post.Content);
            post.CreatedDate = DateTime.Now;
            post.ViewCount = 1;
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.PostCategories, "ID", "Name", post.CategoryID);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            pic = post.Image;
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.PostCategories, "ID", "Name", post.CategoryID);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Alias,CategoryID,Image,Description,Content,HomeFlag,HotFlag,ViewCount,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status")] Post post)
        {
            if (pic != post.Image)
            {
                post.Image = pic;
            }
            post.Alias = StringHelper.ToUnsignString(post.Alias);
            post.UpdatedDate = DateTime.Now;
            post.Description = HttpUtility.HtmlDecode(post.Description);
            post.Content = HttpUtility.HtmlDecode(post.Content);
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.PostCategories, "ID", "Name", post.CategoryID);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public JsonResult Delete(int id)
        {
            new PostNewsDAO().Delete(id);

            return Json(new { redirectUrl = Url.Action("Index", "Posts"), isRedirect = true });

        }
        public JsonResult ChangeStatus(int id)
        {
            var result = new ChangesDAO().PostsStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public string ChangeImage(int? id, string picture)
        {
            pic = picture;
            if (id == null)
            {
                return "Mã không tồn tại";
            }
            Post user = db.Posts.Find(id);
            if (user == null)
            {
                return "Mã không tồn tại";
            }
            user.Image = picture;
            db.SaveChanges();
            return "";
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
