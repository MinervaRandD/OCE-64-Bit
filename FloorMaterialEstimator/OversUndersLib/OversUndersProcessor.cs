
namespace OversUndersLib
{
    using OversUnders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class OversUndersMainProcessor
    {
        private List<double> oversWidths ;
        private List<double> undrsWidths ;
        private List<double> oversLngths ;
        private List<double> undrsLngths ;

        public double rollWidthInInches;

        public OversUndersMainProcessor(
            List<MaterialArea> oversList
            ,List<MaterialArea> undrsList
            ,int rollWidthInInches
            )
        {
            oversWidths = new List<double>();
            undrsWidths = new List<double>();
            oversLngths = new List<double>();
            undrsLngths = new List<double>();

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

            this.rollWidthInInches = rollWidthInInches;
        }

        public void GetOUsOutput(out double totlFillLength, out double wasteFactor, out double netUnders)
        {
            try
            {
                OversUndersOutputGenerator oversUndersOutputGenerator
                    = new OversUndersOutputGenerator(oversWidths, oversLngths, undrsWidths, undrsLngths, rollWidthInInches);

                oversUndersOutputGenerator.GetOUsOutput(out totlFillLength, out wasteFactor, out netUnders);

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
