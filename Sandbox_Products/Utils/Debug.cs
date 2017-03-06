using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sandbox_Products.DAL.Core;
using Sandbox_Products.BLL.Log;

namespace Sandbox_Products.Utils
{
    public class Debug
    {
        public static void LogError(Exception ex)
        {
            LogError(ex, false);
        }

        public static void LogError(Exception ex, bool handled)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                LogsManager logManager = new LogsManager(db);
                logManager.LogException(ex, handled);
            }
        }
    }
}