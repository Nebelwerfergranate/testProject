#define DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox_Products.DAL;
using Sandbox_Products.Utils;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data;

namespace Sandbox_Products.DAL.Core
{
    public class UnitOfWork : IDisposable
    {
        private MsSqlConn _db;
        private SandboxRepository _sandboxRepository;
        private ProductRepository _productRepository;
        private Boolean _disposed = false;

        public UnitOfWork()
        {
            _db = new MsSqlConn();
#if DEBUG
            _db.Database.Log = message => System.Diagnostics.Debug.Write(message);
#endif
        }

        // lazy load doesn't have much sense here...
        public SandboxRepository SandboxRepository
        {
            get
            {
                if (_sandboxRepository == null)
                {
                    _sandboxRepository = new SandboxRepository(_db);
                }

                return _sandboxRepository;
            }
        }

        public ProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_db);
                }

                return _productRepository;
            }
        }

        public DbContextTransaction GetTransaction()
        {
            return _db.Database.BeginTransaction();
        }

        public DbContextTransaction GetTransaction(IsolationLevel isolationLevel)
        {
            return _db.Database.BeginTransaction(isolationLevel);
        }

        public void Save()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public virtual void Dispose(Boolean disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
