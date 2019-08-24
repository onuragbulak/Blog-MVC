using MvcProjesi.Data;
using MvcProjesi.Repositories;
using MvcProjesi.UnitOfWork;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjesi.Controllers
{
    public class UyelikController : Controller
    {
        private MUnitOfWork unitOfWork;

        public UyelikController()
        {
            unitOfWork = new MUnitOfWork();
        }
        public UyelikController(MUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult YeniUyelik()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniUyelik(Uye model, string textBoxDogum, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (String.IsNullOrEmpty(textBoxDogum))
            {
                ModelState.AddModelError("textBoxDogum", "Doğum tarihi boş geçilemez!");
                return View();
            }
            int yil = int.Parse(textBoxDogum.Substring(6));
            if (yil > 2002)
            {
                ModelState.AddModelError("textBoxDogum", "Yaşınız 12'den küçük olamaz!");
                return View();
            }
            Uye uye = new Uye();
            if (file != null)
            {
                file.SaveAs(Server.MapPath("~/Images/") + file.FileName);
                uye.ResimYol = "/Images/" + file.FileName;
            }
            uye.Ad = model.Ad;
            uye.EPosta = model.EPosta;
            uye.Soyad = model.Soyad;
            uye.WebSite = model.WebSite;
            uye.Sifre = model.Sifre;

            unitOfWork.UyeRepository.Insert(uye);
            unitOfWork.Save();
            unitOfWork.Dispose();
           
            return RedirectToAction("UyelikBasarili");

        }
        public ActionResult UyelikBasarili()
        {
            return View();
        }
        public ActionResult UyeGiris()
        {
            return View();
        }

        [HttpPost]
        public string UyeGirisi()
        {
            string posta = Request.Form["txtPosta"];
            string sifre = Request.Form["pswSifre"];
            if (string.IsNullOrEmpty(posta) && string.IsNullOrEmpty(sifre))
            {
                return "E-Posta adresinizi ve şifrenizi girmediniz.";
            }
            else if (string.IsNullOrEmpty(posta))
            {
                return "E-posta adresinizi girmediniz.";
            }
            else if (string.IsNullOrEmpty(sifre))
            {
                return "Şifrenizi girmediniz.";
            }
            else
            {
                using (MvcProjesiContext db = new MvcProjesiContext())
                {
                    //var x = HttpContext.User.Identity.IsAuthenticated;

                    //FormsAuthentication


                    var uye = unitOfWork.UyeRepository.GetByExpression(a => true).Where(i => i.EPosta == posta && i.Sifre == sifre).Select(m => m).SingleOrDefault();
                    
                    if (uye == null)
                    {
                        return "Kullanıcı adınızı ya da şifrenizi hatalı girdiniz!";
                    }

                    Session["uye_Id"] = uye.Id;
                    Session["uye_Ad"] = uye.Ad;

                    unitOfWork.Dispose();

                    return "<script type='text/javascript'>setTimeout(function(){window.location='/'});</script>";
                }
            }
        }
        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return View();
        }
        public ActionResult Profil()
        {
            int uyeId = (int)Session["uye_Id"];
            var uye = unitOfWork.UyeRepository.GetByExpression(a => true).Where(a => a.Id == uyeId).FirstOrDefault();
            unitOfWork.Dispose();

            return View(uye);
        }
    }
}