using MvcProjesi.Attributes;
using MvcProjesi.UnitOfWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    public class Makale : BaseClass
    {
        [Column("Makale_Id")]
        public override int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Makale başlığı 3-50 karakter arasında olmalıdır.")]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "Lütfen makalenin içeriğini giriniz.")]
        [StringLength(500, ErrorMessage = "Makalenin içeriği 500 karakterden uzun olamaz.")]
        [DataType(DataType.Html, ErrorMessage = "Lütfen makale içeriğini html formatında giriniz.")]
        public string Icerik { get; set; }

        //[Column("Tarih")]
        //public override DateTime OlusturmaTarih { get; set; } = DateTime.Now;

        public virtual Uye Uye { get; set; }
        public virtual ICollection<Yorum> Yorums { get; set; }
        public virtual List<Etiket> Etikets { get; set; }

        //public void X()
        //{
        //    var props = typeof(Makale).GetProperties();

        //    props.First().GetCustomAttributes(typeof(ColumnAttribute), false);
        //}

        public override void Validate()
        {
            //Yapmak istediğin validasyon.
            //var unitOfWork = new MUnitOfWork();
            //var uye = unitOfWork.UyeRepository.GetByExpression(u => u.Id == (int)HttpContext.Current.Session[0]).First();
            //uye.OlusturmaTarih
            //ValidationResults.Add(new ValidationResult(""));

            base.Validate();
        }
    }   
}