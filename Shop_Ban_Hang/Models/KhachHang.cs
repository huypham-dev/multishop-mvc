using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop_Ban_Hang.Models
{
	public class KhachHang
	{
		[Display(Name = "Tên đăng nhập")]
		[StringLength(50)]
		[Required(ErrorMessage = "{0} không được để trống.")]
		public string Id { get; set; }

		[Display(Name = "Mật khẩu")]
		[DataType(DataType.Password)]
		[StringLength(50, MinimumLength = 3)]
		[Required(ErrorMessage = "{0} không được để trống.")]
		public string Password { get; set; }

		[Display(Name = "Tên đầy đủ")]
		[StringLength(50)]
		[Required(ErrorMessage = "{0} không được để trống.")]
		public string Fullname { get; set; }

		[Display(Name = "Email")]
		[EmailAddress]
		[StringLength(50)]
		[Required(ErrorMessage = "{0} không được để trống.")]
		public string Email { get; set; }

		[Display(Name = "Địa chỉ")]
		[StringLength(500)]
		[Required(ErrorMessage = "{0} không được để trống.")]
		public string Address { get; set; }
	}
}