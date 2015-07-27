using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BiBiHelp.Helpers;
using BiBiHelp.Model.Interfaces;
using BiBiHelp.Models;
using BiBiHelp.Models.Presenters;

namespace BiBiHelp.Controllers
{
    [Authorize]
    public class AppController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMessageList(int region, int pageIndex = 1, int pageSize = 10)
        {
                MessageListPresenter data = new MessageListPresenter();

                data.Data = new List<MessageModel>();

                IEnumerable<IMessage> items = ServiceManager.MessageService.GetMessageList(region);

                data.PageNumber = pageIndex;
                data.PageSize = pageSize;
                data.TotalItemCount = items.Count();

                items = items.OrderByDescending(x => x.DateCreated).Skip((pageIndex - 1) * pageSize).Take(pageSize);

                foreach (IMessage item in items)
                {
                    data.Data.Add(new MessageModel(item, CurrentUser));
                }

                return new AjaxResult(data);
        }

        [HttpPost]
        public ActionResult AddMessage(string text, int region, bool anonymous)
        {
            IMessage newMessage = ServiceManager.MessageService.AddMessage(text, region, anonymous);

            ServiceManager.SaveChanges();

            MessageListPresenter data = new MessageListPresenter();

            data.Data = new List<MessageModel>();

            data.Data.Add(new MessageModel(newMessage, CurrentUser));

            return new AjaxResult(data);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Support()
        {
            return View();
        }

        public ActionResult Rules()
        {
            return View();
        }
    }
}
