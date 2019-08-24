using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjesi.Attributes
{
    public class MakaleAttribute : ValidationAttribute
    {
        private int length;

        public MakaleAttribute(int length)
        {
            this.length = length;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Makale makale = (Makale)validationContext.ObjectInstance;

            if(makale.Baslik == null && makale.Icerik == null)
            {
                return new ValidationResult(NoEntry());
            }
            else if (makale.Baslik == null)
            {
                return new ValidationResult(BaslikIsNull());
            }
            else if (makale.Icerik == null)
            {
                return new ValidationResult(IcerikIsNull());
            }
            else if (makale.Baslik.Length >= length && makale.Icerik.Length >= length)
            {
                return new ValidationResult(Uzun());
            }
            else if (makale.Baslik.Length >= length)
            {
                return new ValidationResult(UzunBaslik());
            }
            else if (makale.Icerik.Length >= length)
            {
                return new ValidationResult(UzunIcerik());
            }

            return ValidationResult.Success;
        }

        private string NoEntry()
        {
            return $"Lütfen boş alanları doldurunuz.";
        }
        private string BaslikIsNull()
        {
            return $"Lütfen başlığı giriniz.";
        }
        private string IcerikIsNull()
        {
            return $"Lütfen içeriği giriniz";
        }
        private string Uzun()
        {
            return $"Başlığın uzunluğu 50 karakterden fazla, içeriğin uzunluğu ise 500 karakterden fazla olmamalıdır!";
        }
        private string UzunBaslik()
        {
            return $"Başlığın uzunluğu 50 karakterden fazla olmamalıdır!";
        }
        private string UzunIcerik()
        {
            return $"İçeriğin uzunluğu 500 karakterden fazla olmamalıdır!";
        }

    }
}