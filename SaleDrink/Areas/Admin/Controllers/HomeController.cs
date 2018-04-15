using SaleDrink.Areas.Admin.Models.BusinessModel;
using SaleDrink.Areas.Admin.Models.DaoModel;
using SaleDrink.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleDrink.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        AdminDbContext db = new AdminDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.User = new UserDaoModel().CountUserName();
            ViewBag.Order = new UserDaoModel().CountOder();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            string passMD5 = Encryptor.MD5Hash(userName + password);
            var user = db.Administrators.SingleOrDefault(x => x.UserName == userName && x.Password == passMD5 && x.Allowed == true);
            if (user != null)
            {
                Session["userId"] = user.UserId;
                Session["userName"] = user.UserName;
                Session["fullName"] = user.FullName;
                Session["avatar"] = user.Avatar;
                Session["isAdmin"] = user.IsAdmin;
                Session["createdDate"] = user.CreatedDate;
                return RedirectToAction("Index");
            }
            ViewBag.error = "Đăng nhập sai hoặc bạn không có quyền vào";
            return View();
            
        }

        public ActionResult Logout()
        {
            Session["userId"] = null;
            Session["userName"] = null;
            Session["fullName"] = null;
            Session["avatar"] = null;
            Session["isAdmin"] = null;
            Session["createdDate"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult NotificationAuthorize()
        {
            return View();
        }
        public EmptyResult Alive()
        {
            return new EmptyResult();
        }
    }
}