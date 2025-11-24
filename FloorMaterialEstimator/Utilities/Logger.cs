//-------------------------------------------------------------------------------//
// <copyright file="Logger.cs" company="Bruun Estimating, LLC">                  // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Contains all logic related to program logging.
    /// </summary>
    public static class Logger
    {
        private static string messageLogFilePath = string.Empty;
        public static void Intialize(string messageLogFilePath)
        {
            Logger.messageLogFilePath = messageLogFilePath;
        }


#if false
        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="callingMethod">The method calling the error routine</param>
        /// <param name="callingFilePath">The file path in which the calling method exists</param>
        /// <param name="callingFileLineNumber">The line number in the file from which the call was made</param>
        public static void LogError(
            string message
            ,MessageSeparator messageSeparator = MessageSeparator.None
            ,[CallerMemberName] string callingMethod = null
            ,[CallerFilePath] string callingFilePath = null
            ,[CallerLineNumber] int callingFileLineNumber = 0)
        {
            if (string.IsNullOrEmpty(callingMethod))
            {
                callingMethod = "<unknown>";
            }

            if (string.IsNullOrEmpty(callingFilePath))
            {
                callingFilePath = "<unknown>";
            }

            else
            {
                callingFilePath = Path.GetFileName(callingFilePath);
            }

            string callingFileNumberStr = "<unknown>";

            if (callingFileLineNumber != 0)
            {
                callingFileNumberStr = callingFileLineNumber.ToString();
            }

            //if (string.IsNullOrEmpty(Logger.errorLogFilePath))
            //{
            //    MessageBox.Show("Attempt to log error failed: the error log file path is not specified");
            //    return;
            //}

            string logDateTime = DateTime.Now.ToString("yyyy-MM-dd:hh:mm:ss");

            //try
            //{
            //    StreamWriter sw = new StreamWriter(Logger.errorLogFilePath, true);

            //    sw.WriteLine("");
            //    sw.WriteLine("Error Message (" + logDateTime + "): File=\"" + callingFilePath + "\", Method=\"" + callingMethod + "\", Line Number=" + callingFileNumberStr);
            //    sw.WriteLine("");
            //    sw.WriteLine("Message: " + message);

            //    sw.WriteLine("\n____________________________________________________________________\n");
            //    sw.Flush();
            //    sw.Close();
            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show("Attempt to log error threw an exception:\n" + ex.Message);
            //    return;
            //}
        }
#endif

        public static void LogMessage(string message, MessageSeparator messageSeparator = MessageSeparator.None)
        {
            if (string.IsNullOrEmpty(Logger.messageLogFilePath))
            {
                MessageBox.Show("Attempt to log message failed: the message log file path is not specified");
                return;
            }

            string logDateTime = DateTime.Now.ToString("yyyy-MM-dd:hh:mm:ss");

            try
            {
                StreamWriter sw = new StreamWriter(Logger.messageLogFilePath, true);

                sw.WriteLine("");
                sw.WriteLine("Log Message (" + logDateTime + "): " + message);

                if (messageSeparator == MessageSeparator.SingleLine)
                {
                    sw.WriteLine("\n____________________________________________________________________\n");
                }

                else if (messageSeparator == MessageSeparator.DoubleLine)
                {
                    sw.WriteLine("\n====================================================================\n");
                }

                sw.Flush();
                sw.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Attempt to log message threw an exception:\n" + ex.Message);
                return;
            }
        }
    }

    public enum MessageSeparator
    {
        None = 0
        ,SingleLine = 1
        ,DoubleLine = 2
    }
}
