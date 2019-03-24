using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
//using SignalRHub.Models;
using System;
using System.Collections.Generic;
using Journal.DataLayer;
using Journal.DataLayer.EF;

namespace SignalRHub.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;

        public MessageController(IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("Gets/{journalNumber}")]
        public List<PlayerJournalModel> Gets(string journalNumber)
        {
            var playerJournalModelList = new List<PlayerJournalModel>();

            try
            {
                playerJournalModelList = new DummyDataLayer().Gets(journalNumber);
            }
            catch (Exception e)
            {
                //retMessage = e.ToString();
            }
            return playerJournalModelList;
        }


        [HttpPost]
        [Route("Post")]
        public string Post([FromBody]PlayerJournalModel playerJournalModel)
        {
            string retMessage = string.Empty;

            try
            {
                List<PlayerJournalModel> playerJournalModelList = new List<PlayerJournalModel> ();
                playerJournalModelList.Add(playerJournalModel);
                _hubContext.Clients.All.BroadcastMessage(playerJournalModelList);

                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }
            
            return retMessage;
        }

        [HttpPost]
        [Route("Posts")]
        public string Posts([FromBody]List<PlayerJournalModel> playerJournalModels)
        {
            string retMessage = string.Empty;

            try
            {
                //TODO: return a collection of models throguh EF on first time display of table
                _hubContext.Clients.All.BroadcastMessage(playerJournalModels);

                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }
    }
}