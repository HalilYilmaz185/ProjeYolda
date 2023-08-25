using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tst_MVC1.Models;

namespace tst_MVC1.Controllers.Admin
{
    public class AtistirmalikController : Controller
    {
        // GET: Atistirmalik
        [Authorize(Users = "Admin")]
        // GET: Yemek
        [HttpPost]
        public ActionResult Ekle(tstAtistirmalikTable EkleAtistirmalik, HttpPostedFileBase resim)
        {
            DenemeSQL2Entities2 db = new DenemeSQL2Entities2();

            if (resim != null)
            {
                string ResimAdi = System.IO.Path.GetFileName(resim.FileName);
                string adres = Server.MapPath("~/images/atistirmalik/" + ResimAdi);
                resim.SaveAs(adres);

                EkleAtistirmalik.ResimYol = ResimAdi;
            }

            db.tstAtistirmalikTables.Add(EkleAtistirmalik);
            db.SaveChanges();

            return RedirectToAction("YoldaAtistirmalik", "Admin");
        }

        [HttpPost]
        public ActionResult Duzenle(tstAtistirmalikTable DuzenAtistirmalik, HttpPostedFileBase resim)
        {
            DenemeSQL2Entities2 db = new DenemeSQL2Entities2();
            tstAtistirmalikTable Duz = db.tstAtistirmalikTables.FirstOrDefault(x => x.id == DuzenAtistirmalik.id);

            if (resim != null)
            {
                string ResimAdi = System.IO.Path.GetFileName(resim.FileName);
                string adres = Server.MapPath("~/images/atistirmalik/" + ResimAdi);
                resim.SaveAs(adres);

                Duz.ResimYol = ResimAdi;
            }

            Duz.AtistirmalikAdi = DuzenAtistirmalik.AtistirmalikAdi;
            Duz.Aciklama = DuzenAtistirmalik.Aciklama;
            Duz.Fiyat = DuzenAtistirmalik.Fiyat;
            
            db.SaveChanges();

            return RedirectToAction("YoldaAtistirmalik", "Admin");
        }

        public ActionResult Sil(int id)
        {
            DenemeSQL2Entities2 db = new DenemeSQL2Entities2();
            tstAtistirmalikTable SilDB = db.tstAtistirmalikTables.FirstOrDefault(x => x.id == id);
            db.tstAtistirmalikTables.Remove(SilDB);
            db.SaveChanges();

            return RedirectToAction("YoldaAtistirmalik", "Admin");
        }
    }
}