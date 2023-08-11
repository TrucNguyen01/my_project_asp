using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Controllers
{
    public class OrderController : Controller
    {
        Model1 db = new Model1();
        // GET: Order
        public ActionResult Index()
        {
            var user = Session["User"] as WebApplication4.Models.Account;
            int id = 0;
            if (user != null)
            {
                id = user.id_account;
            }

            List<DonHang> ds_donhang = db.DonHangs.Where(row => row.id_account == id && (row.TrangThai == 1 || row.TrangThai == 2)).ToList();
            return View(ds_donhang);
        }

        [HttpPost]
        public ActionResult Delete_Order(int id)
        {
            var order = db.DonHangs.Find(id);
            if(order != null)
            {
                var cart = db.GioHangs.Where(row => row.id_donhang == id).ToList();
                foreach(var item in cart)
                {
                    int SoLuongBan = (int)item.SoLuongBan;
                    var SanPham = db.SanPhams.Find(item.id_sanpham);
                    SanPham.SoLuong = SanPham.SoLuong + SoLuongBan;
                    SanPham.SoLuongMua = SanPham.SoLuongMua - SoLuongBan;

                    item.TrangThai = 0;
                    db.SaveChanges();
                }


                order.Account.SoLanHuyHang = (int)order.Account.SoLanHuyHang + 1;
                order.TrangThai = 0;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        
        public ActionResult Order_Detail(int id)
        {
            var order = db.DonHangs.Find(id);

            var list_product_order = db.GioHangs.Where(row => row.id_donhang == id && row.TrangThai == 0).ToList();
            ViewBag.list_product_order = list_product_order.ToList();

            if(order.id_magiamgia != null)
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

        [HttpPost]
        public ActionResult ConfirmOrder(int id) 
        {
            var order = db.DonHangs.Find(id);
            if (order != null)
            {
                order.Account.SoLanMuaHang = (int)order.Account.SoLanMuaHang + 1;
                order.TrangThai = 3;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult DonHangThanhCong(int index = 1)
        {
            var user = Session["User"] as WebApplication4.Models.Account;
            var list_order = db.DonHangs.Where(row => row.TrangThai == 3 && row.id_account == user.id_account).ToList();

            int SoDonHang1Trang = 10;
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(list_order.Count()) / Convert.ToDouble(SoDonHang1Trang)));
            int soDonHangSkip = (index - 1) * SoDonHang1Trang;

            list_order = list_order.Skip(soDonHangSkip).Take(SoDonHang1Trang).ToList();
            ViewBag.soTrang = soTrang;
            ViewBag.Index = index;

            return View(list_order);
        }
    }
}