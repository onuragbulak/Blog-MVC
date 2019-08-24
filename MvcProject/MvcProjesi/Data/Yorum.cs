using MvcProjesi.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MvcProjesi.Data
{
    public class Yorum : BaseClass
    {
        [Column("Yorum_Id")]
        public override int Id { get; set; }

        [Required(ErrorMessage = "Lütfen yorumun içeriğini giriniz.")]
        [StringLength(50, ErrorMessage = "Yorumun içeriği 50 karakterden uzun olamaz.")]
        public string Icerik { get; set; }

        //[Column("Tarih")]
        //public override DateTime OlusturmaTarih { get; set; } = DateTime.Now;

        public virtual Makale Makale { get; set; }
        public virtual Uye Uye { get; set; }

    }
}