using MvcProjesi.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProjesi.Data
{
    public class Uye : BaseClass
    {
        [Column("Uye_Id")]
        public override int Id { get; set; }

        [Required(ErrorMessage = "Lütfen adınızı giriniz.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Adınızı 3-50 karakter arasında girebilirsiniz.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Soyadınızı 3-50 karakter arasında girebilirsiniz.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen e-posta adresinizi geçerli bir formatta giriniz.")]
        public string EPosta { get; set; }


        [DataType(DataType.Url, ErrorMessage = "Lütfen web site adresinizi, geçerli bir formatta giriniz.")]
        public string WebSite { get; set; }


        [DataType(DataType.ImageUrl, ErrorMessage = "Lütfen resim yolunuzu doğru şekilde giriniz.")]
        public string ResimYol { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string Sifre { get; set; }

        public virtual List<Yorum> Yorums { get; set; }
        public virtual List<Makale> Makales { get; set; }       
    }
}