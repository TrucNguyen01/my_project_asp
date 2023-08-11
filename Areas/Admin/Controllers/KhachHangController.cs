using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/KhachHang
        public ActionResult Index(int index = 1)
        {
            ViewBag.title = "Khách hàng";
            List<Account> ds_account = db.Accounts.Where(row => row.role == 0).ToList();

            int SoKhachHang1Trang = 6;
            int SoTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ds_account.Count) / Convert.ToDouble(SoKhachHang1Trang)));
            int SoKhachHangSkip = (index - 1) * SoKhachHang1Trang;

            ViewBag.Index = index;
            ViewBag.SoTrang = SoTrang;

            ds_account = ds_account.Skip(SoKhachHangSkip).Take(SoKhachHang1Trang).ToList();

            return View(ds_account);
        }

        public ActionResult ThongTinChiTiet(int id)
        {
            var khachhang = db.Accounts.Find(id);
            return View(khachhang);
        }

        [HttpPost]
        public ActionResult ChangeStatus(int id)
        {
            var khachhang = db.Accounts.Find(id);
            if(khachhang != null)
            {
                khachhang.TrangThai = 1 - (int)khachhang.TrangThai;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}