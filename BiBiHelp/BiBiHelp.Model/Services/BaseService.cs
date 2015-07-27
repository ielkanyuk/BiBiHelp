using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiBiHelp.Model.Services
{
    public abstract class BaseService
    {
        protected BaseService(Services services)
        {
            Services = services;
        }

        protected Services Services { get; private set; }
    }
}
