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
using SaleDrink.Areas.Admin.Models.DaoModel;

namespace SaleDrink.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class UserAdministratorsController : Controller
    {
        private AdminDbContext db = new AdminDbContext();
        
        // GET: Admin/UserAdministrators
        public async Task<ActionResult> Index()
        {
            return View(await db.Administrators.ToListAsync());
        }

        // GET: Admin/UserAdministrators/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAdministrator userAdministrator = await db.Administrators.FindAsync(id);
            if (userAdministrator == null)
            {
                return HttpNotFound();
            }
            return View(userAdministrator);
        }

        // GET: Admin/UserAdministrators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/UserAdministrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,UserName,Password,FullName,Email,Avatar,IsAdmin,Allowed,CreatedDate")] UserAdministrator userAdministrator)
        {
            var user = db.Administrators.SingleOrDefault(x => x.UserName == userAdministrator.UserName);

            var email = db.Administrators.SingleOrDefault(x => x.Email == userAdministrator.Email);

            string passMD5 = Common.Encryptor.MD5Hash(userAdministrator.UserName + userAdministrator.Password);

            userAdministrator.Password = passMD5;
            userAdministrator.CreatedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    if (email == null)
                    {
                        db.Administrators.Add(userAdministrator);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Email đã có người dùng, nhập Email khác";
                    }
                }
                else
                {
                    ViewBag.error = "Tài khoản đã tồn tại";
                }
            }

            return View(userAdministrator);
        }
        private static string  pass;
        private static string pic;
        

        // GET: Admin/UserAdministrators/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAdministrator userAdministrator = await db.Administrators.FindAsync(id);
            pass = db.Administrators.SingleOrDefault(x => x.Password == userAdministrator.Password).Password;
            
            if (userAdministrator == null)
            {
                return HttpNotFound();
            }
            return View(userAdministrator);
        }

        // POST: Admin/UserAdministrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserId,UserName,Password,FullName,Email,Avatar,IsAdmin,Allowed,CreatedDate")] UserAdministrator userAdministrator)
        {

            if (pic != userAdministrator.Avatar)
            {
                userAdministrator.Avatar = pic;
            } 

            if (pass != userAdministrator.Password)
            {
                string passMD5 = Common.Encryptor.MD5Hash(userAdministrator.UserName + userAdministrator.Password);

                userAdministrator.Password = passMD5;
            }

            if (ModelState.IsValid)
            {
                db.Entry(userAdministrator).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userAdministrator);
        }

               
        public JsonResult Delete(int id)
        {
             new UserDaoModel().Delete(id);

            return Json(new { redirectUrl = Url.Action("Index", "UserAdministrators"), isRedirect = true });

        }
        
        public JsonResult ChangeStatus(int id)
        {
            var result = new UserDaoModel().ChangeStatus(id);
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
            UserAdministrator user = db.Administrators.Find(id);
            if (user == null)
            {
                return "Mã không tồn tại";
            }
            user.Avatar = picture;
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
