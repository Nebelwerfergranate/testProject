using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Models
{
    public class JqGridResult<T>
    {
        public List<T> rows { get; set; }
        public Int32 total { get; set; }
        public Int32 page { get; set; }
        public Boolean result { get; set; }
        public String message { get; set; }
        public List<String> errors { get; set; } = new List<String>(); 
    }
}