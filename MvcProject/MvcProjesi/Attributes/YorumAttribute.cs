using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjesi.Attributes
{
    public class YorumAttribute : ValidationAttribute
    {
        private int length;

        public YorumAttribute(int length)
        {
            this.length = length;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Yorum yorum = (Yorum)validationContext.ObjectInstance;

            if (yorum.Icerik.Length >= length)
            {
                return new ValidationResult(UzunIcerik());
            }
            return ValidationResult.Success;
        }

        private string UzunIcerik()
        {
            return $"İçeriğin uzunluğu 50 karakterden fazla olmamalıdır!";
        }
    }
}