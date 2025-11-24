using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNestLib
{
    public static class TestDebug
    {
        public static void DumpPolygonInfo(NFP polygon, string outpPath)
        {
            List<string> lines = new List<string>();

            lines.Add("Id=" + polygon.id);
            lines.Add("Name=" + polygon.Name);
            lines.Add("isBin=" + polygon.isBin);
            lines.Add("x=" + polygon.x);
            lines.Add("y=" + polygon.y);
            lines.Add("WidthCalculated=" + polygon.WidthCalculated);
            lines.Add("HeightCalculated=" + polygon.HeightCalculated);
            if (polygon.children== null)
            {
                lines.Add("Children Count=" + 0);
            }

            else
            {
                lines.Add("Children Count=" + polygon.children.Count);
            }
           
            lines.Add("offsetx=" + polygon.offsetx);
            lines.Add("offsety=" + polygon.offsety);
            lines.Add("sourc=" + polygon.source);
            lines.Add("Rotation=" + polygon.rotation);
            lines.Add("polygon.ToString=" + polygon.ToString());

           
            StreamWriter sw = new StreamWriter(outpPath);

            foreach (string line in lines)
            {
                sw.WriteLine(line);
            }

            sw.Flush();
            sw.Close();
        }

    }
}
