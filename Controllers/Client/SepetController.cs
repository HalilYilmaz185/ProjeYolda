using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tst_MVC1.Models;

namespace tst_MVC1.Controllers.Client
{
    public class SepetController : Controller
    {
        [Authorize]
        // GET: Sepet
        [HttpPost]
        public ActionResult SepeteYemekEkle(tstYemekSepet1 Sepet)
        {
            DenemeSQL2Entities3 db = new DenemeSQL2Entities3();
            var SepetIcerigi = db.tstYemekSepet1.FirstOrDefault(x => x.YemekAdi == Sepet.YemekAdi && x.userName == Sepet.userName);


            if (SepetIcerigi != null)
            {
                tstYemekSepet1 Duz = db.tstYemekSepet1.FirstOrDefault(x => x.id == SepetIcerigi.id);
                
                var YemekAdet = SepetIcerigi.Adet;
                YemekAdet += 1;

                var yeniF = SepetIcerigi.Fiyat * YemekAdet;

                Duz.Fiyat = yeniF;
                Duz.Adet = YemekAdet;


                db.SaveChanges();

                return RedirectToAction("ClientYoldaYemek", "Client");
            }
            else
            {
                Sepet.Adet = 1;

                db.tstYemekSepet1.Add(Sepet);
                db.SaveChanges();
                return RedirectToAction("ClientYoldaYemek", "Client");
            }

        }

        public ActionResult SepeteAtisEkle(tstYemekSepet1 Sepet)
        {
            DenemeSQL2Entities3 db = new DenemeSQL2Entities3();
            var SepetIcerigi = db.tstYemekSepet1.FirstOrDefault(x => x.YemekAdi == Sepet.YemekAdi && x.userName == Sepet.userName);


            if (SepetIcerigi != null)
            {
                tstYemekSepet1 Duz = db.tstYemekSepet1.FirstOrDefault(x => x.id == SepetIcerigi.id);

                var YemekAdet = SepetIcerigi.Adet;
                YemekAdet += 1;

                var yeniF = SepetIcerigi.Fiyat * YemekAdet;

                Duz.Fiyat = yeniF;

                Duz.Adet = YemekAdet;


                db.SaveChanges();

                return RedirectToAction("ClientYoldaAtistirmalik", "Client");
            }
            else
            {
                Sepet.Adet = 1;

                db.tstYemekSepet1.Add(Sepet);
                db.SaveChanges();
                return RedirectToAction("ClientYoldaAtistirmalik", "Client");
            }

        }

        public ActionResult SepetSil(int id)
        {
            DenemeSQL2Entities3 db = new DenemeSQL2Entities3();
            tstYemekSepet1 SilDB = db.tstYemekSepet1.FirstOrDefault(x => x.id == id);
            db.tstYemekSepet1.Remove(SilDB);
            db.SaveChanges();

            return RedirectToAction("Sepet", "Client");
        }

    }
}