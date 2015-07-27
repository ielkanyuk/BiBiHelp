using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiBiHelp.Models.Base
{
    /// <summary>
    /// Инормация о состоянии менеджера шаблонов
    /// </summary>
    public class TmplMgrInfo
    {
        /// <summary>
        /// Идентификатор менеджера шаблонов
        /// </summary>
        public string TmplMgrId { get; set; }

        /// <summary>
        /// Урл по которому необходимо загрузить шаблон для отображения данных
        /// </summary>
        public string TemplateUrl { get; set; }

        /// <summary>
        /// Урл по которому можно запросить данные для текущего менеджера шаблонов
        /// </summary>
        public string LoadDataUrl { get; set; }

        /// <summary>
        /// Dom элемент в котором выводится список сущностей
        /// </summary>
        public string Placeholder { get; set; }
    }
}