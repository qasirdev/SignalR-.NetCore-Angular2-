//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleAppSqlTableDependency.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class JournalItem
    {
        public long UserId { get; set; }
        public long JournalItemId { get; set; }
        public int PlayerId { get; set; }
        public int BoughtPrice { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> SoldPrice { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> SoldDate { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual PlayerDetail PlayerDetail { get; set; }
    }
}