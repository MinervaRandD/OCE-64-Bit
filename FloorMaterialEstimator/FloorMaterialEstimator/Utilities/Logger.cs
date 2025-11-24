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

namespace FloorMaterialEstimator.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains all logic related to program logging.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="callingMethod">The method calling the error routine</param>
        /// <param name="callingFilePath">The file path in which the calling method exists</param>
        /// <param name="callingFileLineNumber">The line number in the file from which the call was made</param>
        public static void LogError(
            string message,
            [CallerMemberName] string callingMethod = null,
            [CallerFilePath] string callingFilePath = null,
            [CallerLineNumber] int callingFileLineNumber = 0)
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

            string errorLogFilePath = string.Empty;

            Program.AppConfig.TryGetValue("errorlogfilepath", out errorLogFilePath);

            if (string.IsNullOrEmpty(errorLogFilePath))
            {
                throw new Exception("Attempt to log an error with no error log file path specified.");
            }

            try
            {
                StreamWriter sw = new StreamWriter(errorLogFilePath, true);

                sw.WriteLine("");
                sw.WriteLine("Error Message: File=\"" + callingFilePath + "\", Method=\"" + callingMethod + "\", Line Number=" + callingFileNumberStr);
                sw.WriteLine("");
                sw.WriteLine("Message: " + message);

                sw.Flush();
                sw.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("Attempt to write error log file failed: " + ex.Message, ex);
            }
        }
    }
}
