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
    public class NhaCungCapController : Controller
    {
        QuanLyCaPheEntities db = new QuanLyCaPheEntities();
        // GET: Admin/NhaCungCap
        public ActionResult Index()
        {
            List<WebBanCaPhe.Models.NhaCungCap> nhacungcaps = db.NhaCungCaps.ToList();
            return View(nhacungcaps);
        }

        [HttpPost]
        public ActionResult ThemNhaCungCap( string TenNCC, string DiaChi, string Email, string DienThoai, string Fax)
        {
            //Kiểm tra mã tK
                WebBanCaPhe.Models.NhaCungCap ncc = new Models.NhaCungCap();
                ncc.TenNCC = TenNCC;
                ncc.DiaChi = DiaChi;
                ncc.Email = Email;
                ncc.DienThoai = DienThoai;
                ncc.Fax = Fax;
                db.NhaCungCaps.Add(ncc);
                db.SaveChanges();
                TempData["Success"] = "Thêm nhà cung cấp thành công!!";
                return new RedirectToRouteResult(new
                        RouteValueDictionary(
                        new
                        {
                            controller = "NhaCungCap",
                            action = "Index",
                            area = "Admin"
                        }));
           
        }


        public ActionResult XoaNhaCungCap(int id)
        {
            WebBanCaPhe.Models.NhaCungCap ncc = db.NhaCungCaps.Where(m => m.MaNCC == id).FirstOrDefault();
            db.NhaCungCaps.Remove(ncc);
            db.SaveChanges();
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "NhaCungCap",
                        action = "Index",
                        area = "Admin"
                    }));
        }



        public ActionResult SuaNhaCungCap(int id)
        {
            WebBanCaPhe.Models.NhaCungCap ncc = db.NhaCungCaps.Where(m => m.MaNCC == id).FirstOrDefault();
            return View(ncc);
        }

        [HttpPost]
        public ActionResult SuaNhaCungCap(int MaNCC, string TenNCC, string DiaChi, string Email, string DienThoai, string Fax)
        {
            WebBanCaPhe.Models.NhaCungCap ncc = db.NhaCungCaps.Where(m => m.MaNCC == MaNCC).FirstOrDefault();
            ncc.TenNCC = TenNCC;
            ncc.DiaChi = DiaChi;
            ncc.Email = Email;
            ncc.DienThoai = DienThoai;
            ncc.Fax = Fax;
            db.SaveChanges();
            TempData["Success"] = "Sửa nhà cung cấp thành công!!";
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "NhaCungCap",
                        action = "Index",
                        area = "Admin"
                    }));
        }
    }
}