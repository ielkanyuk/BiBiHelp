using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiBiHelp.Model.Interfaces;

namespace BiBiHelp.Model.Entities
{
    public class Message : Entity, IMessage
    {
        public Message()
        {
            
        }

        public Message(string text, bool anonymous, int region, User author, Message parent = null)
        {
            Author = author;
            Parent = parent;
            Text = text;
            isAnonymous = anonymous;
            Region = region;
        }

        public User Author { get; set; }
        IUser IMessage.Author { get { return Author; } }
        
        public Message Parent { get; set; }
        IMessage IMessage.Parent { get { return Parent; } }

        public string Text { get; set; }

        public bool isAnonymous { get; set; }

        public int Region { get; set; }
    }
}
