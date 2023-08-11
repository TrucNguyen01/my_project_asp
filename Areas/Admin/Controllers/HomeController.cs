using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.title = "Thống kê";

            var admin = Session["Admin"] as WebApplication4.Models.Account;

            if(admin != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login_Account", "Account");
            }
        }
    }
}