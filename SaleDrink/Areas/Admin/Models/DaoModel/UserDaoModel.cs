using SaleDrink.Areas.Admin.Models.BusinessModel;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SaleDrink.Areas.Admin.Models.DaoModel
{
    public class UserDaoModel
    {
        private AdminDbContext db = new AdminDbContext();
        private Drinkdbcontext db123 = new Drinkdbcontext();
        public bool ChangeStatus(int id)
        {
            var user = db.Administrators.Find(id);
            user.Allowed = !user.Allowed;
            db.SaveChanges();
            return user.Allowed;
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Administrators.Find(id);
                db.Administrators.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public int CountUserName()
        {
            return db.Administrators.Count();
        }
        public bool CheckUserName(string userName)
        {
            return db.Administrators.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.Administrators.Count(x => x.Email == email) > 0;
        }

        public int CountOder()
        {
            return db123.Orders.Count();
        }
    }
}