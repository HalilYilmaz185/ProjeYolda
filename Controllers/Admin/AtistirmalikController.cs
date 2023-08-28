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
        public ActionResult Ekle(tstAtistirmalikTable EkleAtistirmalik, HttpPostedFileBase resim, tstAtistirmalikKategoriTable EkleAtistirmalikKategori)
        {
            DenemeSQL2Entities2 db = new DenemeSQL2Entities2();

            DenemeSQL2Entities4 Kdb = new DenemeSQL2Entities4();

            if (resim != null)
            {
                string ResimAdi = System.IO.Path.GetFileName(resim.FileName);
                string adres = Server.MapPath("~/images/atistirmalik/" + ResimAdi);
                resim.SaveAs(adres);

                EkleAtistirmalik.ResimYol = ResimAdi;
            }

            var UrunKategori = Kdb.tstAtistirmalikKategoriTable.FirstOrDefault(x => x.AtistirmalikKategori == EkleAtistirmalik.Aciklama);
            EkleAtistirmalikKategori.AtistirmalikKategori = EkleAtistirmalik.Aciklama;

            if (UrunKategori == null)
            {
                EkleAtistirmalikKategori.Adet = 1;
                Kdb.tstAtistirmalikKategoriTable.Add(EkleAtistirmalikKategori);
            }
            else
            {
                UrunKategori.Adet += 1;
                EkleAtistirmalikKategori.Adet = UrunKategori.Adet;
            }

            db.tstAtistirmalikTables.Add(EkleAtistirmalik);
            Kdb.SaveChanges();
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

        public ActionResult Sil(int id, string Aciklama)
        {
            DenemeSQL2Entities2 db = new DenemeSQL2Entities2();
            DenemeSQL2Entities4 Kdb = new DenemeSQL2Entities4();
            tstAtistirmalikTable SilDB = db.tstAtistirmalikTables.FirstOrDefault(x => x.id == id);

            tstAtistirmalikKategoriTable UrunKategori = Kdb.tstAtistirmalikKategoriTable.FirstOrDefault(x => x.AtistirmalikKategori == Aciklama);
            
            if (UrunKategori.Adet > 1)
            {
                UrunKategori.Adet -= 1;
            }
            else
            {
                Kdb.tstAtistirmalikKategoriTable.Remove(UrunKategori);
            }

            db.tstAtistirmalikTables.Remove(SilDB);
            Kdb.SaveChanges();
            db.SaveChanges();

            return RedirectToAction("YoldaAtistirmalik", "Admin");
        }
    }
}