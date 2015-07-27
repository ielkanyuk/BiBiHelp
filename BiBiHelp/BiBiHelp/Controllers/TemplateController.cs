using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BiBiHelp.Models;

namespace BiBiHelp.Controllers
{
    public class TemplateController : Controller
    {
        /// <summary>
        /// jquery шаблон, используемый для сщщбщений
        /// </summary>
        /// <returns></returns>
        [OutputCache(CacheProfile = "jqTemplateCacheProfile")]
        public ActionResult jqMessageTemplate()
        {
            return PartialView("Messages/jqMessageTemplate", new jQTemplate<MessageModel>());
        }

    }
}
