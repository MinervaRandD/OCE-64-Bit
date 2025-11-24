#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: TextBoxEditForm.cs. Project: Graphics. Created: 6/10/2024         */
/*                                                                                                     */
/* Copyright (c) 2025, Minerva Research and Development, LLC. All rights reserved.                     */
/*                                                                                                     */
/* Not to be copied or distributed in any way without prior authorization. If provided with permission,*/
/* this software is provided without warranty of any kind, express or implied,                         */
/* including but not limited to the warranties of merchantability, fitness for a particular            */
/* purpose, and non-infringement. In no event shall the authors or copyright holders be liable         */
/* for any claim, damages, or other liability, whether in an action of contract, tort, or              */
/* otherwise, arising from, out of, or in connection with the software or the use or other             */
/* dealings in the software.                                                                           */
/*                                                                                                     */
/* Author: Marc Diamond, Minerva Research and Development, LLC                                         */
/*                                                                                                     */
/*******************************************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities.User_Controls;

namespace Graphics
{
    public partial class TextBoxEditForm : Form
    {

        public delegate void ColorSelectedEventHandler(Color color);

        public event ColorSelectedEventHandler TextColorSelected;

        public delegate void TextChangedEventHandler(string text);

        public new event TextChangedEventHandler TextChanged;

        public delegate void TextHorizontalAlignmentChangedEventHandler(HorizontalAlignment horizontalAlignment);

        public event TextHorizontalAlignmentChangedEventHandler TextHorizontalAlignmentChanged;

        public delegate void TextVerticalAlignmentChangedEventHandler(VerticalAlignment verticalAlignment);

        public event TextVerticalAlignmentChangedEventHandler TextVerticalAlignmentChanged;

        public delegate void FontFaceChangedHandler(bool boldFontFace, bool italicFontFace, bool underlineFontFace);

        public event FontFaceChangedHandler FontFaceChanged;

        HorizontalAlignment _textHorizontalAlignment = HorizontalAlignment.Left;

        public HorizontalAlignment TextHorizontalAlignment
        {
            get
            {
                return _textHorizontalAlignment;
            }

            set
            {
                if (value == _textHorizontalAlignment)
                {
                    return;
                }

                _textHorizontalAlignment = value;

                this.txbTextBoxText.TextAlign = _textHorizontalAlignment;
              
                if (TextHorizontalAlignmentChanged != null)
                {
                    TextHorizontalAlignmentChanged.Invoke(_textHorizontalAlignment);
                }
            }
        }

        VerticalAlignment _textVerticalAlignment = VerticalAlignment.Top;


        public VerticalAlignment TextVerticalAlignment
        {
            get
            {
                return _textVerticalAlignment;
            }

            set
            {
                if (value == _textVerticalAlignment)
                {
                    return;
                }

                _textVerticalAlignment = value;

                if (TextVerticalAlignmentChanged != null)
                {
                    TextVerticalAlignmentChanged.Invoke(_textVerticalAlignment);
                }
            }
        }

        public TextBoxEditForm()
        {
            InitializeComponent();

            this.txbTextBoxText.TextChanged += txbTextBoxText_TextChanged;

            this.btnTextAlignLeft.BackColor = SystemColors.ControlLight;
            this.btnTextAlignTop.BackColor = SystemColors.ControlLight;
        }

        private void txbTextBoxText_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
            {
                TextChanged.Invoke(this.txbTextBoxText.Text);
            }
        }

        private void nudFontSize_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSelectTextColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            DialogResult dr = cd.ShowDialog();

            if (dr != DialogResult.OK)
            {
                return;
            }

            Color selectedColor = cd.Color;

            this.txbTextBoxText.ForeColor = selectedColor;

            this.btnSelectTextColor.ForeColor = selectedColor;

            if (TextColorSelected != null)
            {
                TextColorSelected.Invoke(selectedColor);
            }

        }

        private void btnTextAlignLeft_Click(object sender, EventArgs e)
        {
            if (TextHorizontalAlignment == HorizontalAlignment.Left)
            {
                return;
            }

            TextHorizontalAlignment = HorizontalAlignment.Left;

            this.btnTextAlignLeft.BackColor = SystemColors.ControlLight;
            this.btnTextAlignMiddle.BackColor = SystemColors.ControlLightLight;
            this.btnTextAlignRight.BackColor = SystemColors.ControlLightLight;

            this.txbTextBoxText.TextAlign = HorizontalAlignment.Left;
        }

        private void btnTextAlignMiddle_Click(object sender, EventArgs e)
        {
            if (TextHorizontalAlignment == HorizontalAlignment.Center)
            {
                return;
            }

            TextHorizontalAlignment = HorizontalAlignment.Center;

            this.btnTextAlignLeft.BackColor = SystemColors.ControlLightLight;
            this.btnTextAlignMiddle.BackColor = SystemColors.ControlLight;
            this.btnTextAlignRight.BackColor = SystemColors.ControlLightLight;
        }

        private void btnTextAlignRight_Click(object sender, EventArgs e)
        {
            if (TextHorizontalAlignment == HorizontalAlignment.Right)
            {
                return;
            }

            TextHorizontalAlignment = HorizontalAlignment.Right;

            this.btnTextAlignLeft.BackColor = SystemColors.ControlLightLight;
            this.btnTextAlignMiddle.BackColor = SystemColors.ControlLightLight;
            this.btnTextAlignRight.BackColor = SystemColors.ControlLight;
        }

        private void btnTextAlignTop_Click(object sender, EventArgs e)
        {
            if (TextVerticalAlignment == VerticalAlignment.Top)
            {
                return;
            }

            TextVerticalAlignment = VerticalAlignment.Top;

            this.btnTextAlignTop.BackColor = SystemColors.ControlLight;
            this.btnTextAlignCenter.BackColor = SystemColors.ControlLightLight;
            this.btnTextAlignRight.BackColor = SystemColors.ControlLightLight;
        }

        private void btnTextAlignCenter_Click(object sender, EventArgs e)
        {
            if (TextVerticalAlignment == VerticalAlignment.Center)
            {
                return;
            }

            TextVerticalAlignment = VerticalAlignment.Center;

            this.btnTextAlignTop.BackColor = SystemColors.ControlLightLight;
            this.btnTextAlignCenter.BackColor = SystemColors.ControlLight;
            this.btnTextAlignRight.BackColor = SystemColors.ControlLightLight;
        }

        private void btnTextAlignBottom_Click(object sender, EventArgs e)
        {
            if (TextVerticalAlignment == VerticalAlignment.Bottom)
            {
                return;
            }

            TextVerticalAlignment = VerticalAlignment.Bottom;

            this.btnTextAlignTop.BackColor = SystemColors.ControlLightLight;
            this.btnTextAlignCenter.BackColor = SystemColors.ControlLightLight;
            this.btnTextAlignBottom.BackColor = SystemColors.ControlLight;
        }

        private bool _boldFontFace = false;

        public bool BoldFontFace
        {
            get
            {
                return _boldFontFace;
            }

            set
            {
                if (value == _boldFontFace)
                {
                    return;
                }

                if (FontFaceChanged != null)
                {
                    FontFaceChanged.Invoke(BoldFontFace, ItalicFontFace, UnderlineFontFace);
                }
            }
        }


        private bool _italicFontFace = false;

        public bool ItalicFontFace
        {
            get
            {
                return _italicFontFace;
            }

            set
            {
                if (value == _italicFontFace)
                {
                    return;
                }

                if (FontFaceChanged != null)
                {
                    FontFaceChanged.Invoke(BoldFontFace, ItalicFontFace, UnderlineFontFace);
                }
            }
        }


        private bool _underlineFontFace = false;

        public bool UnderlineFontFace
        {
            get
            {
                return _underlineFontFace;
            }

            set
            {
                if (value == _underlineFontFace)
                {
                    return;
                }

                if (FontFaceChanged != null)
                {
                    FontFaceChanged.Invoke(BoldFontFace, ItalicFontFace, UnderlineFontFace);
                }
            }
        }

        private void btnBoldFont_Click(object sender, EventArgs e)
        {
            if (btnBoldFont.BackColor == SystemColors.ControlLight)
            {
                btnBoldFont.BackColor = SystemColors.ControlLightLight;

                BoldFontFace = false;
            }

            else
            {
                btnBoldFont.BackColor = SystemColors.ControlLight;

                BoldFontFace = true;
            }
            
        }

        private void btnItalicFont_Click(object sender, EventArgs e)
        {
            if (btnItalicFont.BackColor == SystemColors.ControlLight)
            {
                btnItalicFont.BackColor = SystemColors.ControlLightLight;

                ItalicFontFace = false;
            }

            else
            {
                btnItalicFont.BackColor = SystemColors.ControlLight;

                ItalicFontFace = true;
            }
        }

        private void btnUnderlineFont_Click(object sender, EventArgs e)
        {
            if (btnUnderlineFont.BackColor == SystemColors.ControlLight)
            {
                btnUnderlineFont.BackColor = SystemColors.ControlLightLight;

                UnderlineFontFace = false;
            }

            else
            {
                btnUnderlineFont.BackColor = SystemColors.ControlLight;

                UnderlineFontFace = true;
            }
        }
    }
}
