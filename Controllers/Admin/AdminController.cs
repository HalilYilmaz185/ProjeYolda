using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tst_MVC1.Models;

namespace tst_MVC1.Controllers
{
    [Authorize(Users = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult YoldaYemek()
        {
            DenemeSQL2Entities1 db = new DenemeSQL2Entities1();
            ViewBag.YoldaYemek = db.tstYemekTable;
            var user = User.Identity.Name;

            ViewBag.AdminName = user;

            return View();
        }

        public ActionResult YoldaAtistirmalik()
        {
            DenemeSQL2Entities2 db = new DenemeSQL2Entities2();
            ViewBag.YoldaAtistirmalik = db.tstAtistirmalikTables;
            var user = User.Identity.Name;

            ViewBag.AdminName = user;
            return View();

        }

        [HttpPost]
        public ActionResult AdminArama(string arama)
        {
            DenemeSQL2Entities1 Ydb = new DenemeSQL2Entities1();
            DenemeSQL2Entities2 Adb = new DenemeSQL2Entities2();

            var user = User.Identity.Name;

            ViewBag.AdminName = user;

            string oncekiSayfa = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
            ViewBag.ESayfa = oncekiSayfa;

            if (arama != null)
            {
                if (ViewBag.ESayfa == "YoldaYemek")
                {
                    var YSearchData = Ydb.tstYemekTable.Where(x => x.YemekAdi.StartsWith(arama));
                    ViewBag.Arama = YSearchData;
                }
                if (ViewBag.ESayfa == "YoldaAtistirmalik")
                {
                    var ASearchData = Adb.tstAtistirmalikTables.Where(x => x.AtistirmalikAdi.StartsWith(arama));
                    ViewBag.Arama = ASearchData;
                }
            }

            return View();
        }
    }
}