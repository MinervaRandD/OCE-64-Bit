using System;
using Geometry;

namespace TestDriverPointClosestToLine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Coordinate coord1 = TestCases.line1.GetNearestPointOnLineToCoord(new Coordinate(-1,1));
            //Coordinate coord2 = TestCases.line1.GetNearestPointOnLineToCoord(new Coordinate(2, 1));
            //Coordinate coord3 = TestCases.line1.GetNearestPointOnLineToCoord(new Coordinate(0.5, 1));

            //Coordinate coord4 = TestCases.line2.GetNearestPointOnLineToCoord(new Coordinate(-1, -1));
            //Coordinate coord5 = TestCases.line2.GetNearestPointOnLineToCoord(new Coordinate(1, 2));
            //Coordinate coord6 = TestCases.line2.GetNearestPointOnLineToCoord(new Coordinate(0.5, 0.5));

            Coordinate coord7 = TestCases.line3.GetNearestPointOnLineToCoord(new Coordinate(-1, -1));
            Coordinate coord8 = TestCases.line3.GetNearestPointOnLineToCoord(new Coordinate(3, 2));
            Coordinate coord9 = TestCases.line3.GetNearestPointOnLineToCoord(new Coordinate(2, 0));
            Coordinate coord10 = TestCases.line3.GetNearestPointOnLineToCoord(new Coordinate(3, 0));
            Coordinate coord11 = TestCases.line3.GetNearestPointOnLineToCoord(new Coordinate(3, -0.5));
        }
    }
}
