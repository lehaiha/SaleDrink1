using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProjectDao
    {
        private Drinkdbcontext db = new Drinkdbcontext();

        public IEnumerable<Project> getProjectDao()
        {
            return db.Projects.Where(x => x.Status == true).ToList();


        }
    }
}
