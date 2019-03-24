using System;
using System.Collections.Generic;
using System.Text;

namespace Journal.DataLayer.EF
{
    public class PlayerJournalModel
    {
        public string JournalItemId { get; set; }
        public string JournalId { get; set; }
        public string PlayerId { get; set; }
        public string BoughtPrice { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public string SoldPrice { get; set; }
        public string CreatedDate { get; set; }
        public string SoldDate { get; set; }
        public string DeletedDate { get; set; }

        public string NotificationId { get; set; }
        public string AlertPrice { get; set; }
        public string IsExpired { get; set; }
        
        public string PlayerFullName { get; set; }
        public string PlayerCardTypeId { get; set; }
        public string FacePhotoUrl { get; set; }
        public string BadgePhotoUrl { get; set; }
        public string NationPhotoUrl { get; set; }
        public string Overall { get; set; }
        public string CurrentPricePS4 { get; set; }
        public string Position { get; set; }
        public string LastUpdatedDate { get; set; }

        public string TotalInvestment { get; set; }
        public string ProfitLossPerCard { get; set; }
        public string TotalInvestmentPL { get; set; }
        public string OverallColor { get; set; }
    }
}
