using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiBiHelp.Models.Base
{
    /// <summary>
    /// Контейнер для данных, передаваемых на клиент(преимущемтвенно используется для построения списков)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataForTemplateBase<T>
    {
        public DataForTemplateBase()
        {
            TmplMgrInfo = new TmplMgrInfo();
            Data = new List<T>();
        }

        /// <summary>
        /// Собственно массив данных
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// Информация о менеджере шаблонов
        /// </summary>
        public TmplMgrInfo TmplMgrInfo { get; set; }

        // ПЕЙДЖИНГ СПИСКОВ

        public string Action { get; set; }

        public string Controller { get; set; }

        public bool HasNextPage
        {
            get { return PageNumber < PageCount; }
        }

        public bool HasPreviousPage
        {
            get { return PageNumber > 1; }
        }

        public int PageCount
        {
            get
            {
                return PageSize != 0 ? Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalItemCount) / PageSize)) : 0;
            }
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItemCount { get; set; }

        //public string TotalItemCountString
        //{
        //    get
        //    {
        //        return ControlHelper.GetTotalItemCountString(TotalItemCount);
        //        //string pagesCountText = "";
        //        //if (TotalItemCount % 10 == 1 && TotalItemCount % 100 != 11)
        //        //{
        //        //    pagesCountText = String.Format(CommonResource.Record, TotalItemCount);
        //        //}
        //        //if (TotalItemCount % 10 >= 2 && TotalItemCount % 10 <= 4 && TotalItemCount % 100 != 12 && TotalItemCount % 100 != 13 && TotalItemCount % 100 != 14)
        //        //{
        //        //    pagesCountText = String.Format(CommonResource.Records, TotalItemCount);
        //        //}
        //        //if (TotalItemCount % 10 > 4 || TotalItemCount % 10 == 0 || TotalItemCount % 100 == 11 || TotalItemCount % 100 == 12 || TotalItemCount % 100 == 13 || TotalItemCount % 100 == 14)
        //        //{
        //        //    pagesCountText = String.Format(CommonResource.ManyRecords, TotalItemCount);
        //        //}

        //        //return pagesCountText;
        //    }
        //}
    }
}