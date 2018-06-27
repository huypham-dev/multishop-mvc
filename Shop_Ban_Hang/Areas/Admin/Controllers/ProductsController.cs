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
using System.Data.Entity.Validation;

namespace Shop_Ban_Hang.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private Shop_Ban_HangEntities db = new Shop_Ban_HangEntities();

        // GET: Admin/Products
        public ActionResult Index(int? page, string searchstring, string product)
        {
			int pageSize = 9;
			int pageNumber = (page ?? 1);

			if (!String.IsNullOrEmpty(searchstring))
			{
				var cate = db.Products.Where(c => c.Name.Contains(searchstring)).ToList();


				return View(cate.OrderBy(o => o.Name).ToPagedList(pageNumber, pageSize));

			}

			ViewBag.Products = product;

			return View(db.Products.OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize));
		}

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
			var product = new Product();
			//product.ProductDate = DateTime.Now;
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "NameVN");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
		[ValidateInput(false)]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Name,UnitBrief,UnitPrice,Image,ProductDate,Available,Description,CategoryId,SupplierId,Quantity,Discount,Special,Latest,Views")] Product product)
        {
            if (ModelState.IsValid)
            {

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "NameVN", product.CategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "NameVN", product.CategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
		[ValidateInput(false)]
		[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UnitBrief,UnitPrice,Image,ProductDate,Available,Description,CategoryId,SupplierId,Quantity,Discount,Special,Latest,Views")] Product product)
        {
            

			try
			{
				if (ModelState.IsValid)
				{
					db.Entry(product).State = EntityState.Modified;
					db.SaveChanges();
					return RedirectToAction("Index");
				}
				ViewBag.CategoryId = new SelectList(db.Categories, "Id", "NameVN", product.CategoryId);
				ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", product.SupplierId);
				return View(product);
			}
			catch (DbEntityValidationException e)
			{
				foreach (var eve in e.EntityValidationErrors)
				{
					Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
						eve.Entry.Entity.GetType().Name, eve.Entry.State);
					foreach (var ve in eve.ValidationErrors)
					{
						Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
							ve.PropertyName, ve.ErrorMessage);
					}
				}
				throw;
			}
		}

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
