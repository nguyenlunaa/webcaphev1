using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanCaPhe.App_Start;

namespace WebBanCaPhe.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class SanPhamController : Controller
    {
        // GET: Admin/SanPham
        public ActionResult Index()
        {
            ViewBag.TitleAction = "Danh sách sản phẩm";
            return View();
        }
        [RootAdminAuthorize]
        public ActionResult Create()
        {
            return View();
        }
    }
}