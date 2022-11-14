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

namespace WebBanCaPhe.Areas.Admin.Controllers
{
    [AdminAuthorize]
    [RootAdminAuthorize]
    public class TaiKhoanController : Controller
    {
        QuanLyCaPheEntities db = new QuanLyCaPheEntities();
        // GET: Admin/TaiKhoan
        public ActionResult Index()
        {
            List<WebBanCaPhe.Models.Admin> admins = db.Admins.ToList();
            return View(admins);
        }

        [HttpPost]
        public ActionResult ThemTaiKhoan(string TaiKhoan, string MatKhau, string HoTen, string Email, string DiaChi)
        {
            //Kiểm tra mã tK
            WebBanCaPhe.Models.Admin adm = db.Admins.Where(m => m.TaiKhoan == TaiKhoan).FirstOrDefault();
            if (adm == null)
            {
                WebBanCaPhe.Models.Admin ad = new Models.Admin();
                ad.TaiKhoan = TaiKhoan;
                ad.MatKhau = GetMD5(MatKhau);
                ad.HoTen = HoTen;
                ad.Email = Email;
                ad.DiaChi = DiaChi;
                ad.Role = "Admin";
                db.Admins.Add(ad);
                db.SaveChanges();
                TempData["Success"] = "Thêm nhân viên thành công!!";
                return new RedirectToRouteResult(new
                        RouteValueDictionary(
                        new
                        {
                            controller = "TaiKhoan",
                            action = "Index",
                            area = "Admin"
                        }));
            }

            TempData["Error"] = "Tài khoản thêm không hợp lệ!!";
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "TaiKhoan",
                        action = "Index",
                        area = "Admin"
                    }));

        }

        //Xóa Tài Khoản
        public ActionResult XoaTaiKhoan(string id)
        {
            WebBanCaPhe.Models.Admin ad = db.Admins.Where(m => m.TaiKhoan == id).FirstOrDefault();
            db.Admins.Remove(ad);
            db.SaveChanges();
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "TaiKhoan",
                        action = "Index",
                        area = "Admin"
                    }));
        }


        //Sửa Tài Khoản
        public ActionResult SuaTaiKhoan(string id)
        {
            WebBanCaPhe.Models.Admin ad = db.Admins.Where(m => m.TaiKhoan == id).FirstOrDefault();
            return View(ad);
        }

        [HttpPost]
        public ActionResult SuaTaiKhoan(string TaiKhoan, string MatKhau, string HoTen, string Email, string DiaChi)
        {
            WebBanCaPhe.Models.Admin ad = db.Admins.Where(m => m.TaiKhoan == TaiKhoan).FirstOrDefault();
            ad.MatKhau = GetMD5(MatKhau);
            ad.HoTen = HoTen;
            ad.Email = Email;
            ad.DiaChi = DiaChi;
            ad.Role = "Admin";
            db.SaveChanges();
            TempData["Success"] = "Sửa nhân viên thành công!!";
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "TaiKhoan",
                        action = "Index",
                        area = "Admin"
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