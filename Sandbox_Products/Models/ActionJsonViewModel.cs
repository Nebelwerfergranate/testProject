using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Models
{
    public class ActionJsonViewModel
    {
        public ActionJsonViewModel()
        {
            errors = new List<String>();
        }

        public Boolean result { get; set; }
        public String message { get; set; }
        public List<String> errors { get; set; }
        public Boolean needRefresh { get; set; }
    }
}