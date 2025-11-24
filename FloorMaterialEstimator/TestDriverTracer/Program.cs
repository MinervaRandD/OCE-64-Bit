using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TracerLib;

namespace TestDriverTracer
{
    class Program
    {
        static Tracer tracer;
        
        static void Main(string[] args)
        {
            tracer = new Tracer(
                TracerLib.TraceLevel.Info | TracerLib.TraceLevel.Error | TracerLib.TraceLevel.Exception | TracerLib.TraceLevel.MethodCall
                , @"C:\Temp\temp.log.txt"
                , true);

            TestTraceException();
            
        }

        static void TestTraceException()
        {
            try
            {
                TestTraceException1();
            }

            catch (Exception ex)
            {
                tracer.TraceException("Test exception", ex, 1, true);
            }
        }

        static void TestTraceException1()
        {
            TestTraceException2();
        }

        static void TestTraceException2()
        {
            throw new NotImplementedException();
        }
        static void TestTraceMethodCall()
        {
            TestTraceMethodCall1(1);
            TestTraceMethodCall2(1, 2);
            TestTraceMethodCall3("abc", "def");
        }

        private static void TestTraceMethodCall1(int v)
        {
            tracer.TraceMethodCall(1, true, new object[] { v });
        }

        private static void TestTraceMethodCall2(int v1, int v2)
        {
            tracer.TraceMethodCall(1, true, new object[] { v1, v2 });
        }

        private static void TestTraceMethodCall3(string v1, string v2)
        {
            tracer.TraceMethodCall(1, true, new object[] { v1, v2 });
        }

    }
}
