//-------------------------------------------------------------------------------//
// <copyright file="ScaleForm.cs" company="Bruun Estimating, LLC">               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FloorMaterialEstimator.Supporting_Forms
{
    public partial class ScaleFormOld : Form
    {
        private double feet = 0.0;
        private double inch = 0.0;

        public double TotalInches = 0.0;


        public ScaleFormOld()
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.MinimizeBox = false;
           
            this.ControlBox = false;

            this.txbFeet.BackColor = Color.White;
            this.txbInch.BackColor = Color.White;

            this.txbFeet.TextChanged += TxbFeet_TextChanged;
            this.txbInch.TextChanged += TxbInch_TextChanged;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            string StrFeet = this.txbFeet.Text.Trim();

            if (string.IsNullOrWhiteSpace(StrFeet))
            {
                feet = 0.0;
            }

            else
            {
                if (!double.TryParse(StrFeet, out feet))
                {
                    MessageBox.Show("Please enter a valid value for feet.");
                    return;
                }
            }

            string StrInch = this.txbInch.Text.Trim();

            if (string.IsNullOrWhiteSpace(StrInch))
            {
                inch = 0.0;
            }

            else
            {
                if (!double.TryParse(StrInch, out inch))
                {
                    MessageBox.Show("Please enter a valid value for inches.");
                    return;
                }
            }

            TotalInches = 12.0 * feet + inch;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void TxbFeet_TextChanged(object sender, EventArgs e)
        {
            string StrFeet = this.txbFeet.Text.Trim();

            if (string.IsNullOrWhiteSpace(StrFeet))
            {
                this.txbFeet.BackColor = Color.White;

                return;
            }

            if (!Utilities.Utilities.IsValidPosDbl(StrFeet))
            {
                this.txbFeet.BackColor = Color.Pink;
            }
            
            else
            {
                this.txbFeet.BackColor = Color.White;
            }
        }

        private void TxbInch_TextChanged(object sender, EventArgs e)
        {
            string StrInch = this.txbInch.Text.Trim();

            if (string.IsNullOrWhiteSpace(StrInch))
            {
                this.txbInch.BackColor = Color.White;

                return;
            }

            if (!Utilities.Utilities.IsValidPosDbl(StrInch))
            {
                this.txbInch.BackColor = Color.Pink;
            }

            else
            {
                this.txbInch.BackColor = Color.White;
            }
        }
    }
}
