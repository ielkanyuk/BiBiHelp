using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BiBiHelp.Authorization;
using BiBiHelp.Helpers;
using BiBiHelp.Model.Interfaces;
using BiBiHelp.Model.Interfaces.Services;

namespace BiBiHelp.Controllers
{
    public class BaseController : Controller
    {
        protected ClientAuthService ClientAuthService { get; private set; }

        public IServiceManager ServiceManager { get; private set; }

        protected BaseController()
        {
            ServiceManager = ServicesInstance.GetServiceManager();
            ClientAuthService = new ClientAuthService(ServiceManager);
        }

        /// <summary>
        /// Возвращает текущего пользователя
        /// </summary>
        protected IUser CurrentUser
        {
            get { return GlobalVariables.CurrentUser; }
        }

        /// <summary>
        /// Для построения различных урлов в action методах
        /// </summary>
        private UrlHelper _urlHelper;
        protected UrlHelper UrlHelper
        {
            get
            {
                if (_urlHelper == null)
                    _urlHelper = new UrlHelper(Request.RequestContext);

                return _urlHelper;
            }
        }
    }
}
