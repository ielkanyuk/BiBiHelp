using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BiBiHelp.Helpers;
using BiBiHelp.Model.Interfaces.Services;
using BiBiHelp.Models;
using BiBiHelp.Models.Presenters;

namespace BiBiHelp.Hubs
{
    public class MessageHub : Microsoft.AspNet.SignalR.Hub
    {
        static MessageHub()
        {
            ServiceManager = ServicesInstance.GetServiceManager();
        }

        public static IServiceManager ServiceManager { get; private set; }

        public void BroadcastMessage(Guid messageid)
        {
            MessageListPresenter model = new MessageListPresenter();
            model.Data = new List<MessageModel>();
            model.Data.Add(new MessageModel(ServiceManager.MessageService.GetMessageById(messageid), ServiceManager.GetCurrentUser()));
            Clients.Others.WriteMessage(model);
        }
    }
}