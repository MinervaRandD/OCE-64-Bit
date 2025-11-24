

namespace FloorMaterialEstimator
{

    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.CanvasManager;
    using Graphics;
    using MaterialsLayout;

    public static class TestAndDebug
    {
        public static string validateCanvasLayoutArea(CanvasLayoutArea layoutArea)
        {
            //foreach (CanvasCut cut in layoutArea.GraphicsCutList)
            //{
            //    if (cut.ParentGraphicsRollout == null)
            //    {
            //        return "Invalid null cut rollout";
            //    }
            //}

            return string.Empty;
        }

        internal static void DumpPatternLines(List<GraphicsDirectedLine> graphicsDirectedLines)
        {
            if (File.Exists(@"C:\Temp\PatternLines.txt"))
            {
                File.Delete(@"C:\Temp\PatternLines.txt");
            }

            StreamWriter sw = new StreamWriter(@"C:\Temp\PatternLines.txt");

            foreach (var line in graphicsDirectedLines)
            {
                string outpLine = line.Coord1.ToString() + "\t" + line.Coord2.ToString();
                sw.WriteLine(outpLine);
            }

            sw.Flush();

            sw.Close();
        }
    }
}
