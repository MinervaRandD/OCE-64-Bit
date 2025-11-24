#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: RemnantSpecForm.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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
using System.Text.RegularExpressions;

namespace MaterialsLayout
{
    public partial class RemnantSpecForm : Form
    {
        //private Regex regex = new Regex(@"^(?:(?!0+')\d+')$");

        public double RemnantSeamWidthInFeet;

        public RemnantSpecForm()
        {
            InitializeComponent();


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string inptWdth = this.txbSetSeamWidth.Text.Trim();

            if (!double.TryParse(inptWdth, out RemnantSeamWidthInFeet))
            {
                MessageBox.Show("A valid seam width must be specified.");
                return;
            }

            this.DialogResult = DialogResult.OK;

            this.Close();

            //if (string.IsNullOrEmpty(inptWdth))
            //{
            //    MessageBox.Show("A non-empty width must be specified.");
            //    return;
            //}

            //Match match = regex.Match(inptWdth);

            //if (!match.Success)
            //{
            //    MessageBox.Show("A valid seam width must be specified.");
            //    return;
            //}

            //string width = match.Value;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}
