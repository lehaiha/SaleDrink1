using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleDrink.Areas.Admin.Models.BusinessModel
{
    public class AuthorizeBusiness: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["userId"] == null)
            {
                //filterContext.Result = new RedirectResult("/Admin/Home/Login?returnUrl=/Admin/" + filterContext.ActionDescriptor.ControllerDescriptor + "/" + filterContext.ActionDescriptor.ActionName);
                filterContext.Result = new RedirectResult("/Admin/Home/Login");
                return;
            }
            int userId = int.Parse(HttpContext.Current.Session["userId"].ToString());

            //Lấy tên action
            string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName
                + "Controller-" +
                filterContext.ActionDescriptor.ActionName;

            AdminDbContext db = new AdminDbContext();
            //Lấy thông tin user
            var admin = db.Administrators
                .Where(a => a.UserId == userId && a.IsAdmin.Value != 0)
                .FirstOrDefault();

            //Nếu là admin  thì mặc nhiên được vào và không cần kiểm tra
            if (admin != null)
            {
                return;
            }

            //Lấy ra tên các permission được gán cho người dùng
            var listpermission = from p in db.Permissions
                                 join g in db.GrantPermissions on p.PermissionId equals g.PermissionId
                                 where g.UserId == userId
                                 select p.PermissionName;

            //Kiểm tra xem các permision có chứa tên action mà người dùng kich hoạt hay không?
            //Nếu không thì nhẩy tới trang thông báo
            if (!listpermission.Contains(actionName))
            {
                filterContext.Result = new RedirectResult("/Admin/Home/NotificationAuthorize");
                return;
            }

        }
    }
}