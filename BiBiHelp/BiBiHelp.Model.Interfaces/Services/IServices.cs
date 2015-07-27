using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiBiHelp.Model.Interfaces.Services
{
    public interface IServices : IDisposable
    {
        /// <summary>
        /// Сервис работы с пользователями
        /// </summary>
        IUserService UserService { get; }

        /// <summary>
        /// Сервис сообщений
        /// </summary>
        IMessageService MessageService { get; }

        /// <summary>
        /// Сохранить данные в БД
        /// </summary>
        int SaveChanges();
    }
}
