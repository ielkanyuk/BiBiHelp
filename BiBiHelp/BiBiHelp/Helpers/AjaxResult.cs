using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BiBiHelp.Helpers.Enum;

namespace BiBiHelp.Helpers
{
    public class AjaxResult : JsonResult
    {
        private ReturnViewType _returnViewType;
        private ViewResultBase _view;
        private object _objectForClient;

        public AjaxResult(object objectForClient, ReturnViewType returnViewType = ReturnViewType.JsonForTemplate)
        {
            _returnViewType = returnViewType;
            _objectForClient = objectForClient;
        }

        /// <summary>
        /// Рендерим вьюшку в виде строки
        /// </summary>
        public string GetViewHtml(ControllerContext context, bool Footer = false)
        {
            IView iView = null;
            ViewResultBase view = _view;

            //если не указано название вьюшки, то ищем её в ControllerContext, иначе достаем из переданной в конструктор вьюшки
            string actionName = view == null || string.IsNullOrEmpty(view.ViewName) ? context.RouteData.GetRequiredString("action") : view.ViewName;

            //в зависимости от того является ли вьюшка partialView или View ищем её разными методами
            if (view is PartialViewResult)
                iView = ViewEngines.Engines.FindPartialView(context, actionName).View;
            else if (view is ViewResult)
                iView = ViewEngines.Engines.FindView(context, actionName, (view as ViewResult).MasterName).View;
            else
                return string.Empty;


            //создаем контекст вьюшки из собранной информации и рендерим в виде html
            using (StringWriter viewHtmlWriter = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(context, iView, view.ViewData, view.TempData, viewHtmlWriter);


                viewContext.View.Render(viewContext, viewHtmlWriter);

                string res = viewHtmlWriter.GetStringBuilder().ToString();

                return res;
            }
        }

        public AjaxResultForClient GetResult(ControllerContext context)
        {
            context.Controller.ViewBag.AjaxResultType = _returnViewType;
            AjaxResultForClient ajaxResultForClient;

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();


            if (_returnViewType.HasFlag(ReturnViewType.JsonForTemplate))
            {
                ajaxResultForClient = new AjaxResultForClient
                {
                    ReturnViewType = _returnViewType,
                    JsonForTemplate = javaScriptSerializer.Serialize(_objectForClient)
                };
            }
            else
            {
                ajaxResultForClient = new AjaxResultForClient
                {
                    ReturnViewType = _returnViewType
                };
            }

            return ajaxResultForClient;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            JsonResult result = new JsonResult();

            result.Data = GetResult(context);

            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.ExecuteResult(context);
        }
    }
}