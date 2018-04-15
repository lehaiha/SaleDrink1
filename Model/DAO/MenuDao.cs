using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
namespace Model.DAO
{
    public class MenuDao
    {
        private Drinkdbcontext db = new Drinkdbcontext();

        public IEnumerable<Menu> ListByGroupId()
        {
            return db.Menus.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
