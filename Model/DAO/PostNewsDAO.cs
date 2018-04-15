using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class PostNewsDAO
    {
        private Drinkdbcontext db = new Drinkdbcontext();

        public IEnumerable<Post> ListByGroupTT(int top)
        {
            return db.Posts.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true && x.CategoryID != 1002 && x.CategoryID == 1).Take(top).ToList(); ;
        }

        public IEnumerable<Post> ListByGroupCT(int top)
        {
            return db.Posts.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true && x.CategoryID != 1002 && x.CategoryID == 1003).Take(top).ToList(); ;
        }

        public IEnumerable<Post> ListByGroup()
        {
            return db.Posts.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true && x.CategoryID != 1002).ToList(); ;
        }

        public IEnumerable<Post> ListByGroup(int top)
        {
            return db.Posts.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true && x.CategoryID != 1002).Take(top).ToList(); ;
        }

        public IEnumerable<Post> ListByGroupId( long id)
        {
            return db.Posts.Where(x => x.Status == true && x.ID == id).ToList();
        }

        public Post ViewDetail(long id)
        {
            var item = db.Posts.Find(id);

            // Tăng số lần xem
            item.ViewCount++;
            db.SaveChanges();
            return item;
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Posts.Find(id);
                db.Posts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Deletect(int id)
        {
            try
            {
                var user = db.PostCategories.Find(id);
                db.PostCategories.Remove(user);
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
