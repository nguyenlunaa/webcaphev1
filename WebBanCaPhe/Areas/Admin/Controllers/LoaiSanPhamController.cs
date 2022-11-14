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
    public class LoaiSanPhamController : Controller
    {
        QuanLyCaPheEntities db = new QuanLyCaPheEntities();
        // GET: Admin/LoaiSanPham
        public ActionResult Index()
        {
            List<WebBanCaPhe.Models.LoaiSanPham> loaisanphams = db.LoaiSanPhams.ToList();
            return View(loaisanphams);
        }

        [HttpPost]
        public ActionResult ThemLoaiSanPham(string TenLoai, string Url_friendly, string HinhAnh)
        {
            //Kiểm tra mã tK
            WebBanCaPhe.Models.LoaiSanPham lsp = new Models.LoaiSanPham();
            lsp.TenLoai = TenLoai;
            lsp.Url_friendly = Url_friendly;
            lsp.HinhAnh = HinhAnh;
            db.LoaiSanPhams.Add(lsp);
            db.SaveChanges();
            TempData["Success"] = "Thêm loại sản phẩm thành công!!";
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "LoaiSanPham",
                        action = "Index",
                        area = "Admin"
                    }));

        }


        public ActionResult XoaLoaiSanPham(int id)
        {
            WebBanCaPhe.Models.LoaiSanPham lsp = db.LoaiSanPhams.Where(m => m.MaLoai == id).FirstOrDefault();
            db.LoaiSanPhams.Remove(lsp);
            db.SaveChanges();
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "LoaiSanPham",
                        action = "Index",
                        area = "Admin"
                    }));
        }

        public ActionResult SuaLoaiSanPham(int id)
        {
            WebBanCaPhe.Models.LoaiSanPham lsp = db.LoaiSanPhams.Where(m => m.MaLoai == id).FirstOrDefault();
            return View(lsp);
        }

        [HttpPost]
        public ActionResult SuaLoaiSanPham(int MaLoai ,string TenLoai, string Url_friendly, string HinhAnh)
        {
            WebBanCaPhe.Models.LoaiSanPham lsp = db.LoaiSanPhams.Where(m => m.MaLoai == MaLoai).FirstOrDefault();
            lsp.TenLoai = TenLoai;
            lsp.Url_friendly = Url_friendly;
            lsp.HinhAnh = HinhAnh;
            db.SaveChanges();
            TempData["Success"] = "Sửa Loại Sản Phẩm thành công!!";
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "LoaiSanPham",
                        action = "Index",
                        area = "Admin"
                    }));
        }

    }
}