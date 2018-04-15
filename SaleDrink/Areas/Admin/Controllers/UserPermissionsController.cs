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
    public class UserPermissionsController : Controller
    {
        private AdminDbContext db = new AdminDbContext();

        // GET: Admin/UserPermissions
        
        public async Task<ActionResult> Index(string id)
        {
            var permissions = db.Permissions.Where(u => u.BusinessId == id && (u.Status == true || u.Status == null));
            return View(await permissions.ToListAsync());
        }

        

        // GET: Admin/UserPermissions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPermission userPermission = await db.Permissions.FindAsync(id);
            if (userPermission == null)
            {
                return HttpNotFound();
            }
            return View(userPermission);
        }

        // GET: Admin/UserPermissions/Create
        public ActionResult Create()
        {
            ViewBag.BusinessId = new SelectList(db.Businesseses, "BusinessId", "BusinessName");
            return View();
        }

        // POST: Admin/UserPermissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PermissionId,PermissionName,Description,BusinessId")] UserPermission userPermission)
        {
            if (ModelState.IsValid)
            {
                db.Permissions.Add(userPermission);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessId = new SelectList(db.Businesseses, "BusinessId", "BusinessName", userPermission.BusinessId);
            return View(userPermission);
        }

        // GET: Admin/UserPermissions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPermission userPermission = await db.Permissions.FindAsync(id);
            if (userPermission == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessId = new SelectList(db.Businesseses, "BusinessId", "BusinessName", userPermission.BusinessId);
            return View(userPermission);
        }

        // POST: Admin/UserPermissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PermissionId,PermissionName,Description,BusinessId,Status")] UserPermission userPermission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userPermission).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "UserPermissions", new { id = userPermission.BusinessId });
            }
            ViewBag.BusinessId = new SelectList(db.Businesseses, "BusinessId", "BusinessName", userPermission.BusinessId);
            return View(userPermission);
        }

        // GET: Admin/UserPermissions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPermission userPermission = await db.Permissions.FindAsync(id);
            if (userPermission == null)
            {
                return HttpNotFound();
            }
            return View(userPermission);
        }

        // POST: Admin/UserPermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserPermission userPermission = await db.Permissions.FindAsync(id);
            db.Permissions.Remove(userPermission);
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
