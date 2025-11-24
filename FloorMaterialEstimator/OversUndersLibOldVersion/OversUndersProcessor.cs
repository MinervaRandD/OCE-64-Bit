
namespace OversUndersLibOldVersion
{
    using OversUnders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public static class OversUndersProcessorOldVersion
    {
        public static void GetOUsOutput(List<MaterialArea> oversList, List<MaterialArea> undrsList, int rollWidthInInches, out double totlFillLength, out double wasteFactor, out double netUnders)
        {
            try
            {

                List<double> oversWidths = new List<double>();
                List<double> undrsWidths = new List<double>();
                List<double> oversLngths = new List<double>();
                List<double> undrsLngths = new List<double>();

                for (int i = 0; i < oversList.Count; i++)
                {
                    oversWidths.Add(oversList[i].WidthInInches / 12.0);
                    oversLngths.Add(oversList[i].LngthInInches / 12.0);
                }

                for (int i = 0; i < undrsList.Count; i++)
                {
                    undrsWidths.Add(undrsList[i].WidthInInches / 12.0);
                    undrsLngths.Add(undrsList[i].LngthInInches / 12.0);
                }

                OversUndersLibOldVersion.Driver.GetOUsOutput(oversWidths, oversLngths, undrsWidths, undrsLngths, (double)rollWidthInInches, out totlFillLength, out wasteFactor, out netUnders);

            }

            catch (Exception ex)
            {
                MessageBox.Show("GetOUsOutput threw an exception: " + ex.Message);

                totlFillLength = 0;
                wasteFactor = 0;
                netUnders = 0;

                return;
            }
        }
    }
}
