using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using System.Text;

namespace WebApplication4.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        // GET: Admin/News
        public ActionResult Index(int index = 1)
        {
            ViewBag.url = "News";
            ViewBag.title = "Tin tức";
            Model1 db = new Model1();
            List<TinTuc> ds = db.TinTucs.ToList();

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
        [ValidateInput(false)]
        public ActionResult Insert(TinTuc t)
        {
            Model1 db = new Model1();
            t.TrangThai = 1;
            db.TinTucs.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            Model1 db = new Model1();
            TinTuc t = db.TinTucs.Where(row => row.id_tintuc == id).FirstOrDefault();
            return View(t);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(TinTuc t)
        {
            Model1 db = new Model1();
            var update_t = db.TinTucs.Where(row => row.id_tintuc == t.id_tintuc).FirstOrDefault();
            update_t.HinhAnh = t.HinhAnh;
            update_t.TieuDe = t.TieuDe;
            update_t.NoiDung = t.NoiDung;


            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]

        public ActionResult Delete(int id)
        {
            Model1 db = new Model1();
            var tintuc = db.TinTucs.Find(id);
            if(tintuc != null)
            {
                db.TinTucs.Remove(tintuc);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}