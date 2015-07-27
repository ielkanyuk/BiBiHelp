using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BiBiHelp.Model.Interfaces;
using BiBiHelp.Model.Interfaces.Services;
using BiBiHelp.Model.Services;

namespace BiBiHelp.Helpers
{
    public class GlobalVariables
    {
        static GlobalVariables()
        {
            ServiceManager = ServicesInstance.GetServiceManager();
        }

        public static IServiceManager ServiceManager { get; private set; }

        /// <summary>
        /// Стандартный класс ASP.NET MVC для построения урлов в соответствии с роутингом
        /// </summary>
        public static UrlHelper UrlHelper
        {
            get { return new UrlHelper(HttpContext.Current.Request.RequestContext); }
        }

        /// <summary>
        /// Стандартный класс ASP.NET MVC для построения урлов в соответствии с роутингом
        /// </summary>
        public static string GetVariableName(object variable)
        {
            return variable.GetType().GetProperties()[0].Name;
        }

        public static IUser CurrentUser
        {
            get
            {
                return ServiceManager.GetCurrentUser();
            }
        }
    }
}