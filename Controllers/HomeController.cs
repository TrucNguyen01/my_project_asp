using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        public ActionResult Index()
        {
            List<SanPham> ds_giamgia = db.SanPhams.OrderByDescending(row => row.GiaGiam).ToList();
            ds_giamgia = ds_giamgia.Take(12).ToList();
            ViewBag.ds_giamgia = ds_giamgia;
            List<SanPham> ds_banchay = (from sp in db.SanPhams
                                        orderby sp.SoLuongMua descending
                                        where sp.LoaiSanPham == 1
                                        select sp).ToList();
                                       
            ds_banchay = ds_banchay.Take(18).ToList();
            ViewBag.ds_banchay = ds_banchay;

            List<SanPham> ds_noibat = (from sp in db.SanPhams
                                        orderby sp.DoHot descending
                                        where sp.LoaiSanPham == 1
                                        select sp).ToList();
            ds_noibat = ds_noibat.Take(18).ToList();
            ViewBag.ds_noibat = ds_noibat;

            List<SanPham> ds_maytinh = db.SanPhams.Where(row => row.LoaiSanPham == 2).ToList();
            ds_maytinh = ds_maytinh.Take(24).ToList();
            ViewBag.ds_maytinh = ds_maytinh.ToList();

            List<SanPham> ds_phukien = db.SanPhams.Where(row => row.LoaiSanPham == 3).ToList();
            ds_phukien = ds_phukien.Take(18).ToList();
            ViewBag.ds_phukien = ds_phukien.ToList();


            var user = Session["User"] as WebApplication4.Models.Account;
            int id = 0;
            if (user != null)
            {
                id = user.id_account;
            }
            List<GioHang> ds_giohang = db.GioHangs.Where(row => row.id_account == id && row.TrangThai == 1).ToList();
            Session["ProductCart"] = ds_giohang.Count();

            return View();
        }

    }
}