using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class SlideDAO
    {
        private Drinkdbcontext db = new Drinkdbcontext();

        public IEnumerable<Slide> SlideShow()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Slides.Find(id);
                db.Slides.Remove(user);
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
