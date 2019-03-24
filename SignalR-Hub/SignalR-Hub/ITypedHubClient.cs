//using SignalRHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Journal.DataLayer.EF;

namespace SignalRHub
{
    public interface ITypedHubClient
    {      
        Task BroadcastMessage(
            List<PlayerJournalModel> playerJournalModel
        );
    }
}
