
namespace Utilities
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CustomToolbarButtonRenderer: ToolStripProfessionalRenderer
    {
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item is ToolStripButton)
            {
                ToolStripButton toolStripButton = (ToolStripButton)e.Item;

                Rectangle rectangle = new Rectangle(0, 0, toolStripButton.Width, toolStripButton.Height);

                if (toolStripButton.Checked)
                {
                   e.Graphics.FillRectangle(Brushes.Gold, rectangle);
                }

                else
                {
                    Brush brush = new SolidBrush(Color.FromArgb(224, 224, 224));

                    e.Graphics.FillRectangle(brush, rectangle);
                }
                
            }

            //OnRenderButtonBackground(e);
        }
    }
}
