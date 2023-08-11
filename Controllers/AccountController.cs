using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class AccountController : Controller
    {
        Model1 db = new Model1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create_Account()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create_Account(Account a)
        {
            a.NgayTao = DateTime.Now;
            a.role = 0;
            a.SoLanMuaHang = 0;
            a.SoLanHuyHang = 0;
            a.TrangThai = 1;
            db.Accounts.Add(a);
            db.SaveChanges();
            return RedirectToAction("Login_Account");
        }
        public ActionResult Login_Account()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login_Account(Account a)
        {
            var account = db.Accounts.SingleOrDefault(row => row.TaiKhoan == a.TaiKhoan && row.MatKhau == a.MatKhau && row.role == 0);
            if(account != null)
            {
                Session["User"] = account;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login_Account");
            }
        }

        public ActionResult Remove_Session()
        {
            Session["User"] = null;
            return RedirectToAction("Login_Account");
        }

        public ActionResult UpdateInfo(int id)
        {
            TempData["id_user"] = id;
            var account = db.Accounts.Find(id);
            return View(account);
        }
        [HttpPost]
        public ActionResult UpdateInfo(Account a)
        {
            int id = Convert.ToInt32(TempData["id_user"]);
            var account = db.Accounts.Find(id);
            account.HoTen = a.HoTen;
            account.SoDienThoai = a.SoDienThoai;
            account.Email = a.Email;
            account.DiaChi = a.DiaChi;
            account.GioiTinh = a.GioiTinh;

            db.SaveChanges();
            Session["User"] = account;
            return RedirectToAction("Index", "Home");
        }
    }
}