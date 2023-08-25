using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tst_MVC1.Models;

namespace tst_MVC1.Controllers.Admin
{
    public class YemekController : Controller
    {
        [Authorize(Users = "Admin")]
        // GET: Yemek
        [HttpPost]
        public ActionResult Ekle(tstYemekTable EkleYemek, HttpPostedFileBase resim)
        {
            DenemeSQL2Entities1 db = new DenemeSQL2Entities1();

            if (resim != null)
            {
                string ResimAdi = System.IO.Path.GetFileName(resim.FileName);
                string adres = Server.MapPath("~/images/yemek/" + ResimAdi);
                resim.SaveAs(adres);

                EkleYemek.ResimYol = ResimAdi;
            }

            db.tstYemekTable.Add(EkleYemek);
            db.SaveChanges();

            return RedirectToAction("YoldaYemek","Admin");
        }

        [HttpPost]
        public ActionResult Duzenle(tstYemekTable DuzenYemek, HttpPostedFileBase resim)
        {
            DenemeSQL2Entities1 db = new DenemeSQL2Entities1();
            tstYemekTable Duz = db.tstYemekTable.FirstOrDefault(x => x.id == DuzenYemek.id);

            if (resim != null)
            {
                string ResimAdi = System.IO.Path.GetFileName(resim.FileName);
                string adres = Server.MapPath("~/images/yemek/" + ResimAdi);
                resim.SaveAs(adres);

                Duz.ResimYol = ResimAdi;
            }

            Duz.YemekAdi = DuzenYemek.YemekAdi;
            Duz.Aciklama = DuzenYemek.Aciklama;
            Duz.Fiyat = DuzenYemek.Fiyat;

            db.SaveChanges();

            return RedirectToAction("YoldaYemek", "Admin");
        }

        public ActionResult Sil(int id)
        {
            DenemeSQL2Entities1 db = new DenemeSQL2Entities1();
            tstYemekTable SilDB = db.tstYemekTable.FirstOrDefault(x => x.id == id);
            db.tstYemekTable.Remove(SilDB);
            db.SaveChanges();

            return RedirectToAction("YoldaYemek", "Admin");
        }

    }
}