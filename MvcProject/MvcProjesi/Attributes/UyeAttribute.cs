using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjesi.Attributes
{
    public class UyeAttribute : ValidationAttribute
    {
        private object obj;       
        
        public UyeAttribute(object obj)
        {
            this.obj = obj;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Uye uye = (Uye)validationContext.ObjectInstance;
            EmailAddressAttribute addressAttribute = new EmailAddressAttribute();

            if (uye.Ad.Length >= (int)obj)
            {
                return new ValidationResult(UzunIcerik());
            }
            else if (uye.Soyad.Length >= (int)obj)
            {
                return new ValidationResult(UzunIcerik());
            }
            else if(!addressAttribute.IsValid(obj))
            {
                return new ValidationResult("Lütfen E-Posta adresinizi doğru formatta giriniz.");
            }
            else if(uye.WebSite.GetType() != typeof(UrlAttribute))
            {
                return new ValidationResult("Lütfen internet adresinizi doğru formatta giriniz.");
            }
            else if (uye.ResimYol != (string)obj)
            {
                return new ValidationResult("Lütfen doğru formatta bir resim seçiniz.");
            }
            else if (uye.Sifre != (string)obj)
            {
                return new ValidationResult("Lütfen şifrenizi doğru formatta giriniz.");
            }

            return ValidationResult.Success;
        }

        private string UzunIcerik()
        {
            return $"İçeriğin uzunluğu 50 karakterden fazla olmamalıdır!";
        }
    }
}