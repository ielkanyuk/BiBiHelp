using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiBiHelp.Model.Interfaces;
using BiBiHelp.Model.Interfaces.Services;

namespace BiBiHelp.Model.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly IServices _serviceManager;

        public ServiceManager()
        {
            _serviceManager = new Services();
        }

        public IUserService UserService { get { return _serviceManager.UserService; } }
        public IMessageService MessageService { get { return _serviceManager.MessageService; } }
        
        public IUser GetCurrentUser()
        {
            return _serviceManager.UserService.GetCurrentUser();
        }

        public int SaveChanges(bool internalMode = false)
        {
            return _serviceManager.SaveChanges();
        }
        
        public void Dispose()
        {
            _serviceManager.Dispose();
        }
    }
}
