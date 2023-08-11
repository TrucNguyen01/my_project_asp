using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Areas.Admin.Controllers
{
    public class BinhLuanController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/BinhLuan
        public ActionResult Index(int index = 1)
        {
            ViewBag.title = "Bình luận";
            List<Comment> ds_comment = db.Comments.Where(row => row.TrangThai == 1).ToList();

            int SoBinhLuan1Trang = 8;
            int SoTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ds_comment.Count) / Convert.ToDouble(SoBinhLuan1Trang)));
            int SoTinTucSkip = (index - 1) * SoBinhLuan1Trang;

            ViewBag.Index = index;
            ViewBag.SoTrang = SoTrang;

            ds_comment = ds_comment.Skip(SoTinTucSkip).Take(SoBinhLuan1Trang).ToList();
            return View(ds_comment);
        }

        public ActionResult PhanHoi(int id)
        {
            TempData["id_comment"] = id;
            var phanhoi = db.Comments.Find(id);
            return View(phanhoi);
        }
        [HttpPost]
        public ActionResult PhanHoi(PhanHoi p)
        {
            int id_comment = Convert.ToInt32(TempData["id_comment"]);
            p.NgayPhanHoi = DateTime.Now;
            p.id_comment = id_comment;
            p.id_account = 2;
            db.PhanHois.Add(p);

            var comment = db.Comments.Find(id_comment);
            comment.TrangThai = 0;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}