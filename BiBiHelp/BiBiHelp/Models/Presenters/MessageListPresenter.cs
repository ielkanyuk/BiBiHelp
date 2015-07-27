using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BiBiHelp.Helpers;
using BiBiHelp.Models.Base;

namespace BiBiHelp.Models.Presenters
{
    public class MessageListPresenter : DataForTemplateBase<MessageModel>
    {
        public MessageListPresenter() 
        {
            TmplMgrInfo.Placeholder = ".iDataContainer";

            TmplMgrInfo.TemplateUrl = GlobalVariables.UrlHelper.Action("jqMessageTemplate", "Template");

            Action = "GetMessageList";

            Controller = "App";
        }
    }
}