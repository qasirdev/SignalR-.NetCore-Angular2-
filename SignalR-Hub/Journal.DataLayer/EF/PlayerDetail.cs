using System;
using System.Collections.Generic;

namespace Journal.DataLayer.EF
{
    public partial class PlayerDetail
    {
        public PlayerDetail()
        {
            DummyAlertNotification = new HashSet<DummyAlertNotification>();
            JournalItem = new HashSet<JournalItem>();
        }

        public int PlayerId { get; set; }
        public string PlayerExternalId { get; set; }
        public string PlayerDummywizId { get; set; }
        public string PlayerDummybinId { get; set; }
        public string PlayerDummyheadId { get; set; }
        public int PlayerCardTypeId { get; set; }
        public string PlayerShortName { get; set; }
        public int? Pace { get; set; }
        public int? Shooting { get; set; }
        public int? Passing { get; set; }
        public int? Dribbling { get; set; }
        public int? Defending { get; set; }
        public int? Physical { get; set; }
        public int? Age { get; set; }
        public string FacePhotoUrl { get; set; }
        public string BadgePhotoUrl { get; set; }
        public string NationPhotoUrl { get; set; }
        public int Overall { get; set; }
        public string Position { get; set; }
        public string PlayerDburl { get; set; }
        public string PlayerFullName { get; set; }
        public int? CurrentPricePs4 { get; set; }
        public int? CurrentPriceXbox { get; set; }
        public int? CurrentPricePc { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool? IsTradable { get; set; }

        public ICollection<DummyAlertNotification> DummyAlertNotification { get; set; }
        public ICollection<JournalItem> JournalItem { get; set; }
    }
}
