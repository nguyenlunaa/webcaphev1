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
    public class QLKhachHangController : Controller
    {
        QuanLyCaPheEntities db = new QuanLyCaPheEntities();
        // GET: Admin/QLKhachHang
        public ActionResult Index()
        {
            List<WebBanCaPhe.Models.ThanhVien> thanhviens = db.ThanhViens.ToList();
            return View(thanhviens);
        }


        [HttpPost]
        public ActionResult ThemKhachHang(string TaiKhoan, string MatKhau, string HoTen, string DiaChi, string Email, string DienThoai)
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
                TempData["Success"] = "Thêm nhân viên thành công!!";
                return new RedirectToRouteResult(new
                        RouteValueDictionary(
                        new
                        {
                            controller = "QLKhachHang",
                            action = "Index",
                            area = "Admin"
                        }));
            }

            TempData["Error"] = "Tài khoản thêm không hợp lệ!!";
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "QLKhachHang",
                        action = "Index",
                        area = "Admin"
                    }));
        }


        public ActionResult XoaKhachHang(string id)
        {
            WebBanCaPhe.Models.ThanhVien tv = db.ThanhViens.Where(m => m.TaiKhoan == id).FirstOrDefault();
            db.ThanhViens.Remove(tv);
            db.SaveChanges();
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "QLKhachHang",
                        action = "Index",
                        area = "Admin"
                    }));
        }



        public ActionResult SuaKhachHang(string id)
        {
            WebBanCaPhe.Models.ThanhVien tv = db.ThanhViens.Where(m => m.TaiKhoan == id).FirstOrDefault();
            return View(tv);
        }

        [HttpPost]
        public ActionResult SuaKhachHang(string MaThanhVien, string MatKhau, string HoTen, string DiaChi, string Email, string DienThoai)
        {
            WebBanCaPhe.Models.ThanhVien tv = db.ThanhViens.Where(m => m.TaiKhoan == MaThanhVien).FirstOrDefault();
            tv.MatKhau = GetMD5(MatKhau);
            tv.HoTen = HoTen;
            tv.DiaChi = DiaChi;
            tv.Email = Email;
            tv.DienThoai = DienThoai;
            db.SaveChanges();
            TempData["Success"] = "Sửa khách hàng thành công!!";
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "QLKhachHang",
                        action = "Index",
                        area = "Admin"
                    }));
        }




        //Mã Hóa Mật Khẩu
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