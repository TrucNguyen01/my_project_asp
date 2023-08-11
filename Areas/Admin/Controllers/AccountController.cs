using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/Account
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
            a.role = 1;
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
            var account = db.Accounts.SingleOrDefault(row => row.TaiKhoan == a.TaiKhoan && row.MatKhau == a.MatKhau && row.role == 1);
            if (account != null)
            {
                Session["Admin"] = account;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login_Account");
            }
        }

        public ActionResult Remove_Session()
        {
            Session["Admin"] = null;
            return RedirectToAction("Login_Account");
        }

        public ActionResult Update_Account()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update_Account(Account a)
        {
            var admin = Session["Admin"] as WebApplication4.Models.Account;
            var account = db.Accounts.Find(admin.id_account);
            account.HoTen = a.HoTen;
            account.SoDienThoai = a.SoDienThoai;
            account.Email = a.Email;
            account.DiaChi = a.DiaChi;
            account.GioiTinh = a.GioiTinh;
            db.SaveChanges();
            Session["Admin"] = account;
            return RedirectToAction("Index", "Home");
        }
    }
}