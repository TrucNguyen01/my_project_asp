using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Controllers
{
    public class DetailProductController : Controller
    {
        Model1 db = new Model1();
        // GET: DetailProduct
        public ActionResult Index(int id)
        {
            TempData["id_sanpham"] = id;
            var sp = db.SanPhams.Where(row => row.id_sanpham == id).FirstOrDefault();
            List<Comment> ds_comment = db.Comments.Where(row => row.id_sanpham == id).ToList();
            ViewBag.ds_comment = ds_comment.ToList();

            var phanhoi_comment = (from p in db.PhanHois
                                   join c in db.Comments
                                   on p.id_comment equals c.id_comment
                                   where c.id_sanpham == id && c.id_comment == p.id_comment
                                   select p).ToList();
            ViewBag.phanhoi_comment = phanhoi_comment.ToList();
           
            if (sp.LoaiSanPham != 3)
            {
                var ch = (from cauhinh in db.CauHinhs
                          join sanpham in db.SanPhams
                          on cauhinh.id_sanpham equals sanpham.id_sanpham
                          where cauhinh.id_sanpham == id
                          select cauhinh).FirstOrDefault();

                ViewBag.cauhinh = ch;
            }
            
            
            var sp_banchay = db.SanPhams.OrderByDescending(row => row.SoLuongMua).ToList();
            sp_banchay = sp_banchay.Take(6).ToList();
            ViewBag.sp_banchay = sp_banchay;


            ViewBag.TenSanPham = sp.TenSanPham;


            
            return View(sp);
        }

        [HttpPost]
        public ActionResult Comment(Comment c)
        {
            c.NgayTao = DateTime.Now;
            var user = Session["User"] as WebApplication4.Models.Account;
            if (user != null)
            {
                c.id_account = user.id_account;
            }
            else
            {
                return RedirectToAction("Login_Account", "Account");
            }
            c.id_sanpham = Convert.ToInt32(TempData["id_sanpham"]);
            c.TrangThai = 1;
            db.Comments.Add(c);
            db.SaveChanges();
            return Redirect(Url.Action("Index/"+c.id_sanpham, "DetailProduct") + "#comment");
        }

        [HttpPost]
        public ActionResult PhanHoi(PhanHoi p)
        {
            int id_sanpham = Convert.ToInt32(TempData["id_sanpham"]);
            p.NgayPhanHoi = DateTime.Now;
            var user = Session["User"] as WebApplication4.Models.Account;
            if (user != null)
            {
                p.id_account = user.id_account;
            }
            else
            {
                return RedirectToAction("Login_Account", "Account");
            }
            db.PhanHois.Add(p);
            db.SaveChanges();
            return Redirect(Url.Action("Index/" + id_sanpham, "DetailProduct") + "#comment");
        }
    }
}