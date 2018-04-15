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
using SaleDrink.Common;
using Model.DAO;

namespace SaleDrink.Areas.Admin.Controllers
{
    public class PostCategoriesController : Controller
    {
        private Drinkdbcontext db = new Drinkdbcontext();

        // GET: Admin/PostCategories
        public async Task<ActionResult> Index()
        {
            return View(await db.PostCategories.ToListAsync());
        }

        // GET: Admin/PostCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCategory postCategory = await db.PostCategories.FindAsync(id);
            if (postCategory == null)
            {
                return HttpNotFound();
            }
            return View(postCategory);
        }

        // GET: Admin/PostCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PostCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Alias,Description,ParentID,DisplayOrder,Image,HomeFlag,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status")] PostCategory postCategory)
        {
            postCategory.Name = StringHelper.UpperCase(postCategory.Name);
            postCategory.Alias = StringHelper.ToUnsignString(postCategory.Alias);
            if (ModelState.IsValid)
            {
                db.PostCategories.Add(postCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(postCategory);
        }

        // GET: Admin/PostCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCategory postCategory = await db.PostCategories.FindAsync(id);
            if (postCategory == null)
            {
                return HttpNotFound();
            }
            return View(postCategory);
        }

        // POST: Admin/PostCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Alias,Description,ParentID,DisplayOrder,Image,HomeFlag,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status")] PostCategory postCategory)
        {
            postCategory.Name = StringHelper.UpperCase(postCategory.Name);
            postCategory.Alias = StringHelper.ToUnsignString(postCategory.Alias);
            if (ModelState.IsValid)
            {
                db.Entry(postCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(postCategory);
        }

        // GET: Admin/PostCategories/Delete/5
        public JsonResult Delete(int id)
        {
            new PostNewsDAO().Deletect(id);

            return Json(new { redirectUrl = Url.Action("Index", "PostCategories"), isRedirect = true });

        }

        public JsonResult ChangeStatus(int id)
        {
            var result = new ChangesDAO().PostsCateStatus(id);
            return Json(new
            {
                status = result
            });
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
