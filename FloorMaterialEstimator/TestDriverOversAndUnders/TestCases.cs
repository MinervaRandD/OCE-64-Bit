using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDriverOversAndUnders
{
    public static class TestCases
    {
        public static List<Tuple<int, int>> Overs1 = new List<Tuple<int, int>>()
        {
            new Tuple<int,int>(48, 480)
        };

        public static List<Tuple<int, int>> Undrs1 = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(15,480),
            new Tuple<int, int>(21,480),
            new Tuple<int, int>(24,480),
            new Tuple<int, int>(27,480),
            new Tuple<int, int>(30,480),
            new Tuple<int, int>(33,480),
            new Tuple<int, int>(42,480),
            new Tuple<int, int>(45,480)
        };


        public static List<Tuple<int, int>> Overs2 = new List<Tuple<int, int>>()
        {
            new Tuple<int,int>(18, 396),
            new Tuple<int,int>(30, 48),
            new Tuple<int,int>(36, 360),
            new Tuple<int,int>(51, 384),
            new Tuple<int,int>(69, 696)
        };

        public static List<Tuple<int, int>> Undrs2 = new List<Tuple<int, int>>()
        {
            new Tuple<int,int>(27,   828 ),
            new Tuple<int,int>(30,   780 ),
            new Tuple<int,int>(33,   912 ),
            new Tuple<int,int>(45,   96  ),
            new Tuple<int,int>(51,   492 ),
            new Tuple<int,int>(63,   1200    ) ,
            new Tuple<int,int>(69,   360 ),
            new Tuple<int,int>(72,   564 )

        };

        public static List<Tuple<int, int>> Overs3 = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(48,480),
            new Tuple<int, int>(54,600),
            new Tuple<int, int>(60,480),
            new Tuple<int, int>(66,600),
            new Tuple<int, int>(78,480)
        };

        public static List<Tuple<int, int>> Undrs3 = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(36,960),
            new Tuple<int, int>(48,840),
            new Tuple<int, int>(60,720),
            new Tuple<int, int>(72,600),
            new Tuple<int, int>(84,480),
            new Tuple<int, int>(96,360),
            new Tuple<int, int>(108,240),
            new Tuple<int, int>(120,120)
        };
    }
}
