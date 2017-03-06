using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Utils.CrudHelpers
{
    public class PagingModel
    {
        private const String _ASCENDING = "asc";
        private const String _DESCENDING = "desc";

        public Int32 Page { get; set; }
        public Int32 Rows { get; set; }
        public String Sidx { get; set; }
        public SortDirections Sord { get; set; }

        public PagingModel(Int32 rows, Int32 page, String sidx, String sord)
        {
            SetDirection(sord);
            Rows = rows;
            Page = page;
            Sidx = sidx;
        }

        private void SetDirection(String sord)
        {
            switch (sord)
            {
                case _ASCENDING:
                    Sord = SortDirections.Ascending;
                    break;
                case _DESCENDING:
                    Sord = SortDirections.Descending;
                    break;
                default:
                    throw new NotImplementedException(GetNotImplementedMessage(sord));
            }
        }

        private String GetNotImplementedMessage(String sord)
        {
            return String.Format("Sorting direction '{0}' is not implemented.", sord);
        }
    }
}