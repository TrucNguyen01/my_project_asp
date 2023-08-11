using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using System.Text;

namespace WebApplication4.Areas.Admin.Controllers
{
    public class NhaCungCapController : Controller
    {
        // GET: Admin/NhaCungCap
        public ActionResult Index(int index = 1)
        {
            ViewBag.title = "Nhà cung cấp";
            ViewBag.url = "NhaCungCap";

            Model1 db = new Model1();
            List<NhaCungCap> ds = db.NhaCungCaps.ToList();

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

        public ActionResult Insert(NhaCungCap n)
        {
            Model1 db = new Model1();
            db.NhaCungCaps.Add(n);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            Model1 db = new Model1();
            NhaCungCap n = db.NhaCungCaps.Where(row => row.id_nhacungcap == id).FirstOrDefault();
            return View(n);
        }
        [HttpPost]
        public ActionResult Update(NhaCungCap n)
        {
            Model1 db = new Model1();
            NhaCungCap update_n = db.NhaCungCaps.Where(row => row.id_nhacungcap == n.id_nhacungcap).FirstOrDefault();

            update_n.TenNhaCungCap = n.TenNhaCungCap;
            update_n.SoDienThoai = n.SoDienThoai;
            update_n.DiaChi = n.DiaChi;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]

        public ActionResult Delete(int id)
        {
            Model1 db = new Model1();
            var nhacc = db.NhaCungCaps.Find(id);
            if (nhacc != null)
            {
                db.NhaCungCaps.Remove(nhacc);
                db.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        
    }
}