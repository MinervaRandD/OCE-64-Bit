

namespace Utilities.User_Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class ColorSelectedEventArgs : EventArgs
    {
        public int A;
        public int R;
        public int G;
        public int B;
    }

    public partial class UCColorPalette : UserControl
    {

        private argb[] paletteColors = new argb[]
        {
            
            #region Palette Colors

            new argb(255, 255, 255, 192),
            new argb(255, 255, 192, 192),
            new argb(255, 192, 255, 192),
            new argb(255, 192, 192, 255),
            new argb(255, 224, 192, 255),
            new argb(255, 255, 255, 0),
            new argb(255, 255, 128, 128),
            new argb(255, 64, 255, 64),
            new argb(255, 128, 128, 255),
            new argb(255, 192, 128, 255),
            new argb(255, 255, 224, 192),
            new argb(255, 255, 0, 0),
            new argb(255, 0, 192, 0),
            new argb(255, 64, 64, 255),
            new argb(255, 128, 0, 255),
            new argb(255, 255, 192, 128),
            new argb(255, 255, 192, 224),
            new argb(255, 224, 255, 192),
            new argb(255, 192, 224, 255),
            new argb(255, 0, 192, 192),
            new argb(255, 255, 160, 64),
            new argb(255, 255, 128, 192),
            new argb(255, 128, 255, 192),
            new argb(255, 64, 160, 255),
            new argb(255, 0, 128, 128),
            new argb(255, 192, 96, 0),
            new argb(255, 255, 64, 160),
            new argb(255, 128, 255, 0),
            new argb(255, 0, 128, 255),
            new argb(255, 128, 128, 128)

            #endregion
        };

        const int bttnListOriginX = 12;
        const int bttnListOriginY = 12;
        const int bttnListSpaceX = 4;
        const int bttnListSpaceY = 4;

        const int bttnSizeX = 32;
        const int bttnSizeY = 32;

        Button[,] buttonArray = new Button[6, 5];

        public Dictionary<Color, Button> ButtonColorDict = new Dictionary<Color, Button>();

        private IEnumerable<Button> buttons => ButtonColorDict.Values;

        public delegate void ColorSelectedEvent(ColorSelectedEventArgs args);

        public event ColorSelectedEvent ColorSelected;

        public UCColorPalette()
        {
            InitializeComponent();

            Init();
        }

        public void Init()
        {
            
     
            int locnY = bttnListOriginY;
            int locnX = 0;

            int index = 0;

            for (int row = 0; row < 6; row++)
            {
                locnX = bttnListOriginX;

                for (int col = 0; col < 5; col++)
                {
                    Button button = new Button();

                    this.Controls.Add(button);

                    button.TabStop = false;

                    button.Size = new Size(bttnSizeX, bttnSizeY);

                    button.Location = new Point(locnX, locnY);

                    argb argb = paletteColors[index++];

                    button.BackColor = Color.FromArgb(argb.A, argb.R, argb.G, argb.B);

                    ButtonColorDict.Add(button.BackColor, button);

                    button.Click += Button_Click;

                    locnX += bttnSizeX + bttnListSpaceX;

                    buttonArray[row, col] = button;

                    button.FlatStyle = FlatStyle.Flat;

                    button.FlatAppearance.BorderColor = Color.White;
                    button.FlatAppearance.BorderSize = 2;
                }

                locnY += bttnSizeY + bttnListSpaceY;
            }


            buttonArray[0, 0].Select();
            
        }

        private void BtnSystemColors_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            DialogResult dialogResult = colorDialog.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            if (ColorSelected == null)
            {
                return;
            }

            ColorSelectedEventArgs args = new ColorSelectedEventArgs()
            {
                A = colorDialog.Color.A,
                R = colorDialog.Color.R,
                G = colorDialog.Color.G,
                B = colorDialog.Color.B
            };
            foreach (Button button in buttons)
            {
                if (button.FlatAppearance.BorderColor == Color.Black)
                {
                    button.BackColor = colorDialog.Color;
                }
            }
            ColorSelected(args);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (ColorSelected == null)
            {
                return;
            }

            Button button = (Button)sender;

            ColorSelectedEventArgs args = new ColorSelectedEventArgs()
            {
                A = button.BackColor.A,
                R = button.BackColor.R,
                G = button.BackColor.G,
                B = button.BackColor.B
            };

            ColorSelected(args);

            SetSelectedButtonFormat(button);
        }

        public void SetSelectedButtonFormat(Button selectedButton)
        {
            foreach (Button button in buttons)
            {
                if (button == selectedButton)
                {
                    button.FlatAppearance.BorderColor = Color.Black;
                }

                else
                {
                    button.FlatAppearance.BorderColor = Color.White;
                }
            }
        }

        public void SetSelectedButtonFormat(Color color)
        {
            foreach (Button button in buttons)
            {
                if (Utilities.ColorEquals(button.BackColor, color))
                {
                    button.FlatAppearance.BorderColor = Color.Black;
                }

                else
                {
                    button.FlatAppearance.BorderColor = Color.White;
                }
            }
        }
    }

}
