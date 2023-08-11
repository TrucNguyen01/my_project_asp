using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using System.Text;

namespace WebApplication4.Areas.Admin.Controllers
{
    public class GiamGiaController : Controller
    {
        // GET: Admin/GiamGia
        public ActionResult Index(int index = 1)
        {

            ViewBag.url = "GiamGia";
            ViewBag.title = "Mã giảm giá";

            Model1 db = new Model1();
            List<GiamGia> ds = db.GiamGias.ToList();

            int SoTinTucTren1Trang = 12;
            int SoTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ds.Count) / Convert.ToDouble(SoTinTucTren1Trang)));
            int SoTinTucSkip = (index - 1) * SoTinTucTren1Trang;

            ViewBag.Index = index;
            ViewBag.SoTrang = SoTrang;

            ds = ds.Skip(SoTinTucSkip).Take(SoTinTucTren1Trang).ToList();

            return View(ds);
        }

        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Insert(GiamGia g)
        {
            Model1 db = new Model1();
            db.GiamGias.Add(g);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            Model1 db = new Model1();
            GiamGia g = db.GiamGias.Where(row => row.id_magiamgia == id).FirstOrDefault();
            return View(g);
        }
        [HttpPost]
        public ActionResult Update(GiamGia g)
        {
            Model1 db = new Model1();
            var update_g = db.GiamGias.Where(row => row.id_magiamgia == g.id_magiamgia).FirstOrDefault();

            update_g.code = g.code;
            update_g.SoTienGiam = g.SoTienGiam;
            update_g.SoLanNhap = g.SoLanNhap;
            update_g.NgayHetHan = g.NgayHetHan;
            update_g.DonHangToiThieu = g.DonHangToiThieu;


            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Model1 db = new Model1();
            var giamgia = db.GiamGias.Find(id);
            if(giamgia != null)
            {
                db.GiamGias.Remove(giamgia);
                db.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}