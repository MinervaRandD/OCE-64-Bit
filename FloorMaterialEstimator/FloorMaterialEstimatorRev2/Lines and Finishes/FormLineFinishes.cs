using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator.Finish_Controls
{
    public partial class FormLineFinishes : Form

    {
        public PictureBox[] dialogBox;
        public PictureBox[] listBox = new PictureBox[50];
        public TextBox[] listTextBox = new TextBox[50];
        public List<Label> listLabel = new List<Label>();
        public Panel[] linestylePanel = new Panel[10];
        public List<Color> listColors = new List<Color>();
        public Pen[] penStyles = new Pen[10];
        public int dialogBoxCount = 30;
        public int[] dialogR = new int[31];
        public int[] dialogG = new int[31];
        public int[] dialogB = new int[31];
        public int listItemsCount = 12;
        public int currentListIndex = 0;
        public int dialogBoxIndex;
        public int maxListItems = 30;
        public int listItemGap = 36;
        public int y = 0;
        public int x2 = 0;
        public List<int> listWidths = new List<int>();
        public List<int> listStyles = new List<int>();
        public List<int> lineR = new List<int>();
        public List<int> lineB = new List<int>();
        public List<int> lineG = new List<int>();

        private Random rnd = new Random();

        public List<string> listLineMarks = new List<string>(){"Line-1","Line-2","Line-3","Line-4","Line-5","Line-6",
                "Line-7","Line-8","Line-9","Line-10","Line-11","Line-12","Line-13","Line-14","Line-15",
                "Line-16","Line-17","Line-18","Line-19","Line-20","Line-21","Line-22","Line-23","Line-24",
                "Finish25", "Line-26","Line-27","Line-28","Line-29","Line-30"};

        public FormLineFinishes()
        {
            InitializeComponent();

            dialogBox = new PictureBox[]
            {pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5,
            pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10,
            pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15,
            pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20,
            pictureBox21, pictureBox22, pictureBox23, pictureBox24, pictureBox25,
            pictureBox26, pictureBox27, pictureBox28, pictureBox29, pictureBox30};
            InitializeForm();
        }
        private void InitializeForm()
        {
            LoadDefaultStyles();
            InitializeDialogBoxes();
            InitializeListColors();
            CreateLabels(listItemsCount);
            InitializeStylePanels();
            SetDrawingStyles(2);
            LoadlstLineWidth();
            listLabel[currentListIndex].BackColor = Color.FromArgb(240, 240, 240);
            listLabel[currentListIndex].BorderStyle = BorderStyle.FixedSingle;
            lblHidden.Visible = false;
            label3.Text = "";
        }

        private void LoadDefaultStyles()
        {
            for (int i = 0; i < listItemsCount; i++)
            {
                listWidths.Add(2);
                listStyles.Add(0);
                lineR.Add(0); lineG.Add(0); lineB.Add(0);
            }
            lineR[0] = 192; lineG[0] = 0; lineB[0] = 0;
            lineR[1] = 0; lineG[1] = 0; lineB[1] = 255;
            lineR[2] = 0; lineG[2] = 128; lineB[2] = 0;
            lineR[3] = 255; lineG[3] = 159; lineB[3] = 63;
            lineR[4] = 255; lineG[4] = 64; lineB[4] = 160;
            lineR[5] = 128; lineG[5] = 0; lineB[5] = 255;
            lineR[6] = 0; lineG[6] = 192; lineB[6] = 0;
            lineR[7] = 47; lineG[7] = 151; lineB[7] = 255;
            lineR[8] = 0; lineG[8] = 0; lineB[8] = 255;
            lineR[9] = 192; lineG[9] = 0; lineB[9] = 0;
            lineR[10] = 0; lineG[10] = 128; lineB[10] = 0;
            lineR[11] = 0; lineG[11] = 0; lineB[11] = 0;
        }

        private void LoadlstLineWidth()
        {
            lstLineWidth.Items.Add("  1  pt");
            lstLineWidth.Items.Add("  2  pt");
            lstLineWidth.Items.Add("  3  pt");
            lstLineWidth.Items.Add("  4  pt");
            lstLineWidth.Items.Add("  5  pt");
            lstLineWidth.Items.Add("  6  pt");
            lstLineWidth.SelectedIndex = 1;
        }

        private void InitializeStylePanels()
        {
            linePanel2.Top = linePanel1.Top + linePanel1.Height + 2;
            linePanel3.Top = linePanel2.Top + linePanel1.Height + 2;
            linePanel4.Top = linePanel3.Top + linePanel1.Height + 2;
            linePanel5.Top = linePanel4.Top + linePanel1.Height + 2;
            linePanel6.Top = linePanel5.Top + linePanel1.Height + 2;
            linePanel7.Top = linePanel6.Top + linePanel1.Height + 2;
            y = linePanel1.Height / 2;
            x2 = linePanel1.Width;
            linePanel1.BackColor = Color.FromArgb(220, 220, 220);
        }

        private void SetDrawingStyles(int w)
        {
            for (int i = 0; i < 7; i++)
            {
                penStyles[i] = new Pen(Color.Black, w);
            }
            //float[] DashPattern0 = { 1, 0, 1, 0 };
            //penStyles[1].DashPattern = DashPattern0;
            float[] DashPattern1 = { 1, 1, 1, 1 };
            penStyles[1].DashPattern = DashPattern1;
            float[] DashPattern2 = { 2, 1, 2, 1 };
            penStyles[2].DashPattern = DashPattern2;
            float[] DashPattern3 = { 4, 1, 4, 1 };
            penStyles[3].DashPattern = DashPattern3;
            float[] DashPattern4 = { 6, 2, 6, 2 };
            penStyles[4].DashPattern = DashPattern4;
            float[] DashPattern5 = { 4, 2, 2, 2 };
            penStyles[5].DashPattern = DashPattern5;
            float[] DashPattern6 = { 8, 2, 3, 2 };
            penStyles[6].DashPattern = DashPattern6;
        }

        private void InitializeListColors()
        {
            for (int i = 0; i < maxListItems; i++)
            {
                listColors.Add(Color.FromArgb(220, 220, 220));
            }
        }

        private void InitializeDialogBoxes()
        {
            dialogBox[0].BorderStyle = BorderStyle.FixedSingle;
            dialogBox[0].BackColor = Color.FromArgb(225, 0, 225);
            dialogBox[0].Tag = Convert.ToString(100);
            dialogBoxIndex = 0;

            dialogR[1] = 255; dialogG[1] = 255; dialogB[1] = 192;
            dialogR[2] = 255; dialogG[2] = 125; dialogB[2] = 125;
            dialogR[3] = 160; dialogG[3] = 255; dialogB[3] = 64;
            dialogR[4] = 128; dialogG[4] = 192; dialogB[4] = 255;
            dialogR[5] = 194; dialogG[5] = 133; dialogB[5] = 255;

            dialogR[6] = 255; dialogG[6] = 255; dialogB[6] = 0;
            dialogR[7] = 255; dialogG[7] = 0; dialogB[7] = 0;
            dialogR[8] = 0; dialogG[8] = 192; dialogB[8] = 0;
            dialogR[9] = 47; dialogG[9] = 151; dialogB[9] = 255;
            dialogR[10] = 167; dialogG[10] = 79; dialogB[10] = 255;

            dialogR[11] = 255; dialogG[11] = 159; dialogB[11] = 63;
            dialogR[12] = 192; dialogG[12] = 0; dialogB[12] = 0;
            dialogR[13] = 0; dialogG[13] = 128; dialogB[13] = 0;
            dialogR[14] = 0; dialogG[14] = 0; dialogB[14] = 255;
            dialogR[15] = 128; dialogG[15] = 0; dialogB[15] = 255;

            dialogR[16] = 192; dialogG[16] = 96; dialogB[16] = 0;
            dialogR[17] = 255; dialogG[17] = 128; dialogB[17] = 255;
            dialogR[18] = 0; dialogG[18] = 255; dialogB[18] = 128;
            dialogR[19] = 0; dialogG[19] = 102; dialogB[19] = 204;
            dialogR[20] = 0; dialogG[20] = 192; dialogB[20] = 192;

            dialogR[21] = 122; dialogG[21] = 61; dialogB[21] = 0;
            dialogR[22] = 255; dialogG[22] = 64; dialogB[22] = 160;
            dialogR[23] = 0; dialogG[23] = 192; dialogB[23] = 96;
            dialogR[24] = 0; dialogG[24] = 64; dialogB[24] = 128;
            dialogR[25] = 0; dialogG[25] = 128; dialogB[25] = 128;

            dialogR[26] = 0; dialogG[26] = 0; dialogB[26] = 0;
            dialogR[27] = 134; dialogG[27] = 134; dialogB[27] = 134;
            dialogR[28] = 182; dialogG[28] = 182; dialogB[28] = 182;
            dialogR[29] = 228; dialogG[29] = 228; dialogB[29] = 228;
            dialogR[30] = 255; dialogG[30] = 255; dialogB[30] = 255;

            for (int i = 1; i < dialogBoxCount; i++)
            {
                dialogBox[i].BorderStyle = BorderStyle.FixedSingle;
                dialogBox[i].Tag = Convert.ToString(i + 100);
            }

            for (int i = 1; i <= dialogBoxCount; i++)
            {
                dialogBox[i - 1].BackColor = Color.FromArgb(dialogR[i], dialogG[i], dialogB[i]);
            }

            foreach (var pic in panel1.Controls.OfType<PictureBox>())
            {
                pic.Click += HandleClick;
            }

        }

        private void SetSelectedListItem(int index)
        {
            for (int i = 0; i < listItemsCount; i++)
            {
                if (i != currentListIndex)
                {
                    listLabel[i].BorderStyle = BorderStyle.None;
                    listLabel[i].BackColor = panel3.BackColor;
                }
            }
            if (index != currentListIndex)
            {
                listLabel[index].BorderStyle = BorderStyle.FixedSingle;
                listLabel[index].BackColor = Color.FromArgb(250, 250, 250);
            }
        }


        private void HandleClick(object sender, EventArgs e)

        {
            if (sender.GetType() == typeof(System.Windows.Forms.Label))
            {
                string listLabelTag = Convert.ToString(((Label)sender).Tag);
                for (int i = 0; i < listItemsCount; i++)
                    if (listLabel[i].Tag == listLabelTag)
                    {
                        currentListIndex = i;
                        textBox2.Text = listLabel[i].Text;
                        SetSelectedListItem(i);
                        listLabel[currentListIndex].BackColor = Color.FromArgb(240, 240, 240);
                        listLabel[currentListIndex].BorderStyle = BorderStyle.FixedSingle;
                        label3.Text = Convert.ToString(currentListIndex);
                    }
                return;
            }
            string boxTag = Convert.ToString(((PictureBox)sender).Tag);
            label3.Text = boxTag;
            if (Convert.ToInt32(boxTag) < 100)
            {
                for (int i = 0; i < listItemsCount; i++)
                {
                    if (listBox[i].Tag == boxTag)
                    {
                        SetSelectedListItem(i);
                    }
                }
            }
            else if (Convert.ToInt32(boxTag) >= 100)
            {
                for (int i = 0; i < dialogBoxCount; i++)
                {
                    if (dialogBox[i].Tag == boxTag)
                    {
                        dialogBox[i].BorderStyle = BorderStyle.Fixed3D;
                        lblFrame.Top = dialogBox[i].Top - 4;
                        lblFrame.Left = dialogBox[i].Left - 4;
                        lblFrame2.Top = lblFrame.Top + 2;
                        lblFrame2.Left = lblFrame.Left + 2;
                        lblFrame.Visible = true;
                        lblFrame2.Visible = true;
                        dialogBoxIndex = i;
                        lineR[currentListIndex] = dialogR[dialogBoxIndex + 1];
                        lineG[currentListIndex] = dialogG[dialogBoxIndex + 1];
                        lineB[currentListIndex] = dialogB[dialogBoxIndex + 1];
                        panel3.Refresh();
                    }
                    else
                    {
                        dialogBox[i].BorderStyle = BorderStyle.FixedSingle;
                    }
                }
            }
        }

        private void FormAreaFinishes_Load(object sender, EventArgs e)
        {
            panel3.Width = panel2.Width - 20;
            panel3.Left = 0;
            panel3.Top = 0;
            panel2.AutoScroll = true;
            lblFrame.Top = dialogBox[0].Top - 4;
            lblFrame.Left = dialogBox[0].Left - 4;
            lblFrame2.Top = lblFrame.Top + 2;
            lblFrame2.Left = lblFrame.Left + 2;
        }


        private void CreateLabel(int i)
        {
            Label mainLabel = new Label();
            listLabel.Add(mainLabel);
            panel3.Controls.Add(listLabel[i]);
            listLabel[i].Top = listItemGap * (i) + 5;
            listLabel[i].Left = lblHidden.Left;
            listLabel[i].Height = lblHidden.Height;
            listLabel[i].AutoSize = true;
            listLabel[i].Text = listLineMarks[i];
            listLabel[i].Font = lblHidden.Font;
            listLabel[i].BorderStyle = BorderStyle.None;
            listLabel[i].BackColor = panel3.BackColor;
            listLabel[i].Tag = Convert.ToString(i + 101);
            listLabel[i].Click += HandleClick;
            listLabel[i].MouseEnter += FormAreaFinishes_MouseEnter;
        }

        private void CreateLabels(int lineCount)
        {
            for (int i = 0; i < lineCount; i++)
            {
                CreateLabel(i);
            }
        }

        private void FormAreaFinishes_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Text = "Done"; //?????????????
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (listItemsCount + 1 > maxListItems)
            {
                var result = MessageBox.Show("Max list items reached");
                //Should never happen but need to deal with - TBD
            }
            listLineMarks[listItemsCount] = "TBD";
            CreateLabel(listItemsCount);
            listItemsCount++;
            listStyles.Add(0);
            listWidths.Add(2);
            lineR.Add(0);
            lineG.Add(0);
            lineB.Add(0);
            listLabel[listItemsCount - 1].Top = listLabel[listItemsCount - 1].Top + 20;
            panel2.ScrollControlIntoView(listLabel[listItemsCount - 1]);
            listLabel[listItemsCount - 1].Top = listLabel[listItemsCount - 1].Top - 20;
            listLabel[listItemsCount - 1].BorderStyle = BorderStyle.FixedSingle;
            listLabel[listItemsCount - 1].BackColor = Color.FromArgb(220, 220, 220);
            currentListIndex = listItemsCount - 1;
            textBox2.Text = "TBD";
            textBox2.Focus();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            listLineMarks.Insert(currentListIndex, "TBD");
            panel3.Controls.Clear();
            listItemsCount++;
            listStyles.Insert(currentListIndex, 0);
            listWidths.Insert(currentListIndex, 2);
            lineR.Insert(currentListIndex, 0);
            lineG.Insert(currentListIndex, 255);
            lineB.Insert(currentListIndex, 255);
            CreateLabels(listItemsCount);
            listLabel[currentListIndex].BorderStyle = BorderStyle.FixedSingle;
            listLabel[currentListIndex].BackColor = Color.FromArgb(220, 220, 220);
            panel3.Refresh();
            textBox2.Text = "TBD";
            textBox2.Focus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            listLineMarks.RemoveAt(currentListIndex);
            listStyles.RemoveAt(currentListIndex);
            listWidths.RemoveAt(currentListIndex);
            lineR.RemoveAt(currentListIndex);
            lineG.RemoveAt(currentListIndex);
            lineB.RemoveAt(currentListIndex);
            listItemsCount--;
            listLabel.Clear();
            panel3.Controls.Clear();
            CreateLabels(listItemsCount);
            panel3.Refresh();
        }

        private void btnColorDialog_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                dialogBox[dialogBoxIndex].BackColor = colorDialog1.Color;
                string s = Convert.ToString(colorDialog1.Color);
                int a = colorDialog1.Color.A;
                int r = colorDialog1.Color.R;
                int g = colorDialog1.Color.G;
                int b = colorDialog1.Color.B;
                label3.Text = s;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            int y = 0;
            int x1 = lblHidden.Left + 4;
            int x2 = panel3.Width - 14;
            Pen StyledPen = new Pen(Color.Black, 1);
            Pen SolidPen = new Pen(Color.Black, 1);
            System.Drawing.Graphics g = e.Graphics;
            int j = 0;
            for (int i = 0; i < listItemsCount; i++)
            {
                y = listLabel[i].Top + listLabel[i].Height + 4;
                j = listStyles[i];
                if (j > 0)
                {
                    StyledPen.Color = Color.FromArgb(lineR[i], lineG[i], lineB[i]);
                    StyledPen.DashPattern = penStyles[j].DashPattern;
                    StyledPen.Width = listWidths[i];
                    g.DrawLine(StyledPen, x1, y, x2, y);
                }
                else
                {
                    SolidPen.Color = Color.FromArgb(lineR[i], lineG[i], lineB[i]);
                    SolidPen.Width = listWidths[i];
                    g.DrawLine(SolidPen, x1, y, x2, y);
                }

            }

        }

        private void linePanel1_Paint_1(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.DrawLine(penStyles[0], 0, y, x2, y);
        }

        private void linePanel2_Paint_1(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.DrawLine(penStyles[1], 0, y, x2, y);
        }

        private void linePanel3_Paint_1(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.DrawLine(penStyles[2], 0, y, x2, y);
        }

        private void linePanel4_Paint_1(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.DrawLine(penStyles[3], 0, y, x2, y);
        }

        private void linePanel5_Paint_1(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.DrawLine(penStyles[4], 0, y, x2, y);
        }

        private void linePanel6_Paint_1(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.DrawLine(penStyles[5], 0, y, x2, y);
        }

        private void linePanel7_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.DrawLine(penStyles[6], 0, y, x2, y);
        }

        public void clearPanelBorders()
        {
            linePanel1.BorderStyle = BorderStyle.None;
            linePanel2.BorderStyle = BorderStyle.None;
            linePanel3.BorderStyle = BorderStyle.None;
            linePanel4.BorderStyle = BorderStyle.None;
            linePanel5.BorderStyle = BorderStyle.None;
            linePanel6.BorderStyle = BorderStyle.None;
            linePanel7.BorderStyle = BorderStyle.None;
        }

        private void panelLineStyles_MouseLeave(object sender, EventArgs e)
        {
            clearPanelBorders();
        }

        private void lstLineWidth_Click(object sender, EventArgs e)
        {
            int i = lstLineWidth.SelectedIndex;
            SetDrawingStyles(i + 1);
            linePanel1.Refresh();
            linePanel2.Refresh();
            linePanel3.Refresh();
            linePanel4.Refresh();
            linePanel5.Refresh();
            linePanel6.Refresh();
            linePanel7.Refresh();
            listWidths[currentListIndex] = lstLineWidth.SelectedIndex + 1;
            panel3.Refresh();
        }


        private void FormAreaFinishes_MouseEnter(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(System.Windows.Forms.Label))
            {
                string labelTag = Convert.ToString(((Label)sender).Tag);
                for (int i = 0; i < listItemsCount; i++)
                    if (listLabel[i].Tag == labelTag)
                    {
                        SetSelectedListItem(i);
                    }
                return;
            }
        }

        private void panel3_MouseEnter(object sender, EventArgs e)
        {
            for (int i = 0; i < listItemsCount; i++)
            {
                if (i != currentListIndex)
                {
                    listLabel[i].BackColor = panel3.BackColor;
                    listLabel[i].BorderStyle = BorderStyle.None;
                }

            }
        }

        private void linePanel1_MouseEnter(object sender, EventArgs e)
        {
            clearPanelBorders();
            linePanel1.BorderStyle = BorderStyle.FixedSingle;
        }

        private void linePanel2_MouseEnter(object sender, EventArgs e)
        {
            clearPanelBorders();
            linePanel2.BorderStyle = BorderStyle.FixedSingle;
        }

        private void linePanel3_MouseEnter(object sender, EventArgs e)
        {
            clearPanelBorders();
            linePanel3.BorderStyle = BorderStyle.FixedSingle;
        }

        private void linePanel4_MouseEnter(object sender, EventArgs e)
        {
            clearPanelBorders();
            linePanel4.BorderStyle = BorderStyle.FixedSingle;
        }

        private void linePanel5_MouseEnter(object sender, EventArgs e)
        {
            clearPanelBorders();
            linePanel5.BorderStyle = BorderStyle.FixedSingle;
        }

        private void linePanel6_MouseEnter(object sender, EventArgs e)
        {
            clearPanelBorders();
            linePanel6.BorderStyle = BorderStyle.FixedSingle;
        }

        private void linePanel7_MouseEnter(object sender, EventArgs e)
        {
            clearPanelBorders();
            linePanel7.BorderStyle = BorderStyle.FixedSingle;
        }

        private void linePanel1_MouseClick(object sender, MouseEventArgs e)
        {
            ProcessListStyle(0);
        }

        private void linePanel2_MouseClick(object sender, MouseEventArgs e)
        {
            ProcessListStyle(1);
        }

        private void linePanel3_MouseClick(object sender, MouseEventArgs e)
        {
            ProcessListStyle(2);
        }

        private void linePanel4_MouseClick(object sender, MouseEventArgs e)
        {
            ProcessListStyle(3);
        }

        private void linePanel5_MouseClick(object sender, MouseEventArgs e)
        {
            ProcessListStyle(4);
        }

        private void linePanel6_MouseClick(object sender, MouseEventArgs e)
        {
            ProcessListStyle(5);
        }

        private void linePanel7_MouseClick(object sender, MouseEventArgs e)
        {
            ProcessListStyle(6);
        }

        private void ProcessListStyle(int i)
        {
            listStyles[currentListIndex] = i;
            listWidths[currentListIndex] = lstLineWidth.SelectedIndex + 1;
            UnHighlightStyles();
            switch (i)
            {
                case 0:
                    linePanel1.BackColor = Color.FromArgb(220, 220, 220);
                    break;
                case 1:
                    linePanel2.BackColor = Color.FromArgb(220, 220, 220);
                    break;
                case 2:
                    linePanel3.BackColor = Color.FromArgb(220, 220, 220);
                    break;
                case 3:
                    linePanel4.BackColor = Color.FromArgb(220, 220, 220);
                    break;
                case 4:
                    linePanel5.BackColor = Color.FromArgb(220, 220, 220);
                    break;
                case 5:
                    linePanel6.BackColor = Color.FromArgb(220, 220, 220);
                    break;
                case 6:
                    linePanel7.BackColor = Color.FromArgb(220, 220, 220);
                    break;
            }
            panel3.Refresh();
        }

        private void UnHighlightStyles()
        {
            linePanel1.BackColor = panel6.BackColor;
            linePanel2.BackColor = panel6.BackColor;
            linePanel3.BackColor = panel6.BackColor;
            linePanel4.BackColor = panel6.BackColor;
            linePanel5.BackColor = panel6.BackColor;
            linePanel6.BackColor = panel6.BackColor;
            linePanel7.BackColor = panel6.BackColor;
        }


    }
}
