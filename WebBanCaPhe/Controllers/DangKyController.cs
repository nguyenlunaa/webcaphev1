using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebBanCaPhe.App_Start;
using WebBanCaPhe.Models;

namespace WebBanCaPhe.Controllers
{
    public class DangKyController : Controller
    {
        QuanLyCaPheEntities db = new QuanLyCaPheEntities();
        // GET: DangKy
        public ActionResult Index()
        {
            List<WebBanCaPhe.Models.ThanhVien> thanhviens = db.ThanhViens.ToList();
            return View(thanhviens);
        }

        [HttpPost]
        public ActionResult DangKyKhachHang(string TaiKhoan, string MatKhau, string HoTen, string DiaChi, string Email, string DienThoai)
        {
            //Kiểm tra mã tK
            WebBanCaPhe.Models.ThanhVien tvm = db.ThanhViens.Where(m => m.TaiKhoan == TaiKhoan).FirstOrDefault();
            if (tvm == null)
            {
                WebBanCaPhe.Models.ThanhVien tv = new Models.ThanhVien();
                tv.TaiKhoan = TaiKhoan;
                tv.MatKhau = GetMD5(MatKhau);
                tv.HoTen = HoTen;
                tv.DiaChi = DiaChi;
                tv.Email = Email;
                tv.DienThoai = DienThoai;
                db.ThanhViens.Add(tv);
                db.SaveChanges();
                TempData["Success"] = "Đăng Kí Thành Công!!";
                return new RedirectToRouteResult(new
                        RouteValueDictionary(
                        new
                        {
                            controller = "DangNhap",
                            action = "DangNhap",
                        }));
            }

            TempData["Error"] = "Tài Khoản Đã Tồn Tại!!";
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "DangKy",
                        action = "DangNhap",
                    }));
        }



        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}