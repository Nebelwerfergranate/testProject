//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sandbox_Products.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductsOperations
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string OperatorName { get; set; }
        public string OperationTypeId { get; set; }
        public short ProductsCount { get; set; }
        public System.DateTime DateTimeCreated { get; set; }
    
        public virtual Products Products { get; set; }
        public virtual ProductsOperationsTypes ProductsOperationsTypes { get; set; }
    }
}