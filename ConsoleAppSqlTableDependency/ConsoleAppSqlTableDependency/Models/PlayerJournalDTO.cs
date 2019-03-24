using ConsoleAppSqlTableDependency.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSqlTableDependency.Models
{
    public class PlayerJournalDTO
    {
        public JournalItem JournalItem { get; set; }
        public PlayerDetail PlayerDetail { get; set; }
        public DummyNotification DummyNotification { get; set; }
    }
}
