using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaleDrink.Areas.Admin.Models.BusinessModel;
using SaleDrink.Areas.Admin.Models.DataModel;

namespace SaleDrink.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class UserPostsController : Controller
    {
        private AdminDbContext db = new AdminDbContext();

        // GET: Admin/UserPosts
        public async Task<ActionResult> Index()
        {
            var posts = db.Posts.Include(u => u.UserAdministrator).Include(u => u.UserCatrgory);
            return View(await posts.ToListAsync());
        }

        // GET: Admin/UserPosts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPost userPost = await db.Posts.FindAsync(id);
            if (userPost == null)
            {
                return HttpNotFound();
            }
            return View(userPost);
        }

        // GET: Admin/UserPosts/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Administrators, "UserId", "UserName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/UserPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PostId,Title,Brief,Content,Picture,CreateDate,Tags,CategoryId,ViewNo,Status,UserId")] UserPost userPost)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(userPost);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Administrators, "UserId", "UserName", userPost.UserId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", userPost.CategoryId);
            return View(userPost);
        }

        // GET: Admin/UserPosts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPost userPost = await db.Posts.FindAsync(id);
            if (userPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Administrators, "UserId", "UserName", userPost.UserId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", userPost.CategoryId);
            return View(userPost);
        }

        // POST: Admin/UserPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PostId,Title,Brief,Content,Picture,CreateDate,Tags,CategoryId,ViewNo,Status,UserId")] UserPost userPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userPost).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Administrators, "UserId", "UserName", userPost.UserId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", userPost.CategoryId);
            return View(userPost);
        }

        // GET: Admin/UserPosts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPost userPost = await db.Posts.FindAsync(id);
            if (userPost == null)
            {
                return HttpNotFound();
            }
            return View(userPost);
        }

        // POST: Admin/UserPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserPost userPost = await db.Posts.FindAsync(id);
            db.Posts.Remove(userPost);
            await db.SaveChangesAsync();
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
