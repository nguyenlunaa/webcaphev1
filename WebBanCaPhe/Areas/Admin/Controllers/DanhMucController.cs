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
    public class DanhMucController : Controller
    {
        QuanLyCaPheEntities db = new QuanLyCaPheEntities();
        // GET: Admin/DanhMuc
        public ActionResult Index()
        {
            List<WebBanCaPhe.Models.DanhMuc> danhmucs = db.DanhMucs.ToList();
            return View(danhmucs);
        }

        [HttpPost]
        public ActionResult ThemDanhMuc(string TenDanhMuc, string Url_friendly)
        {
            //Kiểm tra mã tK
            WebBanCaPhe.Models.DanhMuc dm = new Models.DanhMuc();
            dm.TenDanhMuc = TenDanhMuc;
            dm.Url_friendly = Url_friendly;
            db.DanhMucs.Add(dm);
            db.SaveChanges();
            TempData["Success"] = "Thêm danh mục thành công!!";
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "DanhMuc",
                        action = "Index",
                        area = "Admin"
                    }));

        }


        public ActionResult XoaDanhMuc(int id)
        {
            WebBanCaPhe.Models.DanhMuc dd = db.DanhMucs.Where(m => m.MaDanhMuc == id).FirstOrDefault();
            db.DanhMucs.Remove(dd);
            db.SaveChanges();
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "DanhMuc",
                        action = "Index",
                        area = "Admin"
                    }));
        }

        public ActionResult SuaDanhMuc(int id)
        {
            WebBanCaPhe.Models.DanhMuc dm = db.DanhMucs.Where(m => m.MaDanhMuc == id).FirstOrDefault();
            return View(dm);
        }

        [HttpPost]
        public ActionResult SuaDanhMuc(int MaDanhMuc, string TenDanhMuc, string Url_friendly)
        {
            WebBanCaPhe.Models.DanhMuc dm = db.DanhMucs.Where(m => m.MaDanhMuc == MaDanhMuc).FirstOrDefault();
            dm.TenDanhMuc = TenDanhMuc;
            dm.Url_friendly = Url_friendly;
            db.SaveChanges();
            TempData["Success"] = "Sửa danh mục thành công!!";
            return new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        controller = "DanhMuc",
                        action = "Index",
                        area = "Admin"
                    }));
        }
    }
}