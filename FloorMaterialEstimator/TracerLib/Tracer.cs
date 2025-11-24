#define TRACEMETHODCALL

namespace TracerLib
{

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Utilities;


    public class Tracer
    {
        public static Tracer TraceGen { get; set; } = null;

        private TextWriterTraceListener DefaultTraceListener = null;

        public TraceLevel TraceLevel { get; set; } = TraceLevel.None;

        private string logFileName = string.Empty;

        public Tracer(TraceLevel traceLevel, string logFileName, bool resetTraceFile = false)
        {
            this.TraceLevel = traceLevel;

            this.logFileName = logFileName;

            Trace.Listeners.RemoveAt(0);

            DefaultTraceListener = new TextWriterTraceListener(logFileName);

            Trace.Listeners.Add(DefaultTraceListener);

            Trace.AutoFlush = true;

            //Trace.IndentSize = indentSize;

            if (resetTraceFile)
            {
                if (File.Exists(logFileName))
                {
                    FileStream fileStream = File.Open(logFileName, FileMode.Open);
                    fileStream.SetLength(0);
                    fileStream.Close();
                }
            }

            else
            {
                truncateLogFile(2097152, 1048576);
            }
        }

        public void TraceLog(TraceLevel traceLevel, string msg, int indent = 0)
        {
            if ((traceLevel & this.TraceLevel) == 0)
            {
                return;
            }

            truncateLogFile(2097152, 1048576);

            Trace.WriteLine("\n-------------------------------------------------------------\n");

            msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f") + string.Concat(Enumerable.Repeat("\t", Math.Max(1, indent + 1))) + msg;

            Trace.WriteLine(msg);
        }

        public void TraceLog(string separator, string msg, int indent = 0, bool writeCallStack = false)
        {
            truncateLogFile(2097152, 1048576);

            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f");

            string tabList = string.Concat(Enumerable.Repeat("\t", Math.Max(1, indent)));

            Trace.WriteLine(separator);

            string[] msgList = msg.Split(new char[] { '\n' });

            foreach (string msg1 in msgList)
            {
                Trace.WriteLine(dateTime + tabList + msg1);
            }

            if (!writeCallStack)
            {
                return;
            }

            string[] callStack = Environment.StackTrace.Split(new char[] { '\n' });

            Trace.WriteLine("\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f") + string.Concat(Enumerable.Repeat("\t", Math.Max(1, indent))) + "Call Stack:\n");

            foreach (string callStackLine in callStack)
            {
                msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f") + string.Concat(Enumerable.Repeat("\t", Math.Max(1, indent + 1))) + callStackLine.Trim();

                Trace.WriteLine(msg);
            }
        }

        public void TraceInfo(string msg, int indent = 0, bool writeCallStack = false)
        {
            if ((TraceLevel & TraceLevel.Info) == 0)
            {
                return;
            }

            TraceLog(
                "\n----------------------- (Info) -----------------------------------\n"
                , msg
                , indent
                , writeCallStack);

        }

        public void TraceError(string msg, int indent = 0, bool writeCallStack = true)
        {
            if ((TraceLevel & TraceLevel.Error) == 0)
            {
                return;
            }

            TraceLog(
                "\n----------------------- (Error) ----------------------------------\n"
                , msg
                , indent
                , writeCallStack);

            if (ErrorReported != null)
            {
                ErrorReported.Invoke();
            }
        }

        public void TraceException(string msg, Exception ex, int indent = 0, bool writeCallStack = true)
        {
            if ((TraceLevel & TraceLevel.Exception) == 0)
            {
                return;
            }

            string exMsg = string.Empty;

            Exception ex1 = ex;

            do
            {
                exMsg += ex.Message + '\n';

                ex1 = ex1.InnerException;
            }
            while (ex1 != null);

            msg += "\n" + exMsg + "\n";

            msg += "Exception Call Stack:\n";

            msg += ex.StackTrace + "\n";

            TraceLog(
                "\n----------------------- (Exception) ----------------------------------\n"
                , msg
                , indent
                , writeCallStack);

            if (ErrorReported != null)
            {
                ErrorReported.Invoke();
            }
        }

        public void TraceMethodCall(int indent = 0, bool writeCallStack = false, params object[] callingMethodParamValues) =>
#if DEBUG && TRACEMETHODCALL
            TraceMethodCallDebug(indent, writeCallStack, callingMethodParamValues);
#else
            TraceMethodCallDistribution();
#endif

        private void TraceMethodCallDebug(int indent = 0, bool writeCallStack = false, params object[] callingMethodParamValues)
        {
            if ((TraceLevel & TraceLevel.MethodCall) == 0)
            {
                return;
            }

            string msg = TraceMethodCallMessage(callingMethodParamValues);


            TraceLog("\n"
                , "***Method call: " + msg
                , indent
                , writeCallStack);
        }

        private void TraceMethodCallDistribution() { }

        public string TraceMethodCallMessage(params object[] callingMethodParamValues)
        {
            var method = new StackFrame(skipFrames: 3).GetMethod();
            var methodParams = method.GetParameters();
            var methodCalledBy = new StackFrame(skipFrames: 3).GetMethod();

            var methodCaller = "";
            if (methodCalledBy != null)
            {
                methodCaller = $"{methodCalledBy.DeclaringType.Name}.{methodCalledBy.Name}()";
            }

            if (methodParams.Length == callingMethodParamValues.Length)
            {
                List<string> paramList = new List<string>();
                foreach (var param in methodParams)
                {
                    paramList.Add($"{param.Name}={callingMethodParamValues[param.Position]}");
                }

                return LogMethodCallString(method.Name, string.Join(", ", paramList), methodCaller);

            }
            else
            {
                return LogMethodCallString(method.Name, "/* Please update to pass in all parameters */", methodCaller);
            }
        }

        private string LogMethodCallString(string methodName, string parameterList, string methodCaller)
        {
            return $"{methodCaller} -> {methodName}({parameterList})";
        }

        private void truncateLogFile(int truncateFrom, int truncateTo)
        {
            if (!File.Exists(logFileName))
            {
                return;
            }

            long fileSize = new FileInfo(logFileName).Length;

            if (fileSize < truncateFrom)
            {
                return;
            }

            Utilities.TruncateFileFromEnd(logFileName, truncateTo); // 2 MB largest file
        }

        public event ErrorReportHandler ErrorReported;

        public delegate void ErrorReportHandler();

        public event ExceptionReportHandler ExceptionReported;

        public delegate void ExceptionReportHandler(Exception ex);
    }

}
