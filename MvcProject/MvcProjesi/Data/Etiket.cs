using MvcProjesi.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProjesi.Data
{
    public class Etiket : BaseClass
    {
        [Column("Etiket_Id")]
        public override int Id { get; set; }

        [Required(ErrorMessage = "Lütfen etiketin içeriğini giriniz.")]
        [StringLength(50, ErrorMessage = "Etiketin içeriği 50 karakterden uzun olamaz.")]
        public string Icerik { get; set; }

        //[Column("Tarih")]
        //public override DateTime OlusturmaTarih { get; set; } = DateTime.Now;

        public virtual List<Makale> Makales { get; set; }
    }
}
