using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    public class MvcProjesiContext : DbContext
    {
        public MvcProjesiContext()
            : base("MvcProjesiContext")
        {
            Configuration.LazyLoadingEnabled = true;

            //Database.Delete();
            if (Database.Exists() == false)
            {
                Database.SetInitializer(new FirstDatabaseInitializer());
            }

        }

        public override int SaveChanges()
        {
            try
            {
                foreach (var entry in this.ChangeTracker.Entries().Where(d => d.State != EntityState.Unchanged))
                {
                    var entity = (BaseClass)entry.Entity;
                    //entity.Validate();

                    if (entry.State == EntityState.Added)
                    {
                        //(BaseClass)added.KayitGuncellendi();
                        entity.OlusturmaTarih = DateTime.Now;
                        entity.GuncellemeTarih = DateTime.Now;

                        if (HttpContext.Current.User.Identity.IsAuthenticated)
                        {
                            //HttpContext.Current.Session
                            entity.GuncellemeId = Convert.ToInt32(HttpContext.Current.Session);
                        }
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.GuncellemeTarih = DateTime.Now;

                        if (HttpContext.Current.User.Identity.IsAuthenticated)
                        {
                            //HttpContext.Current.Session
                            entity.GuncellemeId = Convert.ToInt32(HttpContext.Current.Session);
                        }
                    }
                }

                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public DbSet<Etiket> Etikets { get; set; }
        public DbSet<Makale> Makales { get; set; }
        public DbSet<Uye> Uyes { get; set; }
        public DbSet<Yorum> Yorums { get; set; }
    }


    internal class FirstDatabaseInitializer : DropCreateDatabaseAlways<MvcProjesiContext>
    {
        protected override void Seed(MvcProjesiContext db)
        {

            Uye uye = new Uye() { Ad = "Onur", Soyad = "Ağbulak", EPosta = "onuragbulak@deneme.com", ResimYol = "", OlusturmaTarih = DateTime.Now, WebSite = "", Sifre = "123455" };

            db.Uyes.Add(uye);

            //Makale makale1 = new Makale() { Baslik = "Makale Başlığı 1", Icerik = "Makale İçeriği 1", OlusturmaTarih = DateTime.Now, Uye = uye };
            //Makale makale2 = new Makale() { Baslik = "Makale Başlığı 2", Icerik = "Makale İçeriği 2", OlusturmaTarih = DateTime.Now, Uye = uye };
            //Makale makale3 = new Makale() { Baslik = "Makale Başlığı 3", Icerik = "Makale İçeriği 3", OlusturmaTarih = DateTime.Now, Uye = uye };
            //Makale makale4 = new Makale() { Baslik = "Makale Başlığı 4", Icerik = "Makale İçeriği 4", OlusturmaTarih = DateTime.Now, Uye = uye };
            //Makale makale5 = new Makale() { Baslik = "Makale Başlığı 5", Icerik = "Makale İçeriği 5", OlusturmaTarih = DateTime.Now, Uye = uye };
            //Makale makale6 = new Makale() { Baslik = "Makale Başlığı 6", Icerik = "Makale İçeriği 6", OlusturmaTarih = DateTime.Now, Uye = uye };

            //db.Makales.Add(makale1);
            //db.Makales.Add(makale2);
            //db.Makales.Add(makale3);
            //db.Makales.Add(makale4);
            //db.Makales.Add(makale5);
            //db.Makales.Add(makale6);

            //Yorum yorum1 = new Yorum() { Icerik = "Makale 1 için yazılan yorum", OlusturmaTarih = DateTime.Now, Makale = makale1, Uye = uye };
            //Yorum yorum2 = new Yorum() { Icerik = "Makale 2 için yazılan yorum", OlusturmaTarih = DateTime.Now, Makale = makale2, Uye = uye };
            //Yorum yorum3 = new Yorum() { Icerik = "Makale 3 için yazılan yorum", OlusturmaTarih = DateTime.Now, Makale = makale3, Uye = uye };
            //Yorum yorum4 = new Yorum() { Icerik = "Makale 4 için yazılan yorum", OlusturmaTarih = DateTime.Now, Makale = makale4, Uye = uye };
            //Yorum yorum5 = new Yorum() { Icerik = "Makale 5 için yazılan yorum", OlusturmaTarih = DateTime.Now, Makale = makale5, Uye = uye };
            //Yorum yorum6 = new Yorum() { Icerik = "Makale 6 için yazılan yorum", OlusturmaTarih = DateTime.Now, Makale = makale6, Uye = uye };

            //db.Yorums.Add(yorum1);
            //db.Yorums.Add(yorum2);
            //db.Yorums.Add(yorum3);
            //db.Yorums.Add(yorum4);
            //db.Yorums.Add(yorum5);
            //db.Yorums.Add(yorum6);

            //Etiket etiket1 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "Asp.Net", Makales = new List<Makale>() { makale1, makale2, makale3, makale4, makale6 } };
            //Etiket etiket2 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "PHP", Makales = new List<Makale>() { makale5, makale3, makale2, makale1 } };
            //Etiket etiket3 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "Java", Makales = new List<Makale>() { makale2, makale4, makale5 } };
            //Etiket etiket4 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "C#", Makales = new List<Makale>() { makale5, makale4 } };
            //Etiket etiket5 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "Ruby", Makales = new List<Makale>() { makale5, makale6 } };
            //Etiket etiket6 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "C++", Makales = new List<Makale>() { makale5, makale2 } };
            //Etiket etiket7 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "D", Makales = new List<Makale>() { makale5, makale1 } };
            //Etiket etiket8 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "Pyhton", Makales = new List<Makale>() { makale1, makale4 } };
            //Etiket etiket9 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "JSF", Makales = new List<Makale>() { makale5, makale4 } };
            //Etiket etiket10 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "JSP", Makales = new List<Makale>() { makale5, makale3, makale6 } };
            //Etiket etiket11 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "XCode", Makales = new List<Makale>() { makale5, makale4, makale1 } };
            //Etiket etiket12 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "ColdFusion", Makales = new List<Makale>() { makale5, makale2 } };
            //Etiket etiket13 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "Pascal", Makales = new List<Makale>() { makale5, makale1, makale3 } };
            //Etiket etiket14 = new Etiket() { OlusturmaTarih = DateTime.Now, Icerik = "Cobol", Makales = new List<Makale>() { makale5, makale4, makale3, makale1, makale2 } };

            //db.Etikets.Add(etiket1);
            //db.Etikets.Add(etiket2);
            //db.Etikets.Add(etiket3);
            //db.Etikets.Add(etiket4);
            //db.Etikets.Add(etiket5);
            //db.Etikets.Add(etiket6);
            //db.Etikets.Add(etiket7);
            //db.Etikets.Add(etiket8);
            //db.Etikets.Add(etiket9);
            //db.Etikets.Add(etiket10);
            //db.Etikets.Add(etiket11);
            //db.Etikets.Add(etiket12);
            //db.Etikets.Add(etiket13);
            //db.Etikets.Add(etiket14);

            //db.SaveChanges();
        }
    }
}