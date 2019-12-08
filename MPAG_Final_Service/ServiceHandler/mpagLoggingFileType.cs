using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final_Service.ServiceHandler
{
    public partial class mpagLoggingFileType 
    {
        private string MessengerFileName;

        public mpagLoggingFileType(string filename)
        {
            MessengerFileName = filename;
        }

        public void addMessageToFile(string message)
        {
            try
            {
                //Insert into the file that should be accessed
            }
            catch(Exception ex)
            {

            }
        }
    }
}
