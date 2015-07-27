using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiBiHelp.Model.Interfaces.Services
{
    /// <summary>
    /// Интерфейс библиотеки методов работы с сущностями БД
    /// </summary>
    public interface IServiceManager : IDisposable
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
        /// Текущий пользователь
        /// </summary>
        /// <returns></returns>
        IUser GetCurrentUser();

        /// <summary>
        /// Метод сохранения данных в БД IPPS
        /// </summary>
        /// <returns></returns>
        int SaveChanges(bool internalMode = false);
    }
}
