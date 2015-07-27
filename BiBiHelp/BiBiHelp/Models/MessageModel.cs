using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BiBiHelp.Model.Interfaces;

namespace BiBiHelp.Models
{
    public class MessageModel
    {
        public MessageModel()
        {
        }

        public MessageModel(IMessage message, IUser currentUser)
        {
            Id = message.Id;
            Author = message.Author.Name;
            Text = message.Text;
            _dateCreated =  /*currentUser.UtcToUserDate(*/message.DateCreated/*)*/;
        }

        public Guid Id { get; set; }

        public string Author { get; set; }

        public string Text { get; set; }

        private DateTime _dateCreated { get; set; }
        public string DateCreated
        {
            get {
                if (_dateCreated.Date == DateTime.UtcNow.Date)
                    return _dateCreated.ToString("сегодня в HH:mm");

                if (_dateCreated.Date == DateTime.UtcNow.AddDays(-1).Date)
                    return _dateCreated.ToString("вчера в HH:mm");

                if (_dateCreated.Year != DateTime.UtcNow.Year)
                    return _dateCreated.ToString("d MMMM, yyyy в HH:mm");
                
                return _dateCreated.ToString("d MMMM в HH:mm"); 
            }
        }
    }
}