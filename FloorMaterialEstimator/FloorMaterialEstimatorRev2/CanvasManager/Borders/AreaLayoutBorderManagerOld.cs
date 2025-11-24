using Geometry;
using Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator.CanvasManager
{

    public partial class CanvasManager
    {
        BorderBuildStatus BorderBuildStatus;

        CanvasBorder buildingBorder = null;

        List<CanvasBorderHandle> buildingBorderList = new List<CanvasBorderHandle>();

        CanvasBorderDirectionSelector canvasBorderDirectionSelector;

        int borderWidthInInches = 0;

        public void EnterBorderMode()
        {
            borderWidthInInches = 12 * (int)BaseForm.nudFixedWidthFeet.Value + (int)BaseForm.nudFixedWidthInches.Value;

            if (borderWidthInInches <= 0)
            {
                MessageBox.Show("Please provide a valid, non-zero, border width");

                return;
            }

            BorderBuildStatus = BorderBuildStatus.Inactive;

            buildingBorderList.Clear();

            CurrentPage.BorderDirectionHandleDict.Clear();
            CurrentPage.BorderHandleDict.Clear();

            foreach (CanvasLayoutArea canvasLayoutArea in CurrentPage.LayoutAreas)
            {
                canvasLayoutArea.AddBorderHandles();
            }

            CurrentPage.BorderHandles.ToList().ForEach(h => h.Activate());
        }

        public void ExitBorderMode()
        {
            foreach (CanvasBorderHandle borderHandle in CurrentPage.BorderHandles)
            {
                borderHandle.Remove();
            }

            if (!(canvasBorderDirectionSelector is null))
            {
                canvasBorderDirectionSelector.Remove();
            }

            BorderBuildStatus = BorderBuildStatus.Inactive;
        }

        private void processFixedWidthModeClick(double x, double y)
        {
            if (processFixedWidthModeHandleClick(x, y))
            {
                return;
            }

            if (processFixedWidthModeDirection(x, y))
            {
                return;
            }

            MessageBox.Show("Please select a green highlighted handle or direction selector");

            return;
        }

        #region Fixed Width Mode Handle Click

        private bool processFixedWidthModeHandleClick(double x, double y)
        {
            CanvasBorderHandle canvasFixedWidthHandle = CurrentPage.GetSelectedBorderHandle(x, y);

            if (canvasFixedWidthHandle is null)
            {
                return false;
            }

            if (!canvasFixedWidthHandle.IsSelectable)
            {
                return true;
            }

            canvasFixedWidthHandle.Select();

            buildingBorderList.Add(canvasFixedWidthHandle);

            foreach (CanvasBorderHandle nextCanvasFixedWidthHandle in CurrentPage.BorderHandles)
            {
                if (nextCanvasFixedWidthHandle.IsSelected)
                {
                    continue;
                }

                if (!nextCanvasFixedWidthHandle.IsAdjacentTo(canvasFixedWidthHandle))
                {
                    nextCanvasFixedWidthHandle.Deactivate();

                    continue;
                }

                nextCanvasFixedWidthHandle.Activate();
            }

            if (BorderBuildStatus == BorderBuildStatus.Inactive)
            {
                BorderBuildStatus = BorderBuildStatus.NewSequenceStarted;
            }

            else if (BorderBuildStatus == BorderBuildStatus.NewSequenceStarted)
            {
                setupDirectionSetting();

                BorderBuildStatus = BorderBuildStatus.SettingDirection;
            }

            return true;
        }

        private void setupDirectionSetting()
        {
            int count = buildingBorderList.Count;

            Debug.Assert(count >= 2);

            CanvasBorderHandle handle1 = buildingBorderList[count - 2];
            CanvasBorderHandle handle2 = buildingBorderList[count - 1];

            Coordinate coord1 = handle1.BaseCoord;
            Coordinate coord2 = handle2.BaseCoord;

            Coordinate center = new Coordinate(0.5 * (coord1.X + coord2.X), 0.5 * (coord1.Y + coord2.Y));

            double slope = 0;

            double deltaX = coord2.X - coord1.X;
            double deltaY = coord2.Y - coord1.Y;

            if (Math.Abs(deltaY) <= 0.0001)
            {
                if (coord2.X >= coord1.X)
                {
                    slope = double.PositiveInfinity;
                }

                else
                {
                    slope = double.NegativeInfinity;
                }
            }

            else
            {
                slope = deltaX / deltaY;
            }

            canvasBorderDirectionSelector = new CanvasBorderDirectionSelector(Window, CurrentPage, center, slope);

            canvasBorderDirectionSelector.Draw();

            VisioInterop.DeselectAll(Window);
        }

        #endregion

        #region Fixed Width Mode Direction Click

        CanvasBorderDirectionHandle selectedHandle = null;

        private bool processFixedWidthModeDirection(double x, double y)
        {
            CanvasBorderDirectionHandle canvasDirectionHandle = CurrentPage.GetSelectedBorderDirectionHandle(x, y);

            if (canvasDirectionHandle is null)
            {
                return false;
            }

            if (!(selectedHandle is null))
            {
                if (selectedHandle == canvasDirectionHandle)
                {
                    return true;
                }
            }

            selectedHandle = canvasDirectionHandle;

            if (canvasDirectionHandle == canvasBorderDirectionSelector.Handle1)
            {
                canvasBorderDirectionSelector.Handle1.SetSelected();
                canvasBorderDirectionSelector.Handle2.SetUnselected();
            }

            else
            {
                canvasBorderDirectionSelector.Handle2.SetSelected();
                canvasBorderDirectionSelector.Handle1.SetUnselected();
            }

            
            return true;
        }

        #endregion

        private void fixedBorders()
        {
            if (buildingBorderList.Count <= 1)
            {
                return;
            }

            for (int i = 1; i < buildingBorderList.Count; i++)
            {
                Coordinate coord1 = buildingBorderList[i - 1].BaseCoord;
                Coordinate coord2 = buildingBorderList[i].BaseCoord;

                BorderElement borderElement = new BorderElement(coord1, coord2, selectedHandle.direction);
            }
        }
    }

    public enum BorderBuildStatus
    {
        Inactive = 0,
        NewSequenceStarted = 1,
        SettingDirection = 2,
        ContinuingSequence = 3
    }
}
