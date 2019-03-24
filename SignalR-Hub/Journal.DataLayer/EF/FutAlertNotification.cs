using System;
using System.Collections.Generic;

namespace Journal.DataLayer.EF
{
    public partial class DummyAlertNotification
    {
        public long NotificationId { get; set; }
        public long? UserId { get; set; }
        public int PlayerId { get; set; }
        public short PlatformId { get; set; }
        public int? GreaterThan { get; set; }
        public int? SmallerThan { get; set; }
        public short NotificationTypeMask { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? CurrentPrice { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public PlayerDetail Player { get; set; }
    }
}
