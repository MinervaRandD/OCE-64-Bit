using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphics;
using FinishesLib;

namespace CanvasLib.Legend
{
    public partial class UCLegend : UserControl
    {
        private GraphicsPage page;

        private GraphicsWindow window;

        private AreaFinishBaseList areaFinishBaseList;

        public UCLegend()
        {
            InitializeComponent();
        }

        public void Init(
            GraphicsWindow window,
            GraphicsPage page,
            AreaFinishBaseList areaFinishBaseList
            )
        {
            this.page = page;
            this.window = window;
            this.areaFinishBaseList = areaFinishBaseList;

            //areaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            //areaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            //areaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;
            //areaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;

            //VisioInterop.SetLayerVisibility(LegendLayer, false);

        }
    }
}
