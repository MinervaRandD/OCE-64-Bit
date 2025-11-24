//-------------------------------------------------------------------------------//
// <copyright file="ScaleRulerManager.cs" company="Bruun Estimating, LLC">       // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace CanvasLib
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;
  
    using Graphics;
    using Geometry;
    using Utilities;

    using System.Diagnostics;

    public class ScaleRuleController
    {
        private ScaleForm scaleForm;

        public Coordinate ScaleFrstCoord = Coordinate.NullCoordinate;
        public Coordinate ScaleScndCoord = Coordinate.NullCoordinate;

        public ScaleLineState ScaleLineState = ScaleLineState.NotActive;

        public ScaleLineMarker ScaleFrstMarker = null;
        public ScaleLineMarker ScaleScndMarker = null;

        public Shape connectingLine = null;

        public const double scaleLineMarkerWidth = 0.1;

        public void InitiateSetScale()
        {
            ScaleLineState = ScaleLineState.SetScaleInitiated;
        
            DrawingMode = DrawingMode.ScaleLine;

            scaleForm = new ScaleForm(this);

            scaleForm.FormClosed += ScaleForm_FormClosed;
            scaleForm.btnGoBack.Click += BtnGoBack_Click;
            scaleForm.btnReset.Click += BtnReset_Click;
            scaleForm.btnCancel.Click += BtnCancel_Click;
            scaleForm.btnOK.Click += BtnOK_Click;
            scaleForm.txbFeet.TextChanged += TxbFeet_TextChanged;
            scaleForm.txbInches.TextChanged += TxbInches_TextChanged;
            
            scaleForm.Show(BaseForm);
        }

        private void TxbFeet_TextChanged(object sender, EventArgs e)
        {
            setLineLength();
        }

        private void TxbInches_TextChanged(object sender, EventArgs e)
        {
            setLineLength();
        }

        private void setLineLength()
        {
            if (connectingLine is null)
            {
                return;
            }

            double? totalInches = scaleForm.TotalInches();

            if (totalInches is null)
            {
                VisioInterop.SetLineText(connectingLine, string.Empty);
            }

            else
            {
                int inches = (int) Math.Round(totalInches.Value, 0);

                int feet = inches / 12;
                int inch = inches % 12;

                VisioInterop.SetLineText(connectingLine, ' ' + feet.ToString("#,##0") + "' " + inch.ToString("0") + "\" ");
            }
        }

        private void scaleLineDrawingModeClick(int button, double x, double y)
        {
            if (button != 1)
            {
                return;
            }

            switch (ScaleLineState)
            {
                case ScaleLineState.SetScaleInitiated:
                    scaleLineFrstPointClick(x, y);
                    return;

                case ScaleLineState.FrstPointSelected:

                    if (MathUtils.H2Distance(x, y, ScaleFrstCoord.X, ScaleFrstCoord.Y) <= scaleLineMarkerWidth + 0.01)
                    {
                        this.BtnGoBack_Click(null, null);

                        return;
                    }

                    scaleLineScndPointClick(x, y);

                    return;

                case ScaleLineState.ScndPointSelected:

                    if (MathUtils.H2Distance(x, y, ScaleScndCoord.X, ScaleScndCoord.Y) <= scaleLineMarkerWidth + 0.01)
                    {
                        this.BtnGoBack_Click(null, null);

                        return;
                    }

                    if (MathUtils.H2Distance(x, y, ScaleFrstCoord.X, ScaleFrstCoord.Y) <= scaleLineMarkerWidth + 0.01)
                    {
                        Utilities.Swap(ref ScaleFrstCoord, ref ScaleScndCoord);
                        Utilities.Swap(ref ScaleFrstMarker, ref ScaleScndMarker);

                        this.BtnGoBack_Click(null, null);

                        return;
                    }

                    return;
            }
        }

        private void BtnGoBack_Click(object sender, EventArgs e)
        {
            switch (ScaleLineState)
            {
                case ScaleLineState.FrstPointSelected:
           
                    if (!(ScaleFrstMarker is null))
                    {
                        ScaleFrstMarker.Delete();

                        ScaleFrstMarker = null;
                    }

                    ScaleFrstCoord = Coordinate.NullCoordinate;

                    ScaleLineState = ScaleLineState.SetScaleInitiated;

                    scaleForm.SetFormForScaleLineState();

                    return;

                case ScaleLineState.ScndPointSelected:

                    if (!(ScaleScndMarker is null))
                    {
                        ScaleScndMarker.Delete();

                        ScaleScndMarker = null;
                    }

                    if (!(connectingLine is null))
                    {
                        connectingLine.Delete();

                        connectingLine = null;
                    }
                    ScaleScndCoord = Coordinate.NullCoordinate;

                    ScaleLineState = ScaleLineState.FrstPointSelected;

                    scaleForm.SetFormForScaleLineState();

                    return;
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ScaleLineState = ScaleLineState.SetScaleInitiated;

            if (!(ScaleFrstMarker is null))
            {
                ScaleFrstMarker.Delete();

                ScaleFrstMarker = null;
            }

            ScaleFrstCoord = Coordinate.NullCoordinate;

            if (!(ScaleScndMarker is null))
            {
                ScaleScndMarker.Delete();

                ScaleScndMarker = null;
            }

            ScaleScndCoord = Coordinate.NullCoordinate;

            if (!(connectingLine is null))
            {
                connectingLine.Delete();

                connectingLine = null;
            }

            scaleForm.SetFormForScaleLineState();

            scaleForm.ClearMeasureBoxes();
        }

        private void scaleLineFrstPointClick(double x, double y)
        {
            Debug.Assert(ScaleLineState == ScaleLineState.SetScaleInitiated);

            this.ScaleLineState = ScaleLineState.FrstPointSelected;

            this.scaleForm.SetFormForScaleLineState();

            this.ScaleFrstMarker = new ScaleLineMarker(x, y, scaleLineMarkerWidth);

            ScaleFrstMarker.Draw(CurrentPage);

            ScaleFrstCoord = new Coordinate(x, y);
            
            VsoWindow.DeselectAll();

            DrawingShape = true;
        }

        private void scaleLineScndPointClick(double x, double y)
        {
            Debug.Assert(ScaleLineState == ScaleLineState.FrstPointSelected);

            this.ScaleLineState = ScaleLineState.ScndPointSelected;

            this.scaleForm.SetFormForScaleLineState();

            this.ScaleScndMarker = new ScaleLineMarker(x, y, scaleLineMarkerWidth);

            ScaleScndMarker.Draw(CurrentPage);

            ScaleScndCoord = new Coordinate(x, y);

            drawConnectingLine();

            //this.scaleForm.Select();
            //this.scaleForm.txbFeet.Select();
            //this.scaleForm.txbFeet.Text = "";

            VsoWindow.DeselectAll();

            DrawingShape = true;
        }

        private void drawConnectingLine()
        {
            connectingLine = CurrentPage.DrawLine(ScaleFrstCoord.X, ScaleFrstCoord.Y, ScaleScndCoord.X, ScaleScndCoord.Y);

            VisioInterop.SetBaseLineColor(connectingLine, Color.Red);
            VisioInterop.SetEndpointArrows(connectingLine, 8);

            setLineLength();
        }

        public void CancelScaleLine()
        {
            if (scaleForm != null)
            {
                scaleForm.Close();
                scaleForm = null;
            }

            exitSetScale();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            CancelScaleLine();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            double? requestedLength = scaleForm.TotalInches();

            if (requestedLength is null)
            {
                MessageBox.Show("The requested scale is invalided.");

                connectingLine.Delete();

                return;
            }

            double scaleLength = MathUtils.H2Distance(ScaleFrstCoord.X, ScaleFrstCoord.Y, ScaleScndCoord.X, ScaleScndCoord.Y);

            if (scaleLength <= 0.0)
            {
                MessageBox.Show("The requested scale is invalid.");

                connectingLine.Delete();

                return;
            }

            double scale = requestedLength.Value / scaleLength;

            CurrentPage.DrawingScaleInInches = scale;

            scaleForm.Close();
        }


        private void ScaleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BaseForm.btnSetCustomScale.Checked = false;

            if (scaleForm.DialogResult == DialogResult.Cancel)
            {
                exitSetScale();
                
                return;
            }

            scaleForm = null;

            exitSetScale();
        }

        private void exitSetScale()
        {
            if (!(ScaleFrstMarker is null))
            {
                this.ScaleFrstMarker.Delete();

                this.ScaleFrstMarker = null;
            }

            if (!(ScaleScndMarker is null))
            {
                this.ScaleScndMarker.Delete();

                this.ScaleScndMarker = null;
            }

            if (!(connectingLine is null))
            {
                this.connectingLine.Delete();

                this.connectingLine = null;
            }

            this.ScaleFrstCoord = Coordinate.NullCoordinate;
            this.ScaleScndCoord = Coordinate.NullCoordinate;

            ScaleLineState = ScaleLineState.NotActive;

            DrawingShape = false;
            DrawingMode = DrawingMode.Default;

            BaseForm.btnSetCustomScale.Checked = false;

            areaPallet.UpdateFinishStats();
            linePallet.UpdateFinishStats();
        }
    }
}
