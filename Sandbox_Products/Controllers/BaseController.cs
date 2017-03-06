using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sandbox_Products.DAL.Core;

namespace Sandbox_Products.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        private UnitOfWork _database;


        protected UnitOfWork Db
        {
            get
            {
                if (_database == null)
                {
                    _database = new UnitOfWork();
                }

                return _database;
            }
        }

        protected void GrabModelErrors(List<String> errorList)
        {
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errorList.Add(error.ErrorMessage);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _database != null)
            {
                _database.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}