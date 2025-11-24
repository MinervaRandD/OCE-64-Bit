using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace FloorMaterialEstimator.OversUndersForm
{
    public static class OversUndersCommon
    {

        

        public static bool HandleTextBoxChange(
            TextBox wdthTextBox
            ,TextBox lnthTextBox
            ,int OrigWdthFeet
            ,int OrigWdthInch
            ,int OrigLnthFeet
            ,int OrigLnthInch
            , out int? wdthFeet
            , out int? wdthInch
            , out int? lnthFeet
            , out int? lnthInch)
        {
            lnthFeet = null;
            lnthInch = null;

            wdthFeet = null;
            wdthInch = null;

            if (Utilities.Utilities.IsNotNull(wdthTextBox))
            {
                if (!Utilities.Utilities.CheckTextBoxValidMeasurement(wdthTextBox, out wdthFeet, out wdthInch))
                {
                    wdthTextBox.BackColor = Color.Pink;
                }

                else if (wdthFeet.Value < 1)
                {
                    wdthTextBox.BackColor = Color.Pink;
                }

                else if (wdthFeet.Value == OrigWdthFeet && wdthInch.Value == OrigWdthInch)
                {
                    wdthTextBox.BackColor = SystemColors.ControlLightLight;
                }

                else
                {
                    wdthTextBox.BackColor = Color.Yellow;
                }

            }

            if (!Utilities.Utilities.CheckTextBoxValidMeasurement(lnthTextBox, out lnthFeet, out lnthInch))
            {
                lnthTextBox.BackColor = Color.Pink;
            }

            else if (lnthFeet.Value < 1)
            {
                lnthTextBox.BackColor = Color.Pink;
            }

            else if (lnthFeet.Value == OrigLnthFeet && lnthInch.Value == OrigLnthInch)
            {
                lnthTextBox.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                lnthTextBox.BackColor = Color.Yellow;
            }

           
            return wdthTextBox.BackColor != Color.Pink && lnthTextBox.BackColor != Color.Pink;
        }

        public static void FormElementSetup(Label lblNmbr, Label lblWdth, Label lblX, Control txbLnth, Control txbRepeat = null, int yPosn = 0)
        {
            int posn = 1;

            lblNmbr.Width = 32;
            lblNmbr.Location = new Point(posn, yPosn);

            posn += lblNmbr.Width - 1;

            lblWdth.Width = 50;
            lblWdth.Location = new Point(posn, yPosn);

            posn += lblWdth.Width - 1;

            lblX.Width = 20;
            lblX.Location = new Point(posn, yPosn);

            posn += lblX.Width - 1;

            txbLnth.Width = 52;
            txbLnth.Location = new Point(posn, yPosn);

            if (txbRepeat != null)
            {
                posn += txbLnth.Width - 1;
                txbRepeat.Width = 32;
                txbRepeat.Location = new Point(posn, yPosn);
            }
        }

        public static void Form2ElementSetup(Control control, Label lblNmbr, Label lblWdth, Label lblX, Control lblLnth, Control txbRepeat = null, int yPosn = 0)
        {
            control.Width = 200;

            int posn = 1;

            lblNmbr.Width = 48;
            lblNmbr.Location = new Point(posn, yPosn);
            lblNmbr.TextAlign = ContentAlignment.MiddleLeft;

            posn += lblNmbr.Width - 1;

            lblWdth.Width = 50;
            lblWdth.Location = new Point(posn, yPosn);
            lblWdth.TextAlign = ContentAlignment.MiddleCenter;

            posn += lblWdth.Width - 1;

            lblX.Width = 20;
            lblX.Location = new Point(posn, yPosn);
            lblX.TextAlign = ContentAlignment.MiddleCenter;

            posn += lblX.Width - 1;

            lblLnth.Width = 54;
            lblLnth.Location = new Point(posn, yPosn);
            //lblLnth.TextAlign = ContentAlignment.MiddleCenter;

            //if (txbRepeat != null)
            //{
            //    posn += lblLnth.Width - 1;
            //    txbRepeat.Width = 32;
            //    txbRepeat.Location = new Point(posn, yPosn);
            //}
        }
    }
}
