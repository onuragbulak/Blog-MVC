using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcProjesi.Data;
using MvcProjesi.UnitOfWork;

namespace MvcProjesi.Controllers
{
    public class TextController : Controller
    {
        private MUnitOfWork unitOfWork;

        public TextController()
        {
            unitOfWork = new MUnitOfWork();

        }
        public TextController(MUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult MakaleGiris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MakaleGiris(ViewModel _vm)
        {
            ViewModel vm = new ViewModel()
            {
                Makale = new Makale(),
                Etiket = new Etiket()
            };

            int uyeId = (int)Session["uye_Id"];

            vm.Makale.Id = _vm.Makale.Id;
            vm.Makale.Icerik = _vm.Makale.Icerik;
            vm.Makale.Baslik = _vm.Makale.Baslik;
            vm.Makale.Uye = unitOfWork.UyeRepository.GetByExpression(a => true).Where(i => i.Id == uyeId).Select(i => i).FirstOrDefault();

            if (unitOfWork.EtiketRepository.GetByExpression(a => true).Where(a => a.Icerik == _vm.Etiket.Icerik).Any())
            {
                vm.Etiket = unitOfWork.EtiketRepository.GetByExpression(a => true).Where(a => a.Icerik == _vm.Etiket.Icerik).Select(a => a).FirstOrDefault();
                vm.Etiket.Makales.Add(vm.Makale);
                unitOfWork.EtiketRepository.Edit(vm.Etiket);
            }
            else
            {
                vm.Etiket.Id = _vm.Etiket.Id;
                vm.Etiket.Icerik = _vm.Etiket.Icerik;
                vm.Etiket.Makales = new List<Makale>() { vm.Makale };
                unitOfWork.EtiketRepository.Insert(vm.Etiket);
            }
            
            unitOfWork.MakaleRepository.Insert(vm.Makale);

            unitOfWork.Save();
            unitOfWork.Dispose();

            return View();
        }
        public string Main()
        {
            return "<script type='text/javascript'>setTimeout(function(){window.location='/'});</script>";
        }
        public ActionResult YorumGiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YorumGiris(Yorum _yorum, int makaleId, int uyeId)
        {
            var uye = unitOfWork.UyeRepository.GetByExpression(a => true).Where(i => i.Id == uyeId).Select(i => i).FirstOrDefault();
            var makale = unitOfWork.MakaleRepository.GetByExpression(a => true).Where(i => i.Id == makaleId).Select(i => i).FirstOrDefault();
            makale.Uye = uye;

            Yorum yorum = new Yorum
            {
                Icerik = _yorum.Icerik,
                Uye = uye,
                Makale = makale,
                Id = _yorum.Id
            };

            unitOfWork.YorumRepository.Insert(yorum);
            unitOfWork.Save();
            unitOfWork.Dispose();

            return View();
        }       
    }
}