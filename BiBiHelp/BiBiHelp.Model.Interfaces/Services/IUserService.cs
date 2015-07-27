using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiBiHelp.Model.Interfaces.Authorization;

namespace BiBiHelp.Model.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Получить текущего пользователя
        /// </summary>
        IUser GetCurrentUser();

        IUser GetById(Guid id);

        IUser GetByEmail(string email);

        ELoginResult CanLogin(string email, string password);

        void OnLogin(string email, double? clientTimeOffset);
    }
}
