using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiBiHelp.Model.Interfaces.Common;

namespace BiBiHelp.Model.Interfaces
{
    public interface IUser : IEntity, IStatus
    {
        /// <summary>
        /// Имя
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Эл. адрес
        /// </summary>
        string Email { get; }

        byte[] Password { get; set; }

        string PasswordSalt { get; set; }

        DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Часовой пояс пользователя.Смещение в часах
        /// </summary>
        double? TimeZone { get; set; }

        /// <summary>
        /// Переводит время в формате UTC в время в часовом поясе пользователя
        /// </summary>
        /// <param name="utcDate"></param>
        /// <returns></returns>
        DateTime UtcToUserDate(DateTime utcDate);
    }
}
