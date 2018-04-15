using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using SaleDrink.Areas.Admin.Models.BusinessModel;

namespace SaleDrink.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class OrderDetailsController : Controller
    {
        private Drinkdbcontext db = new Drinkdbcontext();

        // GET: Admin/OrderDetails
        public async Task<ActionResult> Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Color).Include(o => o.Order).Include(o => o.Product).Include(o => o.Size);
            return View(await orderDetails.ToListAsync());
        }

        // GET: Admin/OrderDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.ColorId = new SelectList(db.Colors, "ID", "Name");
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "CustomerName");
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name");
            ViewBag.SizeId = new SelectList(db.Sizes, "ID", "Name");
            return View();
        }

        // POST: Admin/OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderID,ProductID,Quantity,Price,ColorId,SizeId")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ColorId = new SelectList(db.Colors, "ID", "Name", orderDetail.ColorId);
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "CustomerName", orderDetail.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", orderDetail.ProductID);
            ViewBag.SizeId = new SelectList(db.Sizes, "ID", "Name", orderDetail.SizeId);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColorId = new SelectList(db.Colors, "ID", "Name", orderDetail.ColorId);
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "CustomerName", orderDetail.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", orderDetail.ProductID);
            ViewBag.SizeId = new SelectList(db.Sizes, "ID", "Name", orderDetail.SizeId);
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderID,ProductID,Quantity,Price,ColorId,SizeId")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ColorId = new SelectList(db.Colors, "ID", "Name", orderDetail.ColorId);
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "CustomerName", orderDetail.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", orderDetail.ProductID);
            ViewBag.SizeId = new SelectList(db.Sizes, "ID", "Name", orderDetail.SizeId);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            db.OrderDetails.Remove(orderDetail);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
