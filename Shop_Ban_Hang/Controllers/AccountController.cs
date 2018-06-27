using Shop_Ban_Hang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Ban_Hang.Controllers
{
    public class AccountController : Controller
    {
        Shop_Ban_HangEntities db = new Shop_Ban_HangEntities();

        // GET: Login
        public ActionResult Login()
        {
           
            return View();
        }


        //POST: 
        [HttpPost]
        public ActionResult Login(Customers customers)
        {
            if (ModelState.IsValid)
            {
                if(db.Customers.Any(c=>c.Id == customers._id) 
                    && db.Customers.Any(c=>c.Password == customers._password))
                {
                    var cus = db.Customers.Find(customers._id);
                    var cus_session = new User_Login();
                    cus_session._userName = cus.Id;
                    cus_session._password = cus.Password;

                    Session.Add("USER_SESSION", cus_session);

                    return RedirectToAction("Index","Cart");
                }
                else
                {
                    ModelState.AddModelError("", "User hoặc Password không đúng.");
                }
               
            }
               
            return View();
        }
		// GET: Đăng xuất
		public ActionResult Logout()
		{
			Session["USER_SESSION"] = null;
			return Redirect("/");
		}
		// GET: Tạo tài khoản
		public ActionResult Create_Customer()
		{

			return View();
		}

		// POST: Tạo tài khoản
		[HttpPost]
		public ActionResult Create_Customer(KhachHang khachHang)
		{
			ViewBag.abc = khachHang.Password;
			if (ModelState.IsValid)
			{
				if (db.Customers.Any(c => c.Id == khachHang.Id) == false)
				{
					Customer customer = new Customer();
					customer.Id = khachHang.Id;
					customer.Password = khachHang.Password;
					customer.Fullname = khachHang.Fullname;
					customer.Email = khachHang.Email;
					customer.Address = khachHang.Address;
					customer.Activated = false;
					customer.Photo = null;

					db.Customers.Add(customer);
					db.SaveChanges();
					ViewBag.thanhcong = true;
					return RedirectToAction("Index", "Home");

				}
				else
				{
					ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
				}

			}
			return View();
		}
	}
}