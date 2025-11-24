using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FloorMaterialEstimator.Finish_Controls
{
    class Program
    {
        private static FinishSeamBaseList finishSeamLineList = new FinishSeamBaseList();

        static void Main(string[] args)
        {

            FinishSeamBase finishSeamLine1 = new FinishSeamBase()
            {
                SeamName = "Seam-1",
                VisioDashType = 1, LineWidthInPts = 2, A = 255, R = 255, G = 0, B = 0
            };

            FinishSeamBase finishSeamLine2 = new FinishSeamBase()
            {
                SeamName = "Seam-2",
                VisioDashType = 2,
                LineWidthInPts = 2, A = 255, R = 0, G = 255, B = 0
            };

            FinishSeamBase finishSeamLine3 = new FinishSeamBase()
            {
                SeamName = "Seam-3",
                VisioDashType = 4, LineWidthInPts = 2, A = 255, R = 0, G = 0,B = 255
            };

            FinishSeamBase finishSeamLine4 = new FinishSeamBase()
            {
                SeamName = "Seam-4",
                VisioDashType = 10, LineWidthInPts = 2,A = 255, R = 128, G = 128,B = 0
            };

            FinishSeamBase finishSeamLine5 = new FinishSeamBase()
            {
                SeamName = "Seam-5",
                VisioDashType = 14, LineWidthInPts = 2, A = 255, R = 128, G = 0, B = 128
            };

            FinishSeamBase finishSeamLine6 = new FinishSeamBase()
            {
                SeamName = "Seam-6",
                VisioDashType = 16, LineWidthInPts = 2, A = 255, R = 0, G = 128, B = 128
            };

            FinishSeamBase finishSeamLine7 = new FinishSeamBase()
            {
                SeamName = "Seam-7",
                VisioDashType = 23, LineWidthInPts = 2, A = 255, R = 0,G  = 0, B = 0
            };

            finishSeamLineList.SeamList = new List<FinishSeamBase>()
            {
                finishSeamLine1,
                finishSeamLine2,
                finishSeamLine3,
                finishSeamLine4,
                finishSeamLine5,
                finishSeamLine6,
                finishSeamLine7
            };

            var serializer = new XmlSerializer(typeof(FinishSeamBaseList));

            string outpFilePath =
                @"C:\Minerva Research and Development\Projects\OCERev2\FloorMaterialEstimator\FloorMaterialEstimatorRev2\Data\Defaults\FinishSeamLineInit.xml";

            StreamWriter sw = new StreamWriter(outpFilePath);

            serializer.Serialize(sw, finishSeamLineList);
        }
    }
}
