using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiBiHelp.Model.Entities;
using BiBiHelp.Model.Interfaces;
using BiBiHelp.Model.Interfaces.Common;

namespace BiBiHelp.Model
{
    public class User : Entity, IUser
    {
        public User() { }

        public User(string email, byte[] passwordHash, string passwordSalt, string name)
        {
            Email = email.ToLower();
            Password = passwordHash;
            PasswordSalt = passwordSalt;
            Name = name;
            Status = EEntityStatus.Active;
        }

        public EEntityStatus Status { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public double? TimeZone { get; set; }

        public byte[] Password { get; set; }

        public string PasswordSalt { get; set; }

        public DateTime LastLoginDate { get; set; }

        public DateTime UtcToUserDate(DateTime utcDate)
        {
            double timeOffset = 0;
            if (!TimeZone.HasValue)
                timeOffset = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time").BaseUtcOffset.Hours;
            else
                timeOffset = TimeZone.Value;
            return utcDate.AddHours(timeOffset);
        }
    }
}
