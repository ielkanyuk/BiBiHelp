using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BiBiHelp.Model.Services;

namespace BiBiHelp
{
    /// <summary>
    /// кешируем сервисы для каждого клиента 
    /// </summary>
    public static class ServicesInstance
    {
        /// <summary>
        /// получить ранее закешированный экземпляр сервиса для текущего пользователя
        /// </summary>
        /// <returns></returns>
        public static ServiceManager GetServiceManager()
        {
            ServiceManager serviceManager = new ServiceManager();
            return serviceManager;
        }
    }
}