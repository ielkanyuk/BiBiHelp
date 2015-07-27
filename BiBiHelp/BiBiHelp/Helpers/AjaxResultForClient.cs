using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BiBiHelp.Helpers.Enum;

namespace BiBiHelp.Helpers
{
    public class AjaxResultForClient
    {
        /// <summary>
        /// Каким образом должен быть представлен результат на сервере
        /// </summary>
        public ReturnViewType ReturnViewType { get; set; }

        /// <summary>
        /// Html, которую сформировала вьюшка
        /// </summary>
        public string ViewHtmlContent { get; set; }

        /// <summary>
        /// Json для клиента
        /// </summary>
        public string JsonData { get; set; }

        /// <summary>
        /// Json-данные для шаблона
        /// </summary>
        public string JsonForTemplate { get; set; }

        /// <summary>
        /// Урл откуда можно загрузить jQuery шаблон для данных
        /// </summary>
        public string TemplateUrl { get; set; }
    }
}