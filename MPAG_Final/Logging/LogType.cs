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

        /// \brief Uses try/ catch block to catch any exception
        /// 
        /// \details This method enters in the logging type and message to append to a text file
        /// <param name="type"><b>LoggingType</b> - The type of logging that needs to be accessed</param>
        /// <param name="message"><b>string</b> - The error message that is to be sent in</param>
        /// \return none      
        public static void ErrorType(LoggingType type, string message)
       {
            message = DateTime.Now + " [Exception]: " + message;

            string path = "Logs";
            FilePath(path);

            if (type == LoggingType.buyer)
            {
                string filepathBuyer = "Logs\\Buyer_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

                RunFile(filepathBuyer, message);
            }
            else if (type == LoggingType.planner)
            {
                string filepathPlanner = "Logs\\Planner_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
                RunFile(filepathPlanner, message);
            }
            else if (type == LoggingType.admin)
            {
                string filepathAdmin = "Logs\\Admin_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
                RunFile(filepathAdmin, message);
            }
            else if (type == LoggingType.database)
            {
                string filepathDatabase = "Logs\\Database_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
                RunFile(filepathDatabase, message);
            }
       }

        /// \brief Demonstrates the functionalities that have been successfully completed
        /// 
        /// \details This method enters in the logging type and message to append to a text file
        /// <param name="message"><b>string</b> - The success message to be written into </param>
        /// \return none
        public static void Functionalities(string message)
        {
            message = DateTime.Now + " [Operation Success]: " + message;

            string path = "Logs";
            FilePath(path);

            string filepathOperations = "Logs\\Operation_"  + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            RunFile(filepathOperations, message);

        }

        /// \brief Creates filepath is non-existant 
        /// 
        /// \details This method creates a directory to be used
        /// <param name="directory"><b>string</b> - Directory to be created</param>
        /// \return none
        public static void FilePath(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// \brief Write to file
        /// 
        /// \details Check to see if there is the file existing and then appends to the existing file
        /// <param name="filepath"><b>string</b> - The type of logging that needs to be accessed</param>
        /// <param name="message"><b>string</b> - The error message that is to be sent in</param>
        /// \return none
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
