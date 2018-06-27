using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop_Ban_Hang.Models;
using System.Web.Mvc;
using PagedList;

namespace Shop_Ban_Hang.Controllers
{
    public class ProductController : Controller
    {
        Shop_Ban_HangEntities db = new Shop_Ban_HangEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        

        // GET: Product
        public ActionResult GetProduct(int categoryID, int ? page)
        {

            if(categoryID != 0)

            {
                ViewBag.categoryID = categoryID;
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                var model = db.Products.Where(p => p.CategoryId == categoryID).OrderBy(u=>u.UnitPrice)
                      .ToPagedList(pageNumber, pageSize);
                return View(model);
            }

            return View();
        }

        // POST: Product
        [HttpPost]
        public ActionResult GetProduct(Product product)
        {
            return View();
        }

        ///lll
        /// <summary>
        /// Hiển thị chi tiết sản phẩm ( GET )
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Product_Detail(int productId=1002)
        {
            var model = db.Products.Find(productId);

            //Tăng số lần xem
            model.Views++;
            db.SaveChanges();

            //Lấy cookie cũ tên views
           var views = Request.Cookies["views"];
            // Nếu chưa có cookie cũ -> tạo mới
            if (views == null)
            {
                views = new HttpCookie("views");
            }
            // Bổ sung mặt hàng đã xem vào cookie
            views.Values[productId.ToString()] = productId.ToString();
            // Đặt thời hạn tồn tại của cookie
            views.Expires = DateTime.Now.AddMonths(1);
            // Gửi cookie về client để lưu lại
            Response.Cookies.Add(views);

            // Lấy List<int> chứa mã hàng đã xem từ cookie
            var keys = views.Values
                .AllKeys.Select(k => int.Parse(k)).ToList();
            // Truy vấn hàng đã xem
            ViewBag.Views = db.Products
                .Where(p => keys.Contains(p.Id)).OrderBy(p=>Guid.NewGuid()).Take(4);
            return View(model);
        }

        // GET: Search
        public ActionResult Search_Product(string keywords, int ? page )
        {
            ViewBag.keywords = keywords;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            if (keywords != null)
            {
                var model = db.Products.Where(p => p.Name.Contains(keywords)).OrderBy(u => u.UnitPrice)
                      .ToPagedList(pageNumber, pageSize);
                return View(model);
            }
            else
            {
                return View(db.Products.Where(p => p.Name.Contains("")).OrderBy(u => u.UnitPrice)
                    .ToPagedList(pageNumber, pageSize));
            }

        }


    }
}