using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using Model.EF;
using Model.DAO;
using SaleDrink.Common;
using SaleDrink.Areas.Admin.Models.BusinessModel;

namespace SaleDrink.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class ProjectProductsController : Controller
    {
        private Drinkdbcontext db = new Drinkdbcontext();
        private static string pic;
        // GET: Admin/ProjectProducts
        public async Task<ActionResult> Index()
        {
            return View(await db.ProjectProducts.ToListAsync());
        }

        // GET: Admin/ProjectProducts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectProduct projectProduct = await db.ProjectProducts.FindAsync(id);
            if (projectProduct == null)
            {
                return HttpNotFound();
            }
            return View(projectProduct);
        }

        // GET: Admin/ProjectProducts/Create
        public ActionResult Create()        {
            
            return View();
        }

        // POST: Admin/ProjectProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Alias,CategoryID,ThumbnailImage,Price,OriginalPrice,PromotionPrice,IncludedVAT,Warranty," +
            "Description,Content,HomeFlag,HotFlag,ViewCount,Tags,CreatedDate,CreatedBy," +
            "UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status")] ProjectProduct projectProduct)
        {
            projectProduct.Name = StringHelper.UpperCase(projectProduct.Name);
            projectProduct.Alias = StringHelper.ToUnsignString(projectProduct.Alias);
            projectProduct.CategoryID = projectProduct.CategoryID;
            projectProduct.ThumbnailImage = projectProduct.ThumbnailImage;
            projectProduct.Price = projectProduct.Price;
            projectProduct.OriginalPrice = projectProduct.OriginalPrice;
            projectProduct.PromotionPrice = projectProduct.PromotionPrice;
            projectProduct.IncludedVAT = projectProduct.IncludedVAT;
            projectProduct.Warranty = projectProduct.Warranty;           
            projectProduct.Description = HttpUtility.HtmlDecode(projectProduct.Description);
            projectProduct.Content = HttpUtility.HtmlDecode(projectProduct.Content);
            projectProduct.HomeFlag = projectProduct.HomeFlag;
            projectProduct.HotFlag=projectProduct.HotFlag;
            projectProduct.CreatedDate = DateTime.Now;
            projectProduct.ViewCount = 1;
            projectProduct.Status = projectProduct.Status;
            if (ModelState.IsValid)
            {
                db.ProjectProducts.Add(projectProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(projectProduct);
        }

        // GET: Admin/ProjectProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectProduct projectProduct = await db.ProjectProducts.FindAsync(id);
            pic = projectProduct.ThumbnailImage;
            if (projectProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.ProjectProducts, "ID", "Name", projectProduct.CategoryID);
            return View(projectProduct);
        }

        // POST: Admin/ProjectProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Alias,CategoryID,ThumbnailImage,Price,OriginalPrice,PromotionPrice,IncludedVAT,Warranty,Description,Content,HomeFlag,HotFlag,ViewCount,Tags,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status")] ProjectProduct projectProduct)
        {
            if (pic != projectProduct.ThumbnailImage)
            {
                projectProduct.ThumbnailImage = pic;
            }
            projectProduct.Name = StringHelper.UpperCase(projectProduct.Name);
            projectProduct.Alias = StringHelper.ToUnsignString(projectProduct.Alias);
            projectProduct.CategoryID = projectProduct.CategoryID;
            projectProduct.ThumbnailImage = projectProduct.ThumbnailImage;
            projectProduct.Price = projectProduct.Price;
            projectProduct.OriginalPrice = projectProduct.OriginalPrice;
            projectProduct.PromotionPrice = projectProduct.PromotionPrice;
            projectProduct.IncludedVAT = projectProduct.IncludedVAT;
            projectProduct.Warranty = projectProduct.Warranty;           
            projectProduct.Description = HttpUtility.HtmlDecode(projectProduct.Description);
            projectProduct.Content = HttpUtility.HtmlDecode(projectProduct.Content);
            projectProduct.HomeFlag = projectProduct.HomeFlag;
            projectProduct.HotFlag=projectProduct.HotFlag;
            projectProduct.UpdatedDate = DateTime.Now;
            projectProduct.ViewCount = 1;
            projectProduct.Status = projectProduct.Status;
            if (ModelState.IsValid)
            {
                db.Entry(projectProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.ProjectProducts, "ID", "Name", projectProduct.CategoryID);
            return View(projectProduct);
        }

        // GET: Admin/ProjectProducts/Delete/5
        public JsonResult Delete(int id)
        {
            new ProjectProductDao().Delete(id);

            return Json(new { redirectUrl = Url.Action("Index", "ProjectProducts"), isRedirect = true });

        }
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProjectProduct projectProduct = await db.ProjectProducts.FindAsync(id);
            db.ProjectProducts.Remove(projectProduct);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public JsonResult ChangeStatus(int id)
        {
            var result = new ChangesDAO().ProjectProductsStatus(id);
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
            ProjectProduct user = db.ProjectProducts.Find(id);
            if (user == null)
            {
                return "Mã không tồn tại";
            }
            user.ThumbnailImage = picture;
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
