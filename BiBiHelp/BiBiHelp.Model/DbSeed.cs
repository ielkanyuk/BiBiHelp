using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BiBiHelp.Model
{
    /// <summary>
    /// Класс заполнения БД первоначальными данными
    /// </summary>
    internal class DbSeed
    {
        private readonly Container _context;

        internal DbSeed(Container context)
        {
            _context = context;
        }

        internal void Seed()
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ru-RU");

            _context.SaveChanges();
        }
    }
}
