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
    public class UserGrantPermissionsController : Controller
    {
        private AdminDbContext db = new AdminDbContext();

        // GET: Admin/UserGrantPermissions
        public async Task<ActionResult> Index(int id)
        {
            //Lấy tất cả các nghiệp vụ (controller) trong csdl
            var listcontrol = db.Businesseses.Where(x=>x.Status==true);
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in listcontrol)
            {
                items.Add(new SelectListItem() { Text = item.BusinessName, Value = item.BusinessId });
            }
            //Lưu ra biến
            ViewBag.items = items;

            //Lấy danh sách quyền đã được cấp
            var listgranted = from g in db.GrantPermissions
                              join p in db.Permissions on g.PermissionId equals p.PermissionId
                              where g.UserId == id
                              select new SelectListItem() { Value = p.PermissionId.ToString(), Text = p.Description.ToString() };
            //Lưu ra biến
            ViewBag.listgranted = listgranted;
            //Lưu id của người dùng đang được cấp ra session
            Session["usergrant"] = id;
            //Lấy người dùng
            var usergrant = await db.Administrators.FindAsync(id);
            //Lưu tên ra biến
            ViewBag.usergrant = ": " + usergrant.UserName + " " + '(' + usergrant.FullName + ')';
            return View();
        }

        //Lấy danh sách quyền đang được cấp cho người dùng
        public JsonResult getPermissions(string id, int usertemp)
        {
            //Lấy tất cả các permission của user và của business
            var listgranted = (from g in db.GrantPermissions
                               join p in db.Permissions on g.PermissionId equals p.PermissionId
                               where g.UserId == usertemp && p.BusinessId == id
                               select new PermissionAction { PermissionId = p.PermissionId, PermissionName = p.PermissionName, Description = p.Description, IsGranted = true }).ToList();

            //Lấy tất cả các permission của business hiện tại
            var listpermission = from p in db.Permissions.Where(x => x.Status == true)
                                 where p.BusinessId == id
                                 select new PermissionAction { PermissionId = p.PermissionId, PermissionName = p.PermissionName, Description = p.Description, IsGranted = false };

            //Lấy tất cả id của permission đã được gán ở trên cho người dùng
            var listpermissionId = listgranted.Select(p => p.PermissionId);

            //So sánh kiểm tra permission của business mà chưa có trong listgrant thì đưa vào (IsGrant=false)
            foreach (var item in listpermission)
            {
                if (!listpermissionId.Contains(item.PermissionId))
                    listgranted.Add(item);
            }
            return Json(listgranted.OrderBy(x => x.Description), JsonRequestBehavior.AllowGet);

        }

        //Cập nhật quyền cho người dùng
        public string updatePermission(int id, int usertemp)
        {
            string msg = "";
            var grant = db.GrantPermissions.Find(id, usertemp);
            var p = db.Permissions.Where(x => x.Status == true);
            if (grant == null)
            {
                UserGrantPermission g = new UserGrantPermission()
                {
                    PermissionId = id,
                    UserId = usertemp,
                    Description = ""
                };
                
                db.GrantPermissions.Add(g);
                msg = "<div class='alert alert-success'>Quyền cấp thành công</div>";
            }
            else
            {
                db.GrantPermissions.Remove(grant);
                msg = "<div class='alert alert-danger'>Quyền hủy thành công</div>";
            }
            db.SaveChanges();
            return msg;
        }




        // GET: Admin/UserGrantPermissions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGrantPermission userGrantPermission = await db.GrantPermissions.FindAsync(id);
            if (userGrantPermission == null)
            {
                return HttpNotFound();
            }
            return View(userGrantPermission);
        }

        // GET: Admin/UserGrantPermissions/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Administrators, "UserId", "UserName");
            ViewBag.PermissionId = new SelectList(db.Permissions, "PermissionId", "PermissionName");
            return View();
        }

        // POST: Admin/UserGrantPermissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PermissionId,UserId,Description")] UserGrantPermission userGrantPermission)
        {
            if (ModelState.IsValid)
            {
                db.GrantPermissions.Add(userGrantPermission);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Administrators, "UserId", "UserName", userGrantPermission.UserId);
            ViewBag.PermissionId = new SelectList(db.Permissions, "PermissionId", "PermissionName", userGrantPermission.PermissionId);
            return View(userGrantPermission);
        }

        // GET: Admin/UserGrantPermissions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGrantPermission userGrantPermission = await db.GrantPermissions.FindAsync(id);
            if (userGrantPermission == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Administrators, "UserId", "UserName", userGrantPermission.UserId);
            ViewBag.PermissionId = new SelectList(db.Permissions, "PermissionId", "PermissionName", userGrantPermission.PermissionId);
            return View(userGrantPermission);
        }

        // POST: Admin/UserGrantPermissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PermissionId,UserId,Description")] UserGrantPermission userGrantPermission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userGrantPermission).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Administrators, "UserId", "UserName", userGrantPermission.UserId);
            ViewBag.PermissionId = new SelectList(db.Permissions, "PermissionId", "PermissionName", userGrantPermission.PermissionId);
            return View(userGrantPermission);
        }

        // GET: Admin/UserGrantPermissions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGrantPermission userGrantPermission = await db.GrantPermissions.FindAsync(id);
            if (userGrantPermission == null)
            {
                return HttpNotFound();
            }
            return View(userGrantPermission);
        }

        // POST: Admin/UserGrantPermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserGrantPermission userGrantPermission = await db.GrantPermissions.FindAsync(id);
            db.GrantPermissions.Remove(userGrantPermission);
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
