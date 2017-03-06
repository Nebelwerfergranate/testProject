using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sandbox_Products.DAL;
using Sandbox_Products.DAL.Core;
using System.Data.SqlClient;

namespace Sandbox_Products.BLL.Log
{
    public class LogsManager
    {
        private UnitOfWork _db;

        public LogsManager(UnitOfWork db)
        {
            _db = db;
        }

        public void LogException(Exception ex, bool handled)
        {
            // I am not sure it works...
            // todo - check
            if (ex is SqlException)
            {
                return;
            }

            Exception deepestExpection = GetDeepestException(ex);

            Logs log = new Logs()
            {
                Text = deepestExpection.Message,
                DateTimeCreated = DateTime.Now,
                Handled = handled
            };

            _db.SandboxRepository.Add(log);
            _db.Save();
        }

        private Exception GetDeepestException(Exception ex)
        {
            if(ex.InnerException != null)
            {
                return GetDeepestException(ex.InnerException);
            }

            return ex;
        }
    }
}