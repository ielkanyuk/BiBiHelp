using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiBiHelp.Model.Interfaces.Services
{
    public interface IMessageService
    {
        IEnumerable<IMessage> GetMessageList(int region);

        IMessage GetMessageById(Guid messageId);

        IMessage AddMessage(string text, int region, bool anonymous, Guid? parent = null);
    }
}
