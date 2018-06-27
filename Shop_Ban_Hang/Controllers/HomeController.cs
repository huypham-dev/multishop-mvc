using PagedList;
using Shop_Ban_Hang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Ban_Hang.Controllers
{
    public class HomeController : Controller
    {
        Shop_Ban_HangEntities db = new Shop_Ban_HangEntities();

		public ActionResult Index()
        {
            var model = db.Categories.Where(c => c.Products.Count >= 4).ToList(); //Moi loai san pham thi lay 4 san pham

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Category()
        {
            var model = db.Categories;
            return PartialView("_Category", model);
        }

        public ActionResult Special()
        {
            var model = db.Products.Where(p => p.Special == true).OrderBy(p => Guid.NewGuid()).Take(4);
            return PartialView("_Special", model);
        }

        public ActionResult Saleoff()
        {
            
            var model = db.Products.Where(p => p.Discount > 0).OrderBy(p=>Guid.NewGuid()).Take(3);
            //ViewBag.List_Product = db.Products.Where(p => p.Discount > 0).Take(1);
            return PartialView("_Saleoff", model);
        }

        //Chi tiết Saleoff
        public ActionResult Saleoff_Detail(int ? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var model = db.Products.Where(p => p.Discount > 0).OrderBy(u=>u.UnitPrice)
               .ToPagedList(pageNumber,pageSize);
            return View(model);
        }

        //Tìm kiếm ( hiển thị ra Menu Search )
        public ActionResult Search()
        {
            var name = Request["term"];

            var data = db.Products
                .Where(p => p.Name.Contains(name))
                .Select(p => p.Name).OrderBy(p=>Guid.NewGuid()).Take(10);
            return Json(data, JsonRequestBehavior.AllowGet);
        }




    }
}