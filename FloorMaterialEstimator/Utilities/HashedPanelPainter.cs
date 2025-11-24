using System.Drawing;
using System.Windows.Forms;

namespace Utilities
{
    public static class HashedPanelPainter
    {
        public static void PaintHorizontalHashedPanel(Panel panel, int nLines, Color color,  PaintEventArgs e)
        {
            // Draw horizontal line pattern

            double height = panel.Height;
            double yIncmnt = height / ((double)nLines + 1.0);

            double yOffset = yIncmnt / 2.0;

            // Set the SmoothingMode property to smooth the line.
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen;

            pen = new Pen(color);

            // Set the width
            pen.Width = (float)2F; // this.FinishSeamLine.LineWeight;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            double sizeX = panel.Width;

            for (int i = 0; i < nLines; i++)
            {
                e.Graphics.DrawLine(pen, 0, (float)yOffset, (float)sizeX, (float)yOffset);

                yOffset += yIncmnt;
            }

            // Dispose of the custom pen.
            pen.Dispose();
        }

        public static void PaintVerticalHashedPanel(Panel panel, int nLines, Color color, PaintEventArgs e)
        {

            // Draw vertical line pattern

            double width = panel.Width;
            double xIncmnt = width / ((double) nLines + 1.0);

            double xOffset = xIncmnt / 2.0;

            // Set the SmoothingMode property to smooth the line.
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen;

            pen = new Pen(color);

            // Set the width
            pen.Width = (float)2F; // this.FinishSeamLine.LineWeight;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            double sizeY = panel.Height;

            for (int i = 0; i < nLines; i++)
            {
                e.Graphics.DrawLine(pen, (float)xOffset, 0, (float)xOffset, (float)sizeY);

                xOffset += xIncmnt;
            }

            // Dispose of the custom pen.
            pen.Dispose();
        }

        public static void PaintCrossHashedPanel(Panel panel, int nLinesHorizontal, int nLinesVertical, Color color, PaintEventArgs e)
        {
            PaintHorizontalHashedPanel(panel, nLinesHorizontal, color, e);
            PaintVerticalHashedPanel(panel, nLinesVertical, color, e);
        }
    }
}
