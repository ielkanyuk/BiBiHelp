using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiBiHelp.Model.Interfaces.Services;

namespace BiBiHelp.Model.Interfaces.App
{
    public interface IAppController
    {
        IServiceManager ServiceManager { get; }
    }
}
