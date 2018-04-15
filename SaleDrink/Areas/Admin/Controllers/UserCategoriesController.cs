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
    
    public class UserCategoriesController : Controller
    {
        private AdminDbContext db = new AdminDbContext();

        // GET: Admin/UserCategories
        public async Task<ActionResult> Index()
        {
            var categories = db.Categories.Include(u => u.UserAdministrator);
            return View(await categories.ToListAsync());
        }

        // GET: Admin/UserCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCategory userCategory = await db.Categories.FindAsync(id);
            if (userCategory == null)
            {
                return HttpNotFound();
            }
            return View(userCategory);
        }

        // GET: Admin/UserCategories/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Administrators, "UserId", "UserName");
            return View();
        }

        // POST: Admin/UserCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryId,CategoryName,OrderNo,Status,UserId")] UserCategory userCategory)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(userCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Administrators, "UserId", "UserName", userCategory.UserId);
            return View(userCategory);
        }

        // GET: Admin/UserCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCategory userCategory = await db.Categories.FindAsync(id);
            if (userCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Administrators, "UserId", "UserName", userCategory.UserId);
            return View(userCategory);
        }

        // POST: Admin/UserCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryId,CategoryName,OrderNo,Status,UserId")] UserCategory userCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Administrators, "UserId", "UserName", userCategory.UserId);
            return View(userCategory);
        }

        // GET: Admin/UserCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCategory userCategory = await db.Categories.FindAsync(id);
            if (userCategory == null)
            {
                return HttpNotFound();
            }
            return View(userCategory);
        }

        // POST: Admin/UserCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserCategory userCategory = await db.Categories.FindAsync(id);
            db.Categories.Remove(userCategory);
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
