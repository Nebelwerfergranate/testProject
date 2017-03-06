using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Sandbox_Products.Models;

namespace Sandbox_Products.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {      
        //
        // GET: /Manage/Index
        public ActionResult Index()
        {

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }

#region Helpers

#endregion
    }
}