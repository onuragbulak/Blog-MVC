using System.Linq;
using System.Web.Mvc;
using MvcProjesi.Data;
using MvcProjesi.Repositories;
using MvcProjesi.UnitOfWork;

namespace MvcProjesi.Controllers
{
    public class HomeController : Controller
    {
        private MUnitOfWork unitOfWork;

        public HomeController()
        {
            unitOfWork = new MUnitOfWork();
        }

        public HomeController(MUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SonBesMakale()
        {
            var gununMakaleleri = unitOfWork.MakaleRepository.GetByExpression(a => true).OrderByDescending(a => a.OlusturmaTarih).Take(5).ToList();

            return PartialView(gununMakaleleri);
        }
        public ActionResult SonBesYorum()
        {
            var gununYorumlari = unitOfWork.YorumRepository.GetByExpression(a => true).OrderByDescending(a => a.OlusturmaTarih).Take(5).ToList();

            return PartialView(gununYorumlari);
        }
        public ActionResult EnCokOnEtiket()
        {
            var fazlaEtiket = unitOfWork.EtiketRepository.GetByExpression(a => true).OrderByDescending(a => a.Makales.Count()).Select(a => a).Take(10).ToList();
            //List<Etiket> etiketListe = (from i in fazlaEtiket orderby i.Makales.Count() descending select i).Take(10).ToList();

            return PartialView(fazlaEtiket);
        }
        public ActionResult TumMakaleler()
        {
            var tumMakaleler = unitOfWork.MakaleRepository.GetAll();

            return View(tumMakaleler);
        }
        public ActionResult TumYorumlar()
        {
            var tumYorumlar = unitOfWork.YorumRepository.GetAll();

            return View(tumYorumlar);
        }
        public ActionResult EtiketinMakaleleri(int etiketId)
        {
            var etiketMakalesi = unitOfWork.EtiketRepository.GetByExpression(a => true).Where(a => a.Id == etiketId).Select(a => a.Makales).ToList();

            return View(etiketMakalesi[0]);
        }
        public ActionResult MakaleDetay(int makaleId)
        {
            var makaleDetay = unitOfWork.MakaleRepository.GetByExpression(a => true).Where(a => a.Id == makaleId).Select(a => a).FirstOrDefault();

            return View(makaleDetay);
        }
        public ActionResult YorumMakalesi(int yorumId)
        {
            var yorumMakalesi = unitOfWork.YorumRepository.GetByExpression(a => true).Where(a => a.Id == yorumId).Select(a => a.Makale).FirstOrDefault();

            return View(yorumMakalesi);
        }

    }
}