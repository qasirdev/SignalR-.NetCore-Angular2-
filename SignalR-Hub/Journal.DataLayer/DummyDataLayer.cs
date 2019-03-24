using Journal.DataLayer.EF;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Journal.DataLayer
{
    public class DummyDataLayer
    {
        public List<PlayerJournalModel> Gets(string journalNumber)
        {
            var playerJournalModels = new List<PlayerJournalModel>();

            try
            {

                using (var context = new SignalRDemoContext())
                {
                    var journalItems = context.JournalItem.Where(j => j.JournalId == Convert.ToInt32(journalNumber)).ToList();

                    foreach (var journalItem in journalItems)
                    {

                        var playerItem = context.PlayerDetail.Where(p => p.PlayerId == journalItem.PlayerId).FirstOrDefault();
                        var notificationItem = context.DummyAlertNotification.Where(n => n.UserId == journalItem.JournalId && n.PlayerId == journalItem.PlayerId && !n.IsDeleted).FirstOrDefault();

                        string alertPrice = "", isExpired="";
                            alertPrice = (notificationItem != null && notificationItem.GreaterThan != null) ? "Over " + notificationItem.GreaterThan.ToString() : "";
                            alertPrice = (alertPrice == "" && notificationItem != null && notificationItem.SmallerThan != null) ? "Under " + notificationItem.SmallerThan.ToString() : alertPrice;
                        if ((notificationItem != null && notificationItem.ExpiryDate != null))
                        {
                            isExpired = (notificationItem.ExpiryDate < DateTime.Now) ? "True" : "False";
                        }

                        // Create a new product
                        PlayerJournalModel playerJournalModel = new PlayerJournalModel
                        {
                            JournalItemId = journalItem.JournalItemId.ToString(),
                            JournalId = journalItem.JournalId.ToString(),
                            PlayerId = journalItem.PlayerId.ToString(),
                            BoughtPrice = journalItem.BoughtPrice.ToString("0,0", CultureInfo.InvariantCulture),
                            Quantity = journalItem.Quantity.ToString(),
                            Description = journalItem.Description.ToString(),
                            SoldPrice = journalItem.SoldPrice.Value.ToString("0,0", CultureInfo.InvariantCulture),
                            CreatedDate = journalItem.CreatedDate.ToString(),
                            SoldDate = journalItem.SoldDate.ToString(),
                            DeletedDate = journalItem.DeletedDate.ToString(),

                            NotificationId = (notificationItem != null) ? notificationItem.NotificationId.ToString() : "",
                            AlertPrice = alertPrice,
                            IsExpired = isExpired,

                            PlayerFullName = playerItem.PlayerFullName.ToString(),
                            PlayerCardTypeId =  ConvertPlayerCardType(playerItem.PlayerCardTypeId),
                            FacePhotoUrl = playerItem.FacePhotoUrl.ToString(),
                            BadgePhotoUrl = playerItem.BadgePhotoUrl.ToString(),
                            NationPhotoUrl = playerItem.NationPhotoUrl.ToString(),
                            Overall = playerItem.Overall.ToString(),
                            CurrentPricePS4 = playerItem.CurrentPricePs4.Value.ToString("0,0", CultureInfo.InvariantCulture),
                            Position = playerItem.Position.ToString(),
                            LastUpdatedDate = CalculateTiem(playerItem.LastUpdatedDate.Value),

                            TotalInvestment = (journalItem.Quantity * journalItem.BoughtPrice).ToString("0,0", CultureInfo.InvariantCulture),
                            ProfitLossPerCard = ((int)(journalItem.SoldPrice - journalItem.BoughtPrice)).ToString("0,0", CultureInfo.InvariantCulture),
                            TotalInvestmentPL = ((int)((journalItem.SoldPrice - journalItem.BoughtPrice) * journalItem.Quantity)).ToString("0,0", CultureInfo.InvariantCulture),
                            OverallColor = ConvertPlayerCardTypeColor(playerItem.PlayerCardTypeId)
                        };

                        playerJournalModels.Add(playerJournalModel);
                    }
                }
            }
            catch (Exception e)
            {
                //retMessage = e.ToString();
            }

            return playerJournalModels;
        }

        private string ConvertPlayerCardType(int cardTypeId)
        {
            var baseUrl = "/assets/small_icons/";
            var result = "";
            switch (cardTypeId)
            {
                case 1:
                    result = "small_rare_gold"; break;
                case 2:    
                    result = "small_common_gold"; break;
                case 3:
                    result = "small_totw_gold"; break;
                case 4:
                    result = "small_rare_silver"; break;
                case 5:    
                    result = "small_common_silver"; break;
                case 6:    
                    result = "small_totw_silver"; break;
                case 7:    
                    result = "small_rare_bronze"; break;
                case 8:    
                    result = "small_common_bronze"; break;
                case 9:    
                    result = "small_totw_bronze"; break;
                case 10:   
                    result = "small_icon"; break;
                case 11:   
                    result = "small_otw"; break;
                case 12:   
                case 13:   
                    result = "small_totgs"; break;
                case 15:   
                    result = "small_Dummymas"; break;
                case 16:   
                    result = "small_toty"; break;
                case 17:   
                    result = "small_tots_gold"; break;
                case 18:   
                    result = "small_scream"; break;
                case 19:   
                    result = "small_potm"; break;
                case 20:   
                    result = "small_motm"; break;
                case 21:   
                    result = "small_sbc"; break;
                case 22:   
                    result = "small_hero"; break;
                case 23:   
                    result = "small_ptg"; break;
                case 24:   
                    result = "small_birthday"; break;
                case 25:   
                    result = "small_motm_europe"; break;
                case 26:   
                    result = "small_Dummyties"; break;
                default:   
                    result = "small_rare_gold"; break;
            }

            return baseUrl + result + ".png" ;
        }

        private string ConvertPlayerCardTypeColor(int cardTypeId)
        {
            var result = "";
            switch (cardTypeId)
            {
                case 1:
                case 2:
                case 4:
                case 5:
                case 7:
                case 8:
                case 12:
                case 13:
                case 18:
                case 21:
                case 24:
                    result = "#353333"; break;
                case 3:
                case 6:
                case 9:
                case 10:
                case 11:
                case 15:
                case 16:
                case 17:
                case 19:
                case 20:
                case 22:
                case 23:
                case 25:
                case 26:
                    result = "White"; break;
                default:
                    result = "Black"; break;
            }


            return result;
        }

        private string CalculateTiem(DateTime LastUpdatedDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - LastUpdatedDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }
    }
}
