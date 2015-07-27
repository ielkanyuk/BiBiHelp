using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiBiHelp.Model.Interfaces.Common;

namespace BiBiHelp.Model.Interfaces
{
    public interface IMessage : IEntity
    {
        IUser Author { get; }

        IMessage Parent { get; }

        string Text { get; }

        bool isAnonymous { get; }

        int Region { get; }
    }
}
