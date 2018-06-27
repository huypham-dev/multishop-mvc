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
	public class CategoriesController : Controller
	{
		private Shop_Ban_HangEntities db = new Shop_Ban_HangEntities();

		// GET: Admin/Categories
		public ActionResult Index(int? page, string searchstring, string categorie)
		{
			int pageSize = 4;
			int pageNumber = (page ?? 1);	

			if (!String.IsNullOrEmpty(searchstring))
			{
				var cate = db.Categories.Where(c => c.NameVN.Contains(searchstring)).ToList();

				
				return View(cate.OrderBy(o => o.NameVN).ToPagedList(pageNumber, pageSize));

			}

			ViewBag.Categories = categorie;

			return View(db.Categories.OrderBy(x => x.NameVN).ToPagedList(pageNumber, pageSize));
		}


		//public ActionResult Search(string searchstring)
		//{
		//	var cate = db.Categories.ToList();
		//	if (!String.IsNullOrEmpty(searchstring))
		//	{
		//		cate = db.Categories.Where(c=>c.NameVN.Contains(searchstring)).ToList();
		//		return View(cate.ToList());

		//	}
		//	return View(cate);
		//}

		// GET: Admin/Categories/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Category category = db.Categories.Find(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}

		// GET: Admin/Categories/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Admin/Categories/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,NameVN,Name,Icon")] Category category)
		{
			if (ModelState.IsValid)
			{
				db.Categories.Add(category);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(category);
		}

		// GET: Admin/Categories/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Category category = db.Categories.Find(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}

		// POST: Admin/Categories/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,NameVN,Name,Icon")] Category category)
		{
			if (ModelState.IsValid)
			{
				db.Entry(category).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(category);
		}

		// GET: Admin/Categories/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Category category = db.Categories.Find(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}

		// POST: Admin/Categories/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Category category = db.Categories.Find(id);
			db.Categories.Remove(category);
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
