using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.Logging
{
    /**
     * \Class   LogType
     * \brief   Used for creating text logs
     * \details Creates the types of logs for the different types which are
     *          for the buyer, planner, admin and the database
     */

    public class LogType
    {
        public LoggingType loggingType;

        public enum LoggingType
        {
            buyer,
            planner,
            admin,
            database
        }

        /// <summary>
        ///        Uses try/ catch block to catch any exceptions
        /// </summary>
        /// <param name="type"><b>LoggingType</b> - The type of logging that needs to be accessed</param>
        /// <param name="message"><b>string</b> - The error message that is to be sent in</param>
       public static void ErrorType(LoggingType type, string message)
       {
            message = DateTime.Now + " [Exception]: " + message;

            if (type == LoggingType.buyer)
            {
                string pathBuyer = "Buyer";
                string filepathBuyer = "Buyer\\Buyer_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

                FilePath(pathBuyer);
                RunFile(filepathBuyer, message);
            }
            else if (type == LoggingType.planner)
            {
                string pathPlanner = "Planner";
                string filepathPlanner = "Planner\\Planner_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

                FilePath(pathPlanner);
                RunFile(filepathPlanner, message);
            }
            else if (type == LoggingType.admin)
            {
                string pathAdmin = "Admin";
                string filepathAdmin = "Admin\\Admin_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

                FilePath(pathAdmin);
                RunFile(filepathAdmin, message);
            }
            else if (type == LoggingType.database)
            {
                string pathDatabase = "Database";
                string filepathDatabase = "Database\\Database_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

                FilePath(pathDatabase);
                RunFile(filepathDatabase, message);
            }
        }

        /// <summary>
        ///         Check to see if the filepath directory has been created
        /// </summary>
        /// <param name="directory"><b>string</b> - assigned file path for the log file</param>
        public static void FilePath(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        ///     Write to the file path 
        /// </summary>
        /// <param name="filepath"><b>string</b> - String that is the requred file path</param>
        /// <param name="message"><b>string</b> - The error message to be sent</param>
        public static void RunFile(string filepath, string message)
        {
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(message);
                    sw.Close();
                }               
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                    sw.Close();
                }
            }
        }

    }
}
