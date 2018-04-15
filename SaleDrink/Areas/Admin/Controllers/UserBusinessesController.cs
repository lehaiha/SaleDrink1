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
    public class UserBusinessesController : Controller
    {
        private AdminDbContext db = new AdminDbContext();

        
        public ActionResult UpdateBusiness()
        {
            ReflectionController rc = new ReflectionController();
            List<Type> listControllerType = rc.GetControllers("SaleDrink.Areas.Admin.Controllers");
            List<string> listControllerOld = db.Businesseses.Select(c => c.BusinessId).ToList();
            List<string> listPermistionOld = db.Permissions.Select(p => p.PermissionName).ToList();
            foreach (var c in listControllerType)
            {
                if (!listControllerOld.Contains(c.Name))
                {
                    UserBusiness b = new UserBusiness() { BusinessId = c.Name, BusinessName = "Chưa có mô tả" };
                    db.Businesseses.Add(b);
                }
                List<string> listPermission = rc.GetActions(c);
                foreach (var p in listPermission)
                {
                    if (!listPermistionOld.Contains(c.Name + "-" + p))
                    {
                        UserPermission permission = new UserPermission() { PermissionName = c.Name + "-" + p, Description = "Chưa có mô tả", BusinessId = c.Name };
                        db.Permissions.Add(permission);
                    }
                }
            }
            db.SaveChanges();
            TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'></span><span class='sr-only'></span> Cập nhật thành công</div>";
            return RedirectToAction("Index");
        }

        // GET: Admin/UserBusinesses
        public async Task<ActionResult> Index()
        {
            return View(await db.Businesseses.Where(x=>x.Status==true || x.Status == null).ToListAsync());
        }

        // GET: Admin/UserBusinesses/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBusiness userBusiness = await db.Businesseses.FindAsync(id);
            if (userBusiness == null)
            {
                return HttpNotFound();
            }
            return View(userBusiness);
        }

        // GET: Admin/UserBusinesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/UserBusinesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BusinessId,BusinessName")] UserBusiness userBusiness)
        {
            if (ModelState.IsValid)
            {
                db.Businesseses.Add(userBusiness);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(userBusiness);
        }

        // GET: Admin/UserBusinesses/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBusiness userBusiness = await db.Businesseses.FindAsync(id);
            if (userBusiness == null)
            {
                return HttpNotFound();
            }
            return View(userBusiness);
        }

        // POST: Admin/UserBusinesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BusinessId,BusinessName,Status")] UserBusiness userBusiness)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userBusiness).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userBusiness);
        }

        // GET: Admin/UserBusinesses/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBusiness userBusiness = await db.Businesseses.FindAsync(id);
            if (userBusiness == null)
            {
                return HttpNotFound();
            }
            return View(userBusiness);
        }

        // POST: Admin/UserBusinesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            UserBusiness userBusiness = await db.Businesseses.FindAsync(id);
            db.Businesseses.Remove(userBusiness);
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
