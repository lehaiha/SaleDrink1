using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProjectProductDao
    {
        private Drinkdbcontext db = new Drinkdbcontext();

        public IEnumerable<ProjectProduct> getProjectProductDao()
        {
            return db.ProjectProducts.Where(x => x.Status == true).ToList();


        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.ProjectProducts.Find(id);
                db.ProjectProducts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


    }

}
