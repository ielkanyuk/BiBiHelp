using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiBiHelp.Model.Entities;
using BiBiHelp.Model.Interfaces;
using BiBiHelp.Model.Interfaces.Services;

namespace BiBiHelp.Model.Services
{
    public class MessageService : BaseService, IMessageService
    {
        public MessageService(Services services) : base(services)
        {
        }

        public IEnumerable<IMessage> GetMessageList(int region)
        {
            return Services.Container.Messages.Include("Author").Where(x => x.Region == region);
        }

        public IMessage GetMessageById(Guid messageId)
        {
            return GetMessageByIdInternal(messageId);
        }

        private Message GetMessageByIdInternal(Guid messageId)
        {
            return Services.Container.Messages.Include("Author").FirstOrDefault(x => x.Id == messageId);
        }

        public IMessage AddMessage(string text, int region, bool anonymous, Guid? parent = null)
        {
            if (parent.HasValue)
            {
                Message parentMessage = GetMessageByIdInternal(parent.Value);
                return Services.Container.Messages.Add(new Message(text, anonymous, region, (User)Services.UserService.GetCurrentUser(), parentMessage));
            }

            return Services.Container.Messages.Add(new Message(text, anonymous, region, (User)Services.UserService.GetCurrentUser()));
        }
    }
}
