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
    
    public partial class ProductsOperationsTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductsOperationsTypes()
        {
            this.ProductsOperations = new HashSet<ProductsOperations>();
        }
    
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string DisplayColor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductsOperations> ProductsOperations { get; set; }
    }
}
