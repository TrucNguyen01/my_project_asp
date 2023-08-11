using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/SanPham
       
        public ActionResult Index(int index = 1, string search = "", string value = "")
        {
            ViewBag.url = "SanPham";
            ViewBag.title = "Sản phẩm";


            List<SanPham> ds = db.SanPhams.Where(row => row.TenSanPham.Contains(search)).ToList();
            if(value == "dienthoai")
            {
                ds = ds.Where(row => row.LoaiSanPham == 1).ToList();
            }
            else if(value == "maytinh")
            {
                ds = ds.Where(row => row.LoaiSanPham == 2).ToList();
            }
            else if (value == "phukien")
            {
                ds = ds.Where(row => row.LoaiSanPham == 3).ToList();
            }
            else if(value == "giatang")
            {
                ds = ds.OrderBy(row => row.GiaBan).ToList();
            }
            else if(value == "giagiam")
            {
                ds = ds.OrderByDescending(row => row.GiaBan).ToList();
            }
            else if(value == "nhaphang")
            {
                ds = ds.OrderBy(row => row.SoLuong).ToList();
            }
            else if (value == "banchay")
            {
                ds = ds.OrderByDescending(row => row.SoLuongMua).ToList();
            }

            int SoSanPham1Trang = 6;
            int TongSoSanPham = ds.Count();
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(TongSoSanPham) / Convert.ToDouble(SoSanPham1Trang)));

            int skip_sanpham = (index - 1) * SoSanPham1Trang;

            ViewBag.SoTrang = soTrang;
            ViewBag.Index = index;
            ViewBag.search = search;
            ViewBag.value = value;

            ds = ds.Skip(skip_sanpham).Take(SoSanPham1Trang).ToList();


            return View(ds);
        }
        public ActionResult Insert()
        {
            List<LoaiSP> loaisp = db.LoaiSPs.ToList();
            ViewBag.loaisanpham = loaisp;

            List<NhaCungCap> nhacc = db.NhaCungCaps.ToList();
            ViewBag.nhacungcap = nhacc;
            return View();
        }

        [HttpPost]
        public ActionResult Insert(SanPham s)
        {
            if(ModelState.IsValid)
            {
                s.GiaBan = s.Gia - s.GiaGiam;
                db.SanPhams.Add(s);
                db.SaveChanges();

                if (s.LoaiSanPham == 3)
                {
                    return RedirectToAction("Index");
                }

                List<SanPham> ds = db.SanPhams.ToList();

                CauHinh c = new CauHinh();
                c.id_sanpham = ds[ds.Count() - 1].id_sanpham;
                db.CauHinhs.Add(c);
                db.SaveChanges();
                return RedirectToAction("Update_CauHinh_SanPham/" + c.id_cauhinh);
            }
            return RedirectToAction("Insert");
        }

        
        public ActionResult Update(int id)
        {
            SanPham s = db.SanPhams.Where(row => row.id_sanpham == id).FirstOrDefault();

            List<LoaiSP> loaisp = db.LoaiSPs.ToList();
            ViewBag.loaisanpham = loaisp;

            List<NhaCungCap> nhacc = db.NhaCungCaps.ToList();
            ViewBag.nhacungcap = nhacc;

            return View(s);
        }
        [HttpPost]
        public ActionResult Update(SanPham s)
        {
            var update_s = db.SanPhams.Where(row => row.id_sanpham == s.id_sanpham).FirstOrDefault();
            update_s.Avatar = s.Avatar;
            update_s.TenSanPham = s.TenSanPham;
            update_s.LoaiSanPham = s.LoaiSanPham;
            update_s.MoTa = s.MoTa;
            update_s.SoLuong = s.SoLuong;
            update_s.Gia = s.Gia;
            update_s.GiaGiam = s.GiaGiam;
            update_s.NhaCungCap = s.NhaCungCap;
            update_s.DoHot = s.DoHot;
            update_s.GiaBan = s.Gia - s.GiaGiam;
            db.SaveChanges();



            var query = (from sanpham in db.SanPhams
                         join cauhinh in db.CauHinhs on sanpham.id_sanpham equals cauhinh.id_sanpham
                         where sanpham.id_sanpham == s.id_sanpham
                         select new
                         {
                             id = cauhinh.id_cauhinh
                         }
                         ).FirstOrDefault();
                        
  


            return RedirectToAction("Update_CauHinh_SanPham/" + query.id);
        }

        public ActionResult Update_CauHinh_SanPham(int id)
        {
            CauHinh c = db.CauHinhs.Where(row => row.id_cauhinh == id).FirstOrDefault();
            return View(c);
        }
        
        [HttpPost]

        public ActionResult Update_CauHinh_SanPham(CauHinh c)
        {
            CauHinh cauhinh = db.CauHinhs.Where(row => row.id_cauhinh == c.id_cauhinh).FirstOrDefault();
            cauhinh.ManHinh = c.ManHinh;
            cauhinh.DoPhanGiai = c.DoPhanGiai;
            cauhinh.CongNgheManHinh = c.CongNgheManHinh;
            cauhinh.RAM = c.RAM;
            cauhinh.BoNho = c.BoNho;
            cauhinh.ChatLieu = c.ChatLieu;
            cauhinh.KichThuoc = c.KichThuoc;
            cauhinh.TrongLuong = c.TrongLuong;
            cauhinh.Camera = c.Camera;
            cauhinh.Pin = c.Pin;
            cauhinh.CPU = c.CPU;
            cauhinh.HinhAnh_CPU = c.HinhAnh_CPU;
            cauhinh.HinhAnh_RAM = c.HinhAnh_RAM;
            cauhinh.HinhAnh_Camera = c.HinhAnh_Camera;
            cauhinh.HinhAnh_Pin = c.HinhAnh_Pin;
            cauhinh.NoiDung_CPU = c.NoiDung_CPU;
            cauhinh.NoiDung_RAM = c.NoiDung_RAM;
            cauhinh.NoiDung_Camera = c.NoiDung_Camera;
            cauhinh.NoiDung_Pin = c.NoiDung_Pin;
            cauhinh.Chip = c.Chip;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var sanpham = db.SanPhams.Find(id);
            var cauhinh = db.CauHinhs.Where(row => row.id_sanpham == id).FirstOrDefault();
            if(sanpham != null)
            {
                db.CauHinhs.Remove(cauhinh);
                db.SaveChanges();
                db.SanPhams.Remove(sanpham);
                db.SaveChanges();

                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}