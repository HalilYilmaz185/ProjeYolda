using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tst_MVC1.Models;
using System.Web.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Ajax.Utilities;

namespace tst_MVC1.Controllers
{
    public class SecurityController : Controller
    {
        DenemeSQL2Entities db = new DenemeSQL2Entities();

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(tstTable user)
        {
            var userInDb = db.tstTable.FirstOrDefault(x => x.userName == user.userName && x.password == user.password);

            if (userInDb != null)
            {
                var rankInDb = db.tstTable.FirstOrDefault(x => x.rank == user.userName);

                if (rankInDb != null)
                {
                    FormsAuthentication.SetAuthCookie(user.userName, false);
                    return RedirectToAction("YoldaYemek", "Admin");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(user.userName, false);
                    return RedirectToAction("ClientYoldaYemek", "Client");
                }

            }
            else
            {

                ViewBag.Mesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }


        }
        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult SignUp(FormCollection form)
        {
            DenemeSQL2Entities db = new DenemeSQL2Entities();
            tstTable model = new tstTable();

            model.userName = form["userName"].Trim();

            var SearchData = db.tstTable.Where(x => x.userName == model.userName).SingleOrDefault();

            if (SearchData != null)
            {
                return RedirectToAction("SignUp");
            }
            else
            {
                
                model.password = form["password"].Trim();
                db.tstTable.Add(model);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

        }

        public JsonResult CheckUsername(string userdata)
        {
            DenemeSQL2Entities db = new DenemeSQL2Entities();
            System.Threading.Thread.Sleep(500);

            if (userdata != "")
            {
                var SearchData = db.tstTable.Where(x => x.userName == userdata).SingleOrDefault();

                if (SearchData != null)
                {
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }
            else
            {
                return Json(2);
            }

        }

    }
}