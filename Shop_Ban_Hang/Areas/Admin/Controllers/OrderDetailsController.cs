using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shop_Ban_Hang.Models;
using PagedList;
using PagedList.Mvc;

namespace Shop_Ban_Hang.Areas.Admin.Controllers
{
    public class OrderDetailsController : Controller
    {
        private Shop_Ban_HangEntities db = new Shop_Ban_HangEntities();

        // GET: Admin/OrderDetails
        //public ActionResult Index()
        //{
        //    var orderDetails = db.OrderDetails.Include(o => o.Order).Include(o => o.Product);
        //    return View(orderDetails.ToList());
        //}
		public ActionResult Index(int? page,string searchstring)
		{
			int pageSize = 9;
			int pageNumber = (page ?? 1);
			//if (!String.IsNullOrEmpty(searchstring))
			//{
			//	var cate = db.OrderDetails.Where(c => c..Contains(searchstring)).ToList();


			//	return View(cate.OrderBy(o => o.Name).ToPagedList(pageNumber, pageSize));

			//}
			return View(db.OrderDetails.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
		}

		// GET: Admin/OrderDetails/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "CustomerId");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: Admin/OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Orders, "Id", "CustomerId", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "CustomerId", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "CustomerId", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();
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
