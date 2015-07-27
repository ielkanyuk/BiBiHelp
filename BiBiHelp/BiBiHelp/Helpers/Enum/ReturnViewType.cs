using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiBiHelp.Helpers.Enum
{
    /// <summary>
    /// Тип данных, который AjaxResult возвращает клиенту
    /// </summary>
    [Flags]
    public enum ReturnViewType
    {
        /// <summary>
        /// Возвращаем json для jquery template
        /// </summary>
        JsonForTemplate = 1,

        Json = 2,

        OperationFailed = 3 ,

        OperationSuccess = 4
    }
}