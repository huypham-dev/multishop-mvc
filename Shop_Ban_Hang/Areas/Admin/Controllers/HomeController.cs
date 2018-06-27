using Shop_Ban_Hang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Ban_Hang.Areas.Admin.Controllers
{
	
    public class HomeController : Controller
    {
		Shop_Ban_HangEntities db = new Shop_Ban_HangEntities();
		// GET: Admin/Home
		public ActionResult Index()
        {
            return View();
        }
		public ActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Login(string username, string password)
		{			
			var user = db.Administrators.SingleOrDefault(x => x.Username == username && x.Password == password);
			if (user!=null)
			{
				Session["username"] = user.Username;
				Session["password"] = user.Password;
				Session["fullname"] = user.Fullname;
				Session["avatar"] = user.Avatar;
				return RedirectToAction("Index");
			}
			ViewBag.error = "Sai tài khoản hoặc mật khẩu !!!";
			return View();
		}

		



		//public ActionResult Logout()
		//{
		//	Session["username"] = null;
		//	Session["password"] = null;
		//	Session["fullname"] = null;
		//	Session["avatar"] = null;
		//	return RedirectToAction("Login");
		//}
	}
}