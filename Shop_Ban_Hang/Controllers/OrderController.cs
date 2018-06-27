using Shop_Ban_Hang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop_Ban_Hang.Controllers
{
    public class OrderController : Controller
    {
        Shop_Ban_HangEntities db = new Shop_Ban_HangEntities();
        //
        // GET: /Order/
        public ActionResult Checkout()
        {
            var session = (User_Login)Session["USER_SESSION"];
            if (session != null)
            {
                var customer = db.Customers.Where(c => c.Id == session._userName).FirstOrDefault();
                var model = new Order();

                model.CustomerId = customer.Id;
                model.OrderDate = DateTime.Now;
                model.Receiver = customer.Fullname;
                model.Address = customer.Address;
                model.Amount = ShoppingCart.Cart.Total;

                return View(model);
            }
            else
            {
                return RedirectToAction("Login","Account");
                //ActionExecutingContext filterContext = new ActionExecutingContext();
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Login" }));
            }

        }

        public ActionResult Purchase(Order model)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(model);
                db.SaveChanges();

            }


            if (ModelState.IsValid)
            {
                var cart = ShoppingCart.Cart;
                foreach (var p in cart.Items)
                {
                    var d = new OrderDetail
                    {
                        Order = model,
                        ProductId = p.Id,
                        UnitPrice = p.UnitPrice,
                        Discount = p.Discount,
                        Quantity = p.Quantity
                    };

                    db.OrderDetails.Add(d);

                }
                db.SaveChanges();

                // Thanh toán trực tuyến
                //var api = new WebApiClient<AccountInfo>();
                //var data = new AccountInfo { 
                //    Id=Request["BankAccount"],
                //    Balance = cart.Total
                //};
                //api.Put("api/Bank/nn", data);
            }

            return RedirectToAction("Detail", new { model.Id });
           
        }

        public ActionResult Detail(int id)
        {

            var order = db.Orders.Find(id);
            //var order = db.Orders.Where(o => o.Id == id).ToList();
            return View(order);
        }

        public ActionResult List()
        {
            var orders = db.Orders
                .Where(o => o.CustomerId == User.Identity.Name);
            return View(orders);
        }
    }
}