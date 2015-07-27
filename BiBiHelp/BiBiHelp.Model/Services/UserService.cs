using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BiBiHelp.Model.Interfaces;
using BiBiHelp.Model.Interfaces.Authorization;
using BiBiHelp.Model.Interfaces.Common;
using BiBiHelp.Model.Interfaces.Services;

namespace BiBiHelp.Model.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(Services services) : base(services)
        {
        }

        public IUser GetCurrentUser()
        {
            return GetByIdInternal(new Guid("2CD97175-64AE-4FD9-B3CD-40A861C436A7"));
            //return GetByEmail(Thread.CurrentPrincipal.Identity.Name);
        }

        public IUser GetByEmail(string email)
        {
            return Services.Container.Users.FirstOrDefault(x => x.Email == email && x.Status != EEntityStatus.Deleted);
        }

        public IUser GetById(Guid id)
        {
            return GetByIdInternal(id);
        }

        private User GetByIdInternal(Guid id)
        {
            return Services.Container.Users.FirstOrDefault(x => x.Id == id && x.Status != EEntityStatus.Deleted);
        }

        /// <summary>
        /// Проверяет, можно ли войти с заданными учетными данными.
        /// </summary>
        public ELoginResult CanLogin(string email, string password)
        {
            if (!Services.Container.Users.Any())
                return ELoginResult.UserNotFound;

            User user = Services.Container.Users.FirstOrDefault(x => x.Email == email.ToLower());

            if (user == null)
                return ELoginResult.UserNotFound;
            
                if (user.Status == EEntityStatus.Blocked)
                    return ELoginResult.UserBlocked;

                if (user.Status == EEntityStatus.Deleted)
                    return ELoginResult.UserRemoved;


            return !new HashProvider().CheckHash(password + user.PasswordSalt, user.Password) ? ELoginResult.InvalidPassword : ELoginResult.Success;
        }

        /// <summary>
        /// Действия при авторизации
        /// </summary>
        public void OnLogin(string email, double? clientTimeOffset)
        {
            User user = Services.Container.Users.FirstOrDefault(x => x.Email == email.ToLower());
            user.LastLoginDate = DateTime.UtcNow;
            Services.SaveChanges();
            UpdateUserTimeZone(user, clientTimeOffset);
        }

        /// <summary>
        /// Обновляет часовой пояс пользователя
        /// </summary>
        public void UpdateUserTimeZone(IUser user, double? usersLocalTimeOffset)
        {
            if (user != null && usersLocalTimeOffset.HasValue && usersLocalTimeOffset != user.TimeZone)
            {
                user.TimeZone = usersLocalTimeOffset;
                Services.SaveChanges();
            }
        }
    }
}
