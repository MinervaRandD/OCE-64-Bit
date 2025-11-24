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
    public partial class FormAreaFinishes : Form
    {
        public PictureBox[] dialogBox;
        public PictureBox[] listBox = new PictureBox[50];
        public TextBox[] listTextBox = new TextBox[50];
        public List<Color> adminColors = new List<Color>();
        public List<Color> listColors = new List<Color>();
        public int dialogBoxCount = 30;
        public int listBoxCount = 12;
        public int dialogBoxIndex;
        public int firstBoxTop;
        public int maxListItems = 30;

        private Random rnd = new Random();

        public List<string> listAreaMarks = new List<string>(){"Finish-1","Finish-2","Finish-3","Finish-4","Finish-5","Finish-6",
                "Finish-7","Finish-8","Finish-9","Finish-10","Finish-11","Finish-12","Finish-13","Finish-14","Finish-15",
                "Finish-16","Finish-17","Finish-18","Finish-19","Finish-20","Finish-21","Finish-22","Finish-23","Finish-24",
                "Finish25", "Finish-26","Finish-27","Finish-28","Finish-29","Finish-30"};
        public FormAreaFinishes()
        {
            InitializeComponent();

            dialogBox = new PictureBox[]
            {pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5,
            pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10,
            pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15,
            pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20,
            pictureBox21, pictureBox22, pictureBox23, pictureBox24, pictureBox25,
            pictureBox26, pictureBox27, pictureBox28, pictureBox29, pictureBox30};

            InitializeDialogBoxes();
            LoadAdminListColors();
            LoadUserListColors();
            CreateListPics(listBoxCount);
            CreateTextBoxes(listBoxCount);

        }

        private void LoadAdminListColors()
        {
            listColors.Add(Color.FromArgb(255, 255, 192));
            listColors.Add(Color.FromArgb(192, 255, 192));
            listColors.Add(Color.FromArgb(255, 192, 192));
            listColors.Add(Color.FromArgb(192, 224, 255));
            listColors.Add(Color.FromArgb(255, 192, 128));
            listColors.Add(Color.FromArgb(224, 192, 255));
            listColors.Add(Color.FromArgb(255, 255, 0));
            listColors.Add(Color.FromArgb(64, 255, 64));
            listColors.Add(Color.FromArgb(255, 128, 192));
            listColors.Add(Color.FromArgb(64, 160, 255));
            listColors.Add(Color.FromArgb(255, 160, 64));
            listColors.Add(Color.FromArgb(0, 192, 192));

        }

        private void LoadUserListColors()
        {
            // if (xml file exists)
            {
            }
        }


        private void InitializeDialogBoxes()
        {
            dialogBox[0].BorderStyle = BorderStyle.Fixed3D;
            dialogBox[0].BackColor = Color.FromArgb(225, 0, 225);
            dialogBox[0].Tag = Convert.ToString(100);
            dialogBoxIndex = 0;
            int[] dialogR = new int[dialogBoxCount + 1];
            int[] dialogG = new int[dialogBoxCount + 1];
            int[] dialogB = new int[dialogBoxCount + 1];

            dialogR[1] = 255; dialogG[1] = 255; dialogB[1] = 192;
            dialogR[2] = 255; dialogG[2] = 192; dialogB[2] = 192;
            dialogR[3] = 192; dialogG[3] = 255; dialogB[3] = 192;
            dialogR[4] = 192; dialogG[4] = 192; dialogB[4] = 255;
            dialogR[5] = 224; dialogG[5] = 192; dialogB[5] = 255;

            dialogR[6] = 255; dialogG[6] = 255; dialogB[6] = 0;
            dialogR[7] = 255; dialogG[7] = 128; dialogB[7] = 128;
            dialogR[8] = 64; dialogG[8] = 255; dialogB[8] = 64;
            dialogR[9] = 128; dialogG[9] = 128; dialogB[9] = 255;
            dialogR[10] = 192; dialogG[10] = 128; dialogB[10] = 255;

            dialogR[11] = 255; dialogG[11] = 224; dialogB[11] = 192;
            dialogR[12] = 255; dialogG[12] = 0; dialogB[12] = 0;
            dialogR[13] = 0; dialogG[13] = 192; dialogB[13] = 0;
            dialogR[14] = 64; dialogG[14] = 64; dialogB[14] = 255;
            dialogR[15] = 128; dialogG[15] = 0; dialogB[15] = 255;

            dialogR[16] = 255; dialogG[16] = 192; dialogB[16] = 128;
            dialogR[17] = 255; dialogG[17] = 192; dialogB[17] = 224;
            dialogR[18] = 224; dialogG[18] = 255; dialogB[18] = 192;
            dialogR[19] = 192; dialogG[19] = 224; dialogB[19] = 255;
            dialogR[20] = 0; dialogG[20] = 192; dialogB[20] = 192;

            dialogR[21] = 255; dialogG[21] = 160; dialogB[21] = 64;
            dialogR[22] = 255; dialogG[22] = 128; dialogB[22] = 192;
            dialogR[23] = 128; dialogG[23] = 255; dialogB[23] = 192;
            dialogR[24] = 64; dialogG[24] = 160; dialogB[24] = 255;
            dialogR[25] = 0; dialogG[25] = 128; dialogB[25] = 128;

            dialogR[26] = 192; dialogG[26] = 96; dialogB[26] = 0;
            dialogR[27] = 255; dialogG[27] = 64; dialogB[27] = 160;
            dialogR[28] = 128; dialogG[28] = 255; dialogB[28] = 0;
            dialogR[29] = 0; dialogG[29] = 128; dialogB[29] = 255;
            dialogR[30] = 128; dialogG[30] = 128; dialogB[30] = 128;

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

        private void BtnColorDialog_Click(object sender, EventArgs e)
        {

            if (colorDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                dialogBox[dialogBoxIndex].BackColor = colorDialog1.Color;
                string s = Convert.ToString(colorDialog1.Color);
                int a = colorDialog1.Color.A;
                int r = colorDialog1.Color.R;
                int b = colorDialog1.Color.B;
                int g = colorDialog1.Color.G;
                label3.Text = s;
            }
        }

        private void SetSelectedListItem(int index)
        {
            for (int j = 0; j < listBoxCount; j++)
            {
                listTextBox[j].BorderStyle = BorderStyle.None;
            }
            listTextBox[index].BorderStyle = BorderStyle.FixedSingle;
            textBox2.Text = listTextBox[index].Text;
            textBox2.Focus();
        }

        private void HandleClick(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(System.Windows.Forms.TextBox))
            {
                string textBoxTag = Convert.ToString(((TextBox)sender).Tag);
                for (int i = 0; i < listBoxCount; i++)
                    if (listTextBox[i].Tag == textBoxTag)
                    {
                        SetSelectedListItem(i);
                    }
                return;
            }
            string boxTag = Convert.ToString(((PictureBox)sender).Tag);
            label3.Text = boxTag;
            if (Convert.ToInt32(boxTag) < 100)
            {
                for (int i = 0; i < listBoxCount; i++)
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
                        listBox[ListIndex()].BackColor = dialogBox[i].BackColor;
                        listColors[ListIndex()] = dialogBox[i].BackColor;
                        dialogBoxIndex = i;
                    }
                    else
                    {
                        dialogBox[i].BorderStyle = BorderStyle.FixedSingle;
                    }
                }
            }
        }

        public int ListIndex()
        {
            int result = -1;
            for (int i = 0; i < listBoxCount; i++)
            {
                if (listTextBox[i].BorderStyle == BorderStyle.FixedSingle)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }


        private void FormAreaFinishes_Load(object sender, EventArgs e)
        {
            firstBoxTop = picBoxListHidden.Top;
            picBoxListHidden.Left = 12;
            picBoxListHidden.Visible = false;
            textBoxHidden.Visible = false;
            panel3.Width = panel2.Width;
            panel3.Left = 0;
            panel3.Top = 0;
            panel2.AutoScroll = true;
            txtTileFeet1.Text = "0'";
            txtTileFeet2.Text = "0'";
            txtTileInch1.Text = "0\"";
            txtTileInch2.Text = "0\"";
            txtRepeatFeet1.Text = "0'";
            txtRepeatFeet2.Text = "0'";
            txtRepeatInch1.Text = "0\"";
            txtRepeatInch2.Text = "0\"";
            lblFrame.Top = dialogBox[0].Top - 4;
            lblFrame.Left = dialogBox[0].Left - 4;
            lblFrame2.Top = lblFrame.Top + 2;
            lblFrame2.Left = lblFrame.Left + 2;
            cboTrim.Items.Add("0\"");
            cboTrim.Items.Add("1\"");
            cboTrim.Items.Add("2\"");
            cboTrim.Items.Add("3\"");
            cboTrim.Items.Add("4\"");
            cboTrim.Items.Add("5\"");
            cboTrim.Items.Add("6\"");
            cboOverlap.Items.Add("0\"");
            cboOverlap.Items.Add("1\"");
            cboOverlap.Items.Add("2\"");
            cboOverlap.Items.Add("3\"");
            cboOverlap.Items.Add("4\"");
            cboOverlap.Items.Add("5\"");
            cboOverlap.Items.Add("6\"");
            cboTrim.SelectedIndex = 0;
            cboOverlap.SelectedIndex = 0;
            SetSelectedListItem(0);

        }

        private void CreateListPic(int i)
        {
            int boxHeight = picBoxListHidden.Height;
            int gap = 6;
            PictureBox mainListBox = new PictureBox();
            listBox[i] = mainListBox;
            panel3.Controls.Add(listBox[i]);
            listBox[i].Height = picBoxListHidden.Height;
            listBox[i].Width = picBoxListHidden.Width;
            listBox[i].Left = picBoxListHidden.Left;
            if (i == 0)
            {
                listBox[i].Top = picBoxListHidden.Top;
            }
            else
            {
                listBox[i].Top = listBox[i - 1].Top + listBox[i - 1].Height + gap;
            }
            listBox[i].BorderStyle = picBoxListHidden.BorderStyle;
            listBox[i].BackColor = listColors[i];
            listBox[i].Tag = Convert.ToString(i);
            listBox[i].Click += HandleClick;
            panel3.Height = firstBoxTop + (listBoxCount) * (boxHeight + gap) + 2 * gap;
        }

        private void CreateListPics(int pBoxes)
        {
            for (int i = 0; i < pBoxes; i++)
            {
                CreateListPic(i);
            }
        }

        private void CreateTextBox(int i)
        {
            TextBox mainTextBox = new TextBox();
            listTextBox[i] = mainTextBox;
            panel3.Controls.Add(listTextBox[i]);
            listTextBox[i].Top = listBox[i].Top + listBox[i].Height / 2 - listTextBox[i].Height / 2;
            listTextBox[i].Left = listBox[i].Left + listBox[i].Width + 6;
            listTextBox[i].Width = textBoxHidden.Width;
            listTextBox[i].Text = listAreaMarks[i];
            listTextBox[i].Multiline = true;
            listTextBox[i].Font = textBoxHidden.Font;
            listTextBox[i].BorderStyle = BorderStyle.None;
            listTextBox[i].BackColor = panel3.BackColor;
            listTextBox[i].Tag = Convert.ToString(i);
            listTextBox[i].Click += HandleClick;
        }

        private void CreateTextBoxes(int pBoxes)
        {
            for (int i = 0; i < pBoxes; i++)
            {
                CreateTextBox(i);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void FormAreaFinishes_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Text = "Done";
            }
        }

        private void btnFinishItemOK_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            listTextBox[ListIndex()].Text = textBox2.Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listBoxCount++;
            if (listBoxCount > maxListItems)
            {
                var result = MessageBox.Show("Max list items reached");
                //Should never happen but need to deal with - TBD
            }
            listAreaMarks[listBoxCount - 1] = "TBD";
            CreateListPic(listBoxCount - 1);
            CreateTextBox(listBoxCount - 1);
            listTextBox[ListIndex()].BorderStyle = BorderStyle.None;
            listTextBox[listBoxCount - 1].BorderStyle = BorderStyle.FixedSingle;
            panel2.ScrollControlIntoView(listBox[listBoxCount - 1]);
            textBox2.Text = "TBD";
            textBox2.Focus();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int index = ListIndex();
            listColors.Insert(index, Color.FromArgb(240, 240, 240));
            listAreaMarks.Insert(index, "TBD");
            panel3.Visible = false;
            panel3.Controls.Clear();
            listBoxCount++;
            CreateListPics(listBoxCount);
            CreateTextBoxes(listBoxCount);
            panel3.Visible = true;
            listTextBox[index].BorderStyle = BorderStyle.FixedSingle;
            panel2.ScrollControlIntoView(listTextBox[index]);
            textBox2.Text = "TBD";
            textBox2.Focus();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxCount == 1)
            {
                var result = MessageBox.Show("There must be at least one finish type.");
                return;
            }
            panel3.Visible = false;
            panel3.Controls.Clear();
            int currentIndex = ListIndex();
            listColors.RemoveAt(currentIndex);
            listAreaMarks.RemoveAt(currentIndex);
            listTextBox[currentIndex].BorderStyle = BorderStyle.FixedSingle;
            listBoxCount--;
            CreateListPics(listBoxCount);
            CreateTextBoxes(listBoxCount);
            listTextBox[currentIndex].BorderStyle = BorderStyle.FixedSingle;
            panel3.Visible = true;
        }

    }
}

