using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
   public class ProjectMenuDao
    {
            private Drinkdbcontext db = new Drinkdbcontext();

            public IEnumerable<ProductCategory> getProjectMenuDao()
            {
                return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();


            }
        
    }
}
