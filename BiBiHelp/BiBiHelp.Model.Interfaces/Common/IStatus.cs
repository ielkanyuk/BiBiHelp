using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiBiHelp.Model.Interfaces.Common
{
    public interface IStatus
    {
        /// <summary>
        /// Значение статуса
        /// </summary>
        EEntityStatus Status { get; }
    }
}
