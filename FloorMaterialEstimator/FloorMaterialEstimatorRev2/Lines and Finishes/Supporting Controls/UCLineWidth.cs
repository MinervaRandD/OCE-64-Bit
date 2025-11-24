using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator.Finish_Controls
{
    public class LineWidthSelectedEventArgs : EventArgs
    {
        public double WidthInPts;
    }

    public delegate void LineWidthSelectedEvent(LineWidthSelectedEventArgs args);

    public partial class UCLineWidth : UserControl
    {
        public event LineWidthSelectedEvent LineWidthSelected;

        Button[] buttonList = new Button[14];

        string[] buttonText = new string[14]
        {
            "0.25 pt"
            , "0.5 pt"
            , "1 pt"
            , "1.5 pt"
            , "2 pt"
            , "2.5 pt"
            , "3 pt"
            , "4 pt"
            , "5 pt"
            , "6 pt"
            , "7 pt"
            , "8 pt"
            , "9 pt"
            , "10 pt"

        };

        double[] lineWidth = new double[14]
        {
            0.25, 0.5, 1, 1.5, 2, 2.5, 3, 4, 5, 6, 7, 8, 9, 10
        };

        public UCLineWidth()
        {
            InitializeComponent();

            this.BorderStyle = BorderStyle.FixedSingle;
            this.HorizontalScroll.Enabled = false;
            this.HorizontalScroll.Visible = false;
            this.HorizontalScroll.Maximum = 0;
            this.AutoScroll= true;
           // this.VerticalScroll= true;
            
        }

        public void Init()
        {
            int bttnLocnX = 2;
            int bttnLocnY = 2;

            for (int i = 0; i < 14; i++)
            {
                Button button = new Button();

                this.Controls.Add(button);

                button.Size = new Size(this.Width - 16, 23);

                button.Location = new Point(bttnLocnX, bttnLocnY);

                button.Tag = i;

                button.Text = buttonText[i];

                button.FlatStyle = FlatStyle.Flat;

                buttonList[i] = button;

                button.Click += Button_Click;
                bttnLocnY += button.Height + 4;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (LineWidthSelected == null)
            {
                return;
            }

            Button button = (Button)sender;

            int index = (int)button.Tag;

            LineWidthSelectedEventArgs args = new LineWidthSelectedEventArgs()
            {
                WidthInPts = lineWidth[index]
            };

            SetSelectedLineWidthIndex(index);

            if (LineWidthSelected != null)
            {
                LineWidthSelected.Invoke(args);
            }
        }

        public void SetSelectedLineWidthIndex(int index)
        {
            foreach (Button button in buttonList)
            {
                if ((int)button.Tag == index)
                {
                    button.FlatAppearance.BorderColor = Color.Black;
                    button.FlatAppearance.BorderSize = 2;
                }

                else
                {
                    button.FlatAppearance.BorderColor = Color.White;
                    button.FlatAppearance.BorderSize = 2;
                }
            }
        }

        internal void SetSelectedLineWidth(double seamWidthInPts)
        {
            for (int i = 0; i < lineWidth.Length; i++)
            {
                if (lineWidth[i] == seamWidthInPts)
                {
                    SetSelectedLineWidthIndex(i);
                    return;
                }

                if (lineWidth[i] > seamWidthInPts)
                {
                    return;
                }
            }
        }
    }
}
