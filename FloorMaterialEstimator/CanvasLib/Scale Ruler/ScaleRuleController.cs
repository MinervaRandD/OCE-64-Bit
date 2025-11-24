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

namespace CanvasLib.Scale_Line
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;
   
    using Graphics;
    using Geometry;
    using Utilities;
    using Globals;
  
    using System.Diagnostics;
    using System.Resources;
    using System.Reflection;

    public class ScaleRuleController
    {
        private ScaleForm scaleForm;

        public Coordinate ScaleFrstCoord = Coordinate.NullCoordinate;
        public Coordinate ScaleScndCoord = Coordinate.NullCoordinate;

        public ScaleLineState ScaleLineState = ScaleLineState.NotActive;

        public ScaleLineMarker ScaleFrstMarker = null;
        public ScaleLineMarker ScaleScndMarker = null;

        public GraphicShape connectingLine = null;

        private double scaleLineMarkerWidth
        {
            get
            {
                return 0.16 / Math.Max(1, SystemState.ZoomPercent); 
            }
        }

        private double scaleLineWidth
        {
            get
            {
                return 1.25 / Math.Max(1, SystemState.ZoomPercent);
            }
        }

        private int scaleLineFontSize
        {
            get
            {
                return (int) Math.Round(24.0 / Math.Max(1, SystemState.ZoomPercent));
            }
        }


        GraphicsPage page;

        GraphicsWindow window;

        bool cancelled = false;

        public bool SeamedAreasExist { get; set; } = false;

        private Action clearAllSeams;

        private Action reseamAllAreas;

        private Action resetAreaSelectionChangedEvent;

        public ScaleRuleController(
            GraphicsWindow window
            ,GraphicsPage page
            , Action clearAllSeams
            , Action reseamAllAreas
            , Action resetAreaSelectionChangedEvent
            )
        {
            this.window = window;
            this.page = page;

            this.clearAllSeams = clearAllSeams;
            this.reseamAllAreas = reseamAllAreas;
            this.resetAreaSelectionChangedEvent = resetAreaSelectionChangedEvent;
        }

        public void InitiateSetScale(Form baseForm, bool seamedAreasExist)
        {
            this.SeamedAreasExist = seamedAreasExist;

            ScaleLineState = ScaleLineState.SetScaleInitiated;
 
            scaleForm = new ScaleForm(this);

            scaleForm.FormClosed += ScaleForm_FormClosed;
            scaleForm.btnGoBack.Click += BtnGoBack_Click;
            scaleForm.btnReset.Click += BtnReset_Click;
            scaleForm.btnCancel.Click += BtnCancel_Click;
            scaleForm.btnOK.Click += BtnOK_Click;
            scaleForm.txbFeet.TextChanged += TxbFeet_TextChanged;
            scaleForm.txbInches.TextChanged += TxbInches_TextChanged;
            scaleForm.txbLengthInFeet.TextChanged += TxbLengthInFeet_TextChanged;
            
            scaleForm.Show(baseForm);
        }


        private void ScaleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            scaleForm.FormClosed -= ScaleForm_FormClosed;
            scaleForm.btnGoBack.Click -= BtnGoBack_Click;
            scaleForm.btnReset.Click -= BtnReset_Click;
            scaleForm.btnCancel.Click -= BtnCancel_Click;
            scaleForm.btnOK.Click -= BtnOK_Click;
            scaleForm.txbFeet.TextChanged -= TxbFeet_TextChanged;
            scaleForm.txbInches.TextChanged -= TxbInches_TextChanged;
        }

        private void TxbFeet_TextChanged(object sender, EventArgs e)
        {
            setLineLength();
        }

        private void TxbInches_TextChanged(object sender, EventArgs e)
        {
            setLineLength();
        }

        private void TxbLengthInFeet_TextChanged(object sender, EventArgs e)
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
                
                int feet = (int) Math.Floor((double) totalInches.Value / 12.0);
                double inchesOnly = totalInches.Value - (double)feet * 12.0;

                string inchFormat = Utilities.FormatInchesTo2DecimalPlaces(inchesOnly);
                
                connectingLine.SetLineText(' ' + feet.ToString("#,##0") + "' " + inchFormat + "\" ");

                connectingLine.SetTextFontSize(scaleLineFontSize);

                //VisioInterop
            }
        }

        public void ScaleLineDrawingModeClick(int button, double x, double y)
        {
            if (button != 2)
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

            this.ScaleFrstMarker = new ScaleLineMarker(window, page, x, y, scaleLineMarkerWidth, scaleLineWidth);

            ScaleFrstMarker.Draw();

            ScaleFrstCoord = new Coordinate(x, y);

            window?.DeselectAll();

            SystemState.DrawingShape = true;
        }

        private void scaleLineScndPointClick(double x, double y)
        {
            Debug.Assert(ScaleLineState == ScaleLineState.FrstPointSelected);

            this.ScaleLineState = ScaleLineState.ScndPointSelected;

            this.scaleForm.SetFormForScaleLineState();

            this.ScaleScndMarker = new ScaleLineMarker(window, page, x, y, scaleLineMarkerWidth, scaleLineWidth);

            ScaleScndMarker.Draw();

            ScaleScndCoord = new Coordinate(x, y);

            drawConnectingLine();

            window?.DeselectAll();

            SystemState.DrawingShape = true;
        }

        private void drawConnectingLine()
        {
            connectingLine = page.DrawLine(this, ScaleFrstCoord.X, ScaleFrstCoord.Y, ScaleScndCoord.X, ScaleScndCoord.Y, string.Empty);

            connectingLine.SetLineColor(Color.Red);
            connectingLine.SetLineWidth(scaleLineWidth);
            //VisioInterop.SetEndpointArrows(connectingLine, 8);

            setLineLength();
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
            cancelled = true;

            CancelScaleLine();
        }

        public void CancelScaleLine()
        {
            cancelled = true;

            if (scaleForm != null)
            {
                scaleForm.Close();
                scaleForm = null;
            }

            exitSetScale();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (SeamedAreasExist && scaleForm.CkbRemoveAllSeams.Checked == false && scaleForm.CkbReseamAllAreas.Checked == false)
            {
                MessageBox.Show("There are seamed areas that will be impacted by the scale change. Please select an action in the highlighted area, or cancel the operation.");
                return;
            }

            cancelled = false;

            double? requestedLength = scaleForm.TotalInches();

            if (requestedLength is null)
            {
                ManagedMessageBox.Show("The requested scale is invalid");

                connectingLine.Delete();

                return;
            }

            double scaleLength = MathUtils.H2Distance(ScaleFrstCoord.X, ScaleFrstCoord.Y, ScaleScndCoord.X, ScaleScndCoord.Y);

            if (scaleLength <= 0.0)
            {
                ManagedMessageBox.Show("The requested scale is invalid.");

                connectingLine.Delete();

                return;
            }

            double scale = requestedLength.Value / scaleLength;

            page.DrawingScaleInInches = scale;

            exitSetScale();

            if (SeamedAreasExist && scaleForm.CkbRemoveAllSeams.Checked)
            {
                this.clearAllSeams();
            }

            else if (SeamedAreasExist && scaleForm.CkbReseamAllAreas.Checked)
            {
                this.reseamAllAreas();
            }

            this.resetAreaSelectionChangedEvent();

            scaleForm.Close();

            scaleForm = null;
        }

        public void exitSetScale()
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

            if (SetScaleCompleted != null)
            {
                SetScaleCompleted.Invoke(cancelled);
            }
     
        }

        public delegate void SetScaleCompletedHandler(bool cancelled);

        public static event SetScaleCompletedHandler SetScaleCompleted;

    }

}
