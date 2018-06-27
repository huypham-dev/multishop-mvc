using Shop_Ban_Hang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Ban_Hang.Areas.Admin.Controllers
{
    public class RevenueController : Controller
    {
		//GET: Admin/Revenue
		public ActionResult Index()
		{
			return View();
		}
		Shop_Ban_HangEntities db = new Shop_Ban_HangEntities();

		public ActionResult byProduct(DateTime? Min = null, DateTime? Max = null)
		{
			if (Min == null)
			{
				Min = DateTime.MinValue;
			}
			if (Max == null)
			{
				Max = DateTime.MaxValue;
			}

			var model = db.OrderDetails
				.Where(d => d.Order.OrderDate >= Min && d.Order.OrderDate <= Max)
				.GroupBy(d => d.Product)
				.Select(g => new ReportInfo
				{
					Group = g.Key.Name,
					Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
					Count = g.Sum(d => d.Quantity),
					Min = g.Min(d => d.UnitPrice),
					Max = g.Max(d => d.UnitPrice),
					Avg = g.Average(d => d.UnitPrice)
				});
			ViewBag.Min = Min;
			ViewBag.Max = Max;
			return View("Index", model);
		}

		public ActionResult byCategory(DateTime? Min = null, DateTime? Max = null)
		{
			if (Min == null)
			{
				Min = DateTime.MinValue;
			}
			if (Max == null)
			{
				Max = DateTime.MaxValue;
			}

			var model = db.OrderDetails
				.Where(d => d.Order.OrderDate >= Min && d.Order.OrderDate <= Max)
				.GroupBy(d => d.Product.Category)
				.Select(g => new ReportInfo
				{
					Group = g.Key.NameVN,
					Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
					Count = g.Sum(d => d.Quantity),
					Min = g.Min(d => d.UnitPrice),
					Max = g.Max(d => d.UnitPrice),
					Avg = g.Average(d => d.UnitPrice)
				});
			ViewBag.Min = Min;
			ViewBag.Max = Max;
			return View("Index", model);
		}

		public ActionResult bySupplier()
		{
			var model = db.OrderDetails
				.GroupBy(d => d.Product.Supplier)
				.Select(g => new ReportInfo
				{
					Group = g.Key.Name,
					Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
					Count = g.Sum(d => d.Quantity),
					Min = g.Min(d => d.UnitPrice),
					Max = g.Max(d => d.UnitPrice),
					Avg = g.Average(d => d.UnitPrice)
				});
			return View("Index", model);
		}

		public ActionResult byCustomer(DateTime? Min = null, DateTime? Max = null)
		{
			if (Min == null)
			{
				Min = DateTime.MinValue;
			}
			if (Max == null)
			{
				Max = DateTime.MaxValue;
			}

			var model = db.OrderDetails
				.Where(d => d.Order.OrderDate >= Min && d.Order.OrderDate <= Max)
				.GroupBy(d => d.Order.Customer)
				.Select(g => new ReportInfo
				{
					Group = g.Key.Fullname,
					Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
					Count = g.Sum(d => d.Quantity),
					Min = g.Min(d => d.UnitPrice),
					Max = g.Max(d => d.UnitPrice),
					Avg = g.Average(d => d.UnitPrice)
				});
			ViewBag.Min = Min;
			ViewBag.Max = Max;
			return View("Index", model);
		}

		public ActionResult byYear()
		{

			var model = db.OrderDetails
				.GroupBy(d => d.Order.OrderDate.Year)
				.Select(g => new ReportInfo
				{
					iGroup = g.Key,
					Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
					Count = g.Sum(d => d.Quantity),
					Min = g.Min(d => d.UnitPrice),
					Max = g.Max(d => d.UnitPrice),
					Avg = g.Average(d => d.UnitPrice)
				})
				.OrderBy(i => i.iGroup);
			ViewBag.Year = "Năm ";
			return View("Index", model);
		}

		public ActionResult byMonth()
		{
			var model = db.OrderDetails
				.GroupBy(d => d.Order.OrderDate.Month)
				.Select(g => new ReportInfo
				{
					iGroup = g.Key,
					Year = g.Min(d => d.Order.OrderDate.Year),
					Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
					Count = g.Sum(d => d.Quantity),
					Min = g.Min(d => d.UnitPrice),
					Max = g.Max(d => d.UnitPrice),
					Avg = g.Average(d => d.UnitPrice)
				})
				.OrderBy(i => i.iGroup);
			ViewBag.Month = "Tháng ";
			return View("Index", model);
		}

		public ActionResult byQuarter()
		{
			var model = db.OrderDetails
				.GroupBy(d => (d.Order.OrderDate.Month - 1) / 3 + 1)
				.Select(g => new ReportInfo
				{
					iGroup = g.Key,
					Year = g.Min(d => d.Order.OrderDate.Year),
					Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
					Count = g.Sum(d => d.Quantity),
					Min = g.Min(d => d.UnitPrice),
					Max = g.Max(d => d.UnitPrice),
					Avg = g.Average(d => d.UnitPrice)
				})
				.OrderBy(i => i.iGroup);
			ViewBag.Quarter = "Qúy ";
			return View("Index", model);
		}
	}
}