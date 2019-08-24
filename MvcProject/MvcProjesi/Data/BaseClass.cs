using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProjesi.Data
{
    public abstract class BaseClass
    {
        public abstract int Id { get; set; }

        public DateTime OlusturmaTarih { get; set; }

        [Column("Guncelleme_Id")]
        public int GuncellemeId { get; set; }

        public DateTime GuncellemeTarih { get; set; }

        public List<ValidationResult> ValidationResults = new List<ValidationResult>();

        //public void KayitGuncellendi()
        //{
        //    GuncellemeId = 1;
        //    GuncellemeTarih = DateTime.Now;
        //}

        public virtual void Validate()
        {

            if (ValidationResults.Count > 0)
            {
                throw new Exception(string.Join(", ", ValidationResults.Select(v => v.ErrorMessage)));
            }
        }
    }
}