using System;
using System.Collections.Generic;

namespace Journal.DataLayer.EF
{
    public partial class JournalItem
    {
        public long JournalItemId { get; set; }
        public long JournalId { get; set; }
        public int PlayerId { get; set; }
        public int BoughtPrice { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int? SoldPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public PlayerDetail Player { get; set; }
    }
}
