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
using SaleDrink.Areas.Admin.Models.BusinessModel;
using Model.DAO;
using SaleDrink.Common;

namespace SaleDrink.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class ProductCategoriesController : Controller
    {
        private Drinkdbcontext db = new Drinkdbcontext();
        private static string pic;
        // GET: Admin/ProductCategories
        public async Task<ActionResult> Index()
        {
            return View(await db.ProductCategories.ToListAsync());
        }

        // GET: Admin/ProductCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = await db.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // GET: Admin/ProductCategories/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name");
            return View();
        }

        // POST: Admin/ProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Alias,Description,ParentID,DisplayOrder,HomeOrder,Image,HomeFlag,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status")] ProductCategory productCategory)
        {
            productCategory.Name = StringHelper.UpperCase(productCategory.Name);
            productCategory.Alias = StringHelper.ToUnsignString(productCategory.Alias);
            productCategory.Description = HttpUtility.HtmlDecode(productCategory.Description);
            
            if (ModelState.IsValid)
            {
                db.ProductCategories.Add(productCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productCategory);
        }

        // GET: Admin/ProductCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = await db.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: Admin/ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Alias,Description,ParentID,DisplayOrder,HomeOrder,Image,HomeFlag,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productCategory);
        }

        // GET: Admin/ProductCategories/Delete
        public JsonResult Delete(int id)
        {
            new ProductCategoriesDAO().Delete(id);

            return Json(new { redirectUrl = Url.Action("Index", "ProductCategories"), isRedirect = true });

        }

       
        public JsonResult ChangeStatus(int id)
        {
            var result = new ChangesDAO().ProductCategoriesStatus(id);
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
            ProductCategory user = db.ProductCategories.Find(id);
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
