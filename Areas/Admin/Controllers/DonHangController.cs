using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/DonHang
        public ActionResult Index(int index = 1)
        {
            ViewBag.title = "Đơn hàng";

            List<DonHang> ds_donhang = db.DonHangs.Where(row => row.TrangThai == 1 || row.TrangThai == 2).ToList();


            int SoDonHang1Trang = 8;
            int TongSoSanPham = ds_donhang.Count();
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(TongSoSanPham) / Convert.ToDouble(SoDonHang1Trang)));

            int skip_donhang = (index - 1) * SoDonHang1Trang;

            ViewBag.SoTrang = soTrang;
            ViewBag.Index = index;

            ds_donhang = ds_donhang.Skip(skip_donhang).Take(SoDonHang1Trang).ToList();


            return View(ds_donhang);
        }

        public ActionResult DonHangThanhCong(int index = 1)
        {

            List<DonHang> ds_donhang = db.DonHangs.Where(row => row.TrangThai == 3).ToList();


            int SoDonHang1Trang = 8;
            int TongSoSanPham = ds_donhang.Count();
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(TongSoSanPham) / Convert.ToDouble(SoDonHang1Trang)));

            int skip_donhang = (index - 1) * SoDonHang1Trang;

            ViewBag.SoTrang = soTrang;
            ViewBag.Index = index;

            ds_donhang = ds_donhang.Skip(skip_donhang).Take(SoDonHang1Trang).ToList();


            return View(ds_donhang);
        }

        public ActionResult DonHangHuy(int index = 1)
        {

            List<DonHang> ds_donhang = db.DonHangs.Where(row => row.TrangThai == 0).ToList();


            int SoDonHang1Trang = 8;
            int TongSoSanPham = ds_donhang.Count();
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(TongSoSanPham) / Convert.ToDouble(SoDonHang1Trang)));

            int skip_donhang = (index - 1) * SoDonHang1Trang;

            ViewBag.SoTrang = soTrang;
            ViewBag.Index = index;

            ds_donhang = ds_donhang.Skip(skip_donhang).Take(SoDonHang1Trang).ToList();


            return View(ds_donhang);
        }

        
        public ActionResult ChiTietDonHang(int id)
        {
            var order = db.DonHangs.Find(id);

            var list_product_order = db.GioHangs.Where(row => row.id_donhang == id && row.TrangThai == 0).ToList();
            ViewBag.list_product_order = list_product_order.ToList();

            if (order.id_magiamgia != null)
            {
                var giamgia = db.GiamGias.Where(row => row.id_magiamgia == order.id_magiamgia).FirstOrDefault();
                ViewBag.GiamGia = giamgia.SoTienGiam;
            }
            else
            {
                ViewBag.GiamGia = 0;
            }

            return View(order);
        }

        public ActionResult ChangeStatus(int id)
        {
            var donhang = db.DonHangs.Find(id);
            if(donhang != null)
            {
                donhang.TrangThai = 2;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}