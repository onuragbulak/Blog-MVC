using MvcProjesi.Data;
using MvcProjesi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjesi.UnitOfWork
{
    public class MUnitOfWork : IDisposable
    {
        private MvcProjesiContext db = new MvcProjesiContext();
        private GenericRepository<Makale> makaleRepository;
        private GenericRepository<Uye> uyeRepository;
        private GenericRepository<Yorum> yorumRepository;
        private GenericRepository<Etiket> etiketRepository;


        public GenericRepository<Makale> MakaleRepository
        {
            get
            {
                if (this.makaleRepository == null)
                {
                    this.makaleRepository = new GenericRepository<Makale>(db);
                }
                return makaleRepository;
            }
        }
        public GenericRepository<Uye> UyeRepository
        {
            get
            {
                if (this.uyeRepository == null)
                {
                    this.uyeRepository = new GenericRepository<Uye>(db);
                }
                return uyeRepository;
            }
        }
        public GenericRepository<Yorum> YorumRepository
        {
            get
            {
                if (this.yorumRepository == null)
                {
                    this.yorumRepository = new GenericRepository<Yorum>(db);
                }
                return yorumRepository;
            }
        }
        public GenericRepository<Etiket> EtiketRepository
        {
            get
            {
                if (this.etiketRepository == null)
                {
                    this.etiketRepository = new GenericRepository<Etiket>(db);
                }
                return etiketRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
