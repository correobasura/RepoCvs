using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados.UtilGeneral
{
    public class CustomLogger
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Log(string component, string message)
        {
            _log.Info(message);
        }
    }
}
