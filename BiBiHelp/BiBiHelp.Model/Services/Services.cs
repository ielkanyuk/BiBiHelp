using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiBiHelp.Model.Interfaces;
using BiBiHelp.Model.Interfaces.Services;

namespace BiBiHelp.Model.Services
{
    public class Services: IServices
    {
        public Services()
        {
            Container = new Container();
        }

        internal Container Container { get; private set; }


        private UserService _userService;
        internal UserService UserService
        {
            get
            {
                return _userService = _userService ?? new UserService(this);
            }
        }
        IUserService IServices.UserService { get { return UserService; } }
        
        private MessageService _messageService;
        internal MessageService MessageService
        {
            get
            {
                return _messageService = _messageService ?? new MessageService(this);
            }
        }
        IMessageService IServices.MessageService { get { return MessageService; } }

        public int SaveChanges()
        {
            return Container.SaveChanges();
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        public IUser GetCurrentUser()
        {
            return UserService.GetCurrentUser();
        }
    }
}
