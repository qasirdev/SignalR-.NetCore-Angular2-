using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsoleAppSqlTableDependency.Models
{
    public class PlayerDTO
    {
        public int PlayerId { get; set; }
        public string PlayerExternalId { get; set; }
        public string PlayerDummywizId { get; set; }
        public string PlayerDummybinId { get; set; }
        public string PlayerDummyheadId { get; set; }
        public int PlayerCardTypeId { get; set; }
        public string PlayerCardTypeDisplayName { get; set; }
        public string PlayerShortName { get; set; }
        public Nullable<int> Pace { get; set; }
        public Nullable<int> Shooting { get; set; }
        public Nullable<int> Passing { get; set; }
        public Nullable<int> Dribbling { get; set; }
        public Nullable<int> Defending { get; set; }
        public Nullable<int> Physical { get; set; }
        public Nullable<int> Age { get; set; }
        public int Overall { get; set; }
        public string Position { get; set; }
        public string PlayerDBURL { get; set; }
        public string PlayerFullName { get; set; }
        public string FacePhotoUrl { get; set; }
        public string BadgePhotoUrl { get; set; }
        public string NationPhotoUrl { get; set; }

        public int CurrentPricePS4 { get; set; }
        public int CurrentPriceXbox { get; set; }
        public int CurrentPricePC { get; set; }
    }
}