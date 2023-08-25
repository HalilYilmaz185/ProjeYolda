using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using tst_MVC1.Models;

namespace tst_MVC1.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        // GET: Admin
        public ActionResult Sepet()
        {
            DenemeSQL2Entities3 db = new DenemeSQL2Entities3();

            var user = User.Identity.Name;
            ViewBag.ToplamFiyat = db.tstYemekSepet1.Where(x => x.userName.Equals(user)).Select(x => x.Fiyat).Sum();

            ViewBag.Sepet = db.tstYemekSepet1.Where(x => x.userName.Equals(user));

            ViewBag.ClientID = user;
            return View();
        }

        public ActionResult ClientYoldaYemek()
        {
            DenemeSQL2Entities1 db = new DenemeSQL2Entities1();
            DenemeSQL2Entities3 idDB = new DenemeSQL2Entities3();

            var user = User.Identity.Name;

            //Sepet ikonunun üzerinde badge sayısı için
            ViewBag.Count = idDB.tstYemekSepet1.Where(x => x.userName.Equals(user)).Select(x => x.Adet).Sum();

            ViewBag.ClientID = user;
            ViewBag.YoldaYemek = db.tstYemekTable;

            return View();
        }

        public ActionResult ClientYoldaAtistirmalik()
        {
            DenemeSQL2Entities2 db = new DenemeSQL2Entities2();
            DenemeSQL2Entities3 idDB = new DenemeSQL2Entities3();
            var user = User.Identity.Name;


            ViewBag.Count = idDB.tstYemekSepet1.Where(x => x.userName.Equals(user)).Select(x => x.Adet).Sum();

            ViewBag.ClientID = user;

            ViewBag.YoldaAtistirmalik = db.tstAtistirmalikTables;

            

            return View();

        }

        [HttpPost]
        public ActionResult ClientArama(string arama)
        {
            DenemeSQL2Entities1 Ydb = new DenemeSQL2Entities1();
            DenemeSQL2Entities2 Adb = new DenemeSQL2Entities2();

            var user = User.Identity.Name;

            ViewBag.ClientID = user;

            string oncekiSayfa = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
            ViewBag.ESayfa = oncekiSayfa;

            if (arama != null)
            {
                if (ViewBag.ESayfa == "ClientYoldaYemek")
                {
                    var YSearchData = Ydb.tstYemekTable.Where(x => x.YemekAdi.StartsWith(arama));
                    ViewBag.Arama = YSearchData;
                }
                if (ViewBag.ESayfa == "ClientYoldaAtistirmalik")
                {
                    var YSearchData = Adb.tstAtistirmalikTables.Where(x => x.AtistirmalikAdi.StartsWith(arama));
                    ViewBag.Arama = YSearchData;
                }
                if (ViewBag.ESayfa == "Sepet")
                {
                    return RedirectToAction("ClientYoldaYemek", "Client");
                }
            }

            return View();
        }
    }
}