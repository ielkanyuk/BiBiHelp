using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BiBiHelp.Model.Interfaces.Services;

namespace BiBiHelp.Authorization
{
    public class ClientAuthService
    {
        IServiceManager _manager;

        public ClientAuthService(IServiceManager manager)
        {
            _manager = manager;
        }

        public void Login(string email, bool remember, double? clientTimeOffset)
        {
            FormsAuthentication.SetAuthCookie(email, remember);
            _manager.UserService.OnLogin(email, clientTimeOffset);

        }
    }
}