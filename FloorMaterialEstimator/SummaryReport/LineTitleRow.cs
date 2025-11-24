

namespace SummaryReport
{
    using System;
    using System.Windows.Forms;
    using FinishesLib;

    using System.Drawing;

    public partial class LineTitleRow : UserControl
    {
      
        int ckbFinishFilterSizeX => RowColumnWidths.CkbFinishFilterSizeX;
        int pnlColorSizeX => RowColumnWidths.PnlColorSizeX;
        int valueLFSizeX => RowColumnWidths.ValueSizeX;
        int valueSFSizeX => RowColumnWidths.ValueSizeX;
        int unitsSizeX => RowColumnWidths.UnitsSizeX;
        int tagSizeX => RowColumnWidths.TagSizeX;
        int typeSizeX = RowColumnWidths.TypeSizeX;
        int valueSmallSizeX = RowColumnWidths.ValueSmallSizeX;

        int totlSizeX = RowColumnWidths.TotlSizeX;

        public LineTitleRow()
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;
            this.Anchor = AnchorStyles.None;
            this.AutoSize = false;

            initializeControls();
        }

        private void initializeControls()
        {
            // Set up element sizes

            int cntlSizeY = 23;

            int locX = 1;
            int locY = 1;

            // Check box

            locX += ckbFinishFilterSizeX;

            // Color panel

            locX += pnlColorSizeX;

            // Total Linear Feet

            this.lblTotalLF.Size = new Size(valueLFSizeX, cntlSizeY);
            this.lblTotalLF.Location = new Point(locX, locY);

            locX += valueLFSizeX;

            // Total Linear Feet

            this.lblTotalSF.Size = new Size(valueLFSizeX, cntlSizeY);
            this.lblTotalSF.Location = new Point(locX, locY);

            locX += valueSFSizeX;

            // Tag

             this.lblTag.Size = new Size(tagSizeX, cntlSizeY);
            this.lblTag.Location = new Point(locX, locY);

            locX += tagSizeX;

            // Type

            this.lblType.Size = new Size(typeSizeX, cntlSizeY);
            this.lblType.Location = new Point(locX, locY);

            locX += typeSizeX;

            locX += valueSmallSizeX + unitsSizeX;

            // L-Repeats

            this.Size = new Size(totlSizeX, cntlSizeY);
        }

    }
}
