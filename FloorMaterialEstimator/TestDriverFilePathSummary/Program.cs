using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDriverFilePathSummary
{
    using Utilities;

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter a file path: ");
                string filePath = Console.ReadLine();
                if (filePath == "Exit")
                {
                    break;
                }
                Console.WriteLine("Path summary: \"" + Utilities.FilePathSummary(filePath, 3, 1) + "\"\n");
            }
        }
    }
}
