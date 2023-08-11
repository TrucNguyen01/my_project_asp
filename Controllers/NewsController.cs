using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Controllers
{
    public class NewsController : Controller
    {
        Model1 db = new Model1();

        // GET: News
        public ActionResult Index(int index = 1)
        {
            List<TinTuc> ds_tintuc = db.TinTucs.ToList();

            int soTinTuc1Trang = 8;
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ds_tintuc.Count()) / Convert.ToDouble(soTinTuc1Trang)));
            int soTinTucSkip = (index - 1) * soTinTuc1Trang;

            ds_tintuc = ds_tintuc.Skip(soTinTucSkip).Take(soTinTuc1Trang).ToList();
            ViewBag.soTrang = soTrang;
            ViewBag.Index = index;

            return View(ds_tintuc);
        }

        public ActionResult New_Detail(int id)
        {
            var tintuc = db.TinTucs.Find(id);
            return View(tintuc);
        }
    }
}