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
    public class ProductsController : Controller
    {
        private Drinkdbcontext db = new Drinkdbcontext();

        // GET: Admin/Products
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.ProductCategory);
            return View(await products.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Alias,CategoryID,ThumbnailImage,Price,OriginalPrice,PromotionPrice,IncludedVAT,Warranty,Description,Content,HomeFlag,HotFlag,ViewCount,Tags,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status")] Product product)
        {
            product.Name = StringHelper.UpperCase(product.Name);
            product.Alias = StringHelper.ToUnsignString(product.Alias);
            product.Description = HttpUtility.HtmlDecode(product.Description);
            product.Price = product.Price;
            //product.CategoryID = product.CategoryID;
            product.CreatedDate = DateTime.Now;
            product.Status = product.Status;
            product.HotFlag = product.HotFlag;
            product.HomeFlag = product.HomeFlag;
            product.ThumbnailImage = product.ThumbnailImage;

            product.MetaDescription = HttpUtility.HtmlDecode(product.MetaDescription);

            product.ViewCount = 1;
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            

            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
     
            Product product = await db.Products.FindAsync(id);
            pic = product.ThumbnailImage;

            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Alias,CategoryID,ThumbnailImage,Price,OriginalPrice,PromotionPrice,IncludedVAT,Warranty,Description,Content,HomeFlag,HotFlag,ViewCount,Tags,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status")] Product product)
        {
            if (pic != product.ThumbnailImage)
            {
                product.ThumbnailImage = pic;
            }
            product.Name = StringHelper.UpperCase(product.Name);
            product.Alias = StringHelper.ToUnsignString(product.Alias);
            product.Description = HttpUtility.HtmlDecode(product.Description);
            product.Price = product.Price;
            //product.CategoryID = product.CategoryID;
            product.UpdatedDate = DateTime.Now;
            product.Status = product.Status;
            product.HotFlag = product.HotFlag;
            product.HomeFlag = product.HomeFlag;
        

            product.MetaDescription = HttpUtility.HtmlDecode(product.MetaDescription);
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        private static string pic;

        public JsonResult Delete(int id)
        {
            new ProductDao().Delete(id);

            return Json(new { redirectUrl = Url.Action("Index", "Products"), isRedirect = true });

        }
        public string ChangeImage(int? id, string picture)
        {
            pic = picture;
            if (id == null)
            {
                return "Mã không tồn tại";
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return "Mã không tồn tại";
            }
            product.ThumbnailImage = picture;
            db.SaveChanges();
            return "";
        }
        public JsonResult ChangeStatus(int id)
        {
            var result = new ChangesDAO().ProductStatus(id);
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
