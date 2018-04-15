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

namespace SaleDrink.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class SlidesController : Controller
    {
        private Drinkdbcontext db = new Drinkdbcontext();
        private static string pic;
        // GET: Admin/Slides
        public async Task<ActionResult> Index()
        {
            return View(await db.Slides.ToListAsync());
        }

        // GET: Admin/Slides/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = await db.Slides.FindAsync(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: Admin/Slides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Slides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Description,Image,Url,DisplayOrder,Status,Content")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                db.Slides.Add(slide);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(slide);
        }

        // GET: Admin/Slides/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = await db.Slides.FindAsync(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Description,Image,Url,DisplayOrder,Status,Content")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slide).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(slide);
        }

        // GET: Admin/Slides/Delete/5
        public JsonResult Delete(int id)
        {
            new SlideDAO().Delete(id);

            return Json(new { redirectUrl = Url.Action("Index", "Slides"), isRedirect = true });

        }

        public JsonResult ChangeStatus(int id)
        {
            var result = new ChangesDAO().SliseStatus(id);
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
            Slide user = db.Slides.Find(id);
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
