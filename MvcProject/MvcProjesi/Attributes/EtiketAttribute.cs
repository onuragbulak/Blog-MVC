using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjesi.Attributes
{
    public class EtiketAttribute : ValidationAttribute
    {
        private int length;

        public EtiketAttribute(int length)
        {
            this.length = length;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Etiket etiket = (Etiket)validationContext.ObjectInstance;

            if (etiket.Icerik == null)
            {
                return new ValidationResult(IcerikIsNull());
            }
            else if (etiket.Icerik.Length >= length)
            {
                return new ValidationResult(UzunIcerik());
            }

            return ValidationResult.Success;
        }

        private string IcerikIsNull()
        {
            return $"Lütfen içeriği giriniz";
        }
        private string UzunIcerik()
        {
            return $"İçeriğin uzunluğu 500 karakterden fazla olmamalıdır!";
        }
    }
}