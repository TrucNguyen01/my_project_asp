using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ProductController : Controller
    {
        Model1 db = new Model1();
        // GET: Product
    
        public ActionResult Index(int index = 1, string search = "", string value = "", string loai = "")
        {
            ViewBag.search = search;
            ViewBag.loai = loai;
            
            List<SanPham> ds = db.SanPhams.ToList();

            List<SanPham> ds_giamgia = ds.OrderByDescending(row => row.GiaGiam).ToList();
            ds_giamgia = ds_giamgia.Take(5).ToList();
            ViewBag.ds_giamgia = ds_giamgia;
            ds = ds.Where(row => row.TenSanPham.Contains(search)).ToList();
            if(loai == "dienthoai")
            {
                ds = ds.Where(row => row.LoaiSanPham == 1).ToList();
            }
            else if(loai == "maytinh")
            {
                ds = ds.Where(row => row.LoaiSanPham == 2).ToList();
            }
            else if(loai == "phukien")
            {
                ds = ds.Where(row => row.LoaiSanPham == 3).ToList();
            }

            if (value == "giatang")
            {
                ds = ds.OrderBy(row => row.GiaBan).ToList();
            }
            else if (value == "giagiam")
            {
                ds = ds.OrderByDescending(row => row.GiaBan).ToList();
            }
            else if (value == "banchay")
            {
                ds = ds.OrderByDescending(row => row.SoLuongMua).ToList();
            }
            else if(value == "moinhat")
            {
                ds = ds.OrderByDescending(row => row.NgayTao).ToList();
            }
            else if (value == "cunhat")
            {
                ds = ds.OrderBy(row => row.NgayTao).ToList();
            }
            else if (value == "tenaz")
            {
                ds = ds.OrderBy(row => row.TenSanPham).ToList();
            }
            else if (value == "tenza")
            {
                ds = ds.OrderByDescending(row => row.TenSanPham).ToList();
            }

            int soSanPham1Trang = 15;
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ds.Count()) / Convert.ToDouble(soSanPham1Trang)));
            int soSanPhamSkip = (index - 1) * soSanPham1Trang;
            ViewBag.Index = index;
            ViewBag.SoTrang = soTrang;
            ds = ds.Skip(soSanPhamSkip).Take(soSanPham1Trang).ToList();


            return View(ds);
        }
    }
}