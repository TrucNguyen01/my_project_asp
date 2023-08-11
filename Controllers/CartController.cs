using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class CartController : Controller
    {
        Model1 db = new Model1();
        // GET: Cart
        public ActionResult Index(int index = 1)
        {
            var user = Session["User"] as WebApplication4.Models.Account;
            int id = 0;
            if(user != null)
            {
                id = user.id_account;
            }
            List<GioHang> ds_giohang = db.GioHangs.Where(row => row.id_account == id && row.TrangThai == 1).ToList();
            Session["ProductCart"] = ds_giohang.Count();

            int soSanPham1Trang = 4;
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ds_giohang.Count()) / Convert.ToDouble(soSanPham1Trang)));
            int soSanPhamSkip = (index - 1) * soSanPham1Trang;

            ds_giohang = ds_giohang.Skip(soSanPhamSkip).Take(soSanPham1Trang).ToList();
            ViewBag.soTrang = soTrang;
            ViewBag.Index = index;

            ViewData["tong"] = ViewBag.tong;

            return View(ds_giohang);
        }
        [HttpPost]
        public ActionResult Add_Product(GioHang g)
        {
            var user = Session["User"] as WebApplication4.Models.Account;
            if (user == null)
            {
                return RedirectToAction("Login_Account", "Account");
            }


            var query = db.GioHangs.Where(row => row.id_sanpham == g.id_sanpham && row.TrangThai == 1 && row.id_account == user.id_account).FirstOrDefault();
            if(query == null)
            {
                g.id_account = user.id_account;
                g.TrangThai = 1;
                db.GioHangs.Add(g);
            }
            else
            {
                int soLuongMoi =  (int)g.SoLuongBan + (int)query.SoLuongBan;
                query.SoLuongBan = soLuongMoi;
            }


            var sp_Them = db.SanPhams.Find(g.id_sanpham);
            sp_Them.SoLuong = (int)sp_Them.SoLuong - (int)g.SoLuongBan;
            sp_Them.SoLuongMua = (int)sp_Them.SoLuongMua + (int)g.SoLuongBan;

            db.SaveChanges();


            
            List<GioHang> ds_giohang = db.GioHangs.Where(row => row.id_account == user.id_account && row.TrangThai == 1).ToList();
            Session["ProductCart"] = ds_giohang.Count();

            return RedirectToAction("Index/"+g.id_sanpham, "DetailProduct");
        }

        [HttpPost]
        public ActionResult Buy_Product(GioHang g)
        {
            var user = Session["User"] as WebApplication4.Models.Account;
            if (user == null)
            {
                return RedirectToAction("Login_Account", "Account");
            }


            var query = db.GioHangs.Where(row => row.id_sanpham == g.id_sanpham && row.TrangThai == 1 && row.id_account == user.id_account).FirstOrDefault();
            if (query == null)
            {
                g.id_account = user.id_account;
                g.TrangThai = 1;
                db.GioHangs.Add(g);
            }
            else
            {
                int soLuongMoi = (int)g.SoLuongBan + (int)query.SoLuongBan;
                query.SoLuongBan = soLuongMoi;
            }

            var sp_update = db.SanPhams.Find(g.id_sanpham);
            sp_update.SoLuong = (int)sp_update.SoLuong - (int)g.SoLuongBan;
            sp_update.SoLuongMua = (int)sp_update.SoLuongMua + (int)g.SoLuongBan;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete_Product_Cart(int id)
        {
            var spXoa = db.GioHangs.Find(id);
            if(spXoa != null)
            {
                var sp_update = db.SanPhams.Find(spXoa.id_sanpham);
                sp_update.SoLuong = (int)sp_update.SoLuong + (int)spXoa.SoLuongBan;
                sp_update.SoLuongMua = (int)sp_update.SoLuongMua - (int)spXoa.SoLuongBan;

                db.GioHangs.Remove(spXoa);
                db.SaveChanges();
                return Json( new { success = true});
            }
            return Json(new { success = false });
        }
      
        public ActionResult Total_Cart(string total)
        {
            var user = Session["User"] as WebApplication4.Models.Account;
            TempData["total"] = Convert.ToDouble(total);

            List<GioHang> ds_giohang = db.GioHangs.Where(row => row.id_account == user.id_account && row.TrangThai == 1).ToList();
            Session["ProductCart"] = ds_giohang.Count();

            return RedirectToAction("Comfirm_Info_User");
        }

        public ActionResult Comfirm_Info_User()
        {
            var user = Session["User"] as WebApplication4.Models.Account;
            List<GioHang> ds_giohang = db.GioHangs.Where(row => row.id_account == user.id_account && row.TrangThai == 1).ToList();
            Session["ProductCart"] = ds_giohang.Count();
            return View();
        }

        [HttpPost]
        public ActionResult Comfirm_Info_User(DonHang d, string codes)
        {
            var user = Session["User"] as WebApplication4.Models.Account;
            d.Email = user.Email;

            double TongTien = Convert.ToDouble(TempData["total"]);

            var query = db.GiamGias.Where(row => row.code == codes && row.TrangThai == 1).FirstOrDefault();
            if(query == null)
            {
                d.id_magiamgia = null;
            }
            else
            {
                d.id_magiamgia = query.id_magiamgia;
                query.SoLanDaNhap = (int)query.SoLanDaNhap + 1;
                if(query.SoLanDaNhap == query.SoLanNhap)
                {
                    query.TrangThai = 0;
                }
                TongTien = (double)TongTien - (double)query.SoTienGiam;
            }

            d.id_account = user.id_account;
            d.NgayTao = DateTime.Now;
            d.TrangThai = 1;
            d.TongTien = TongTien;

            db.DonHangs.Add(d);
            db.SaveChanges();

            var product_cart = db.GioHangs.Where(row => row.id_account == user.id_account && row.TrangThai == 1).ToList();
            foreach(var item in product_cart) {
                item.TrangThai = 0;
                item.id_donhang = d.id_DonHang;
            }
            db.SaveChanges();

            return RedirectToAction("Index", "Order");
        }
    }
}