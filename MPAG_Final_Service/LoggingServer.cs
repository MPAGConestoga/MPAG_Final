using MPAG_Final_Service.ServiceHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final_Service
{
    class LoggingServer
    {
        private mpagLoggingFileType log;
        public LoggingServer(mpagLoggingFileType messasgeLog)
        {
            this.log = messasgeLog;
        }
    }
}
