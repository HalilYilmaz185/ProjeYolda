using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tst_MVC1.Models;

namespace tst_MVC1.Controllers.Admin
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        public ActionResult KullanıcıSil(int id)
        {
            DenemeSQL2Entities db = new DenemeSQL2Entities();
            tstTable SilK = db.tstTable.FirstOrDefault(x => x.id == id);
            db.tstTable.Remove(SilK);
            db.SaveChanges();

            return RedirectToAction("KullanıcıArama","Admin");
        }

        public ActionResult KullanıcıSepetSil(int id)
        {
            DenemeSQL2Entities3 db = new DenemeSQL2Entities3();
            tstYemekSepet1 SilDB = db.tstYemekSepet1.FirstOrDefault(x => x.id == id);
            db.tstYemekSepet1.Remove(SilDB);
            db.SaveChanges();

            return RedirectToAction("KullanıcıArama", "Admin");
        }
    }
}