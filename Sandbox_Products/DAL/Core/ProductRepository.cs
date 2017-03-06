using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Core.Objects;
using Sandbox_Products.Utils;

namespace Sandbox_Products.DAL.Core
{
    public class ProductRepository
    {
        protected MsSqlConn _context;

        public ProductRepository(MsSqlConn context)
        {
            _context = context;
        }

        public Int64 GetProductCountById(int id)
        {
            Int64? result = 0;

            try
            {
                ObjectParameter count = new ObjectParameter("count", typeof(Int64?));
                _context.GetProductCountById(id, count);

                result = (Int64?)count.Value;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

            return result.GetValueOrDefault(0);
        }
    }
}