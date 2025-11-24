//-------------------------------------------------------------------------------//
// <copyright file="Page.cs" company="Bruun Estimating, LLC">                    // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

namespace CanvasLib.Counters
{
    using System;
    using System.Drawing;
    using Geometry;
    using Graphics;
    using SettingsLib;

    public class GraphicsCounter : GraphicsCircle
    {
        public char CounterTag => counter.Tag;
        public Color CounterColor => counter.Color;
        public int CounterIndex => (int) (CounterTag - 'A');

        public CounterDisplaySize CounterSize => counter.CounterDisplaySize;
        public bool ShowCircle => counter.ShowCircle;

        private Counter counter;

        private double radius
        {
            get
            {
                switch (CounterSize)
                {
                    case CounterDisplaySize.Small: return GlobalSettings.CounterSmallCircleRadius;
                    case CounterDisplaySize.Medium: return GlobalSettings.CounterMediumCircleRadius;
                    case CounterDisplaySize.Large: return GlobalSettings.CounterLargeCircleRadius;
                }

                return GlobalSettings.CounterMediumCircleRadius;
            }
        }

        private double fontSizeInPts
        {
            get
            {
                switch (CounterSize)
                {
                    case CounterDisplaySize.Small: return GlobalSettings.CounterSmallFontInPts;
                    case CounterDisplaySize.Medium: return GlobalSettings.CounterMediumFontInPts;
                    case CounterDisplaySize.Large: return GlobalSettings.CounterLargeFontInPts;
                }

                return GlobalSettings.CounterMediumFontInPts;
            }
        }

        public GraphicsCounter(GraphicsWindow window, GraphicsPage page, Coordinate center, Counter counter) : base(window, page, center, GlobalSettings.CounterMediumCircleRadius)
        {
            this.counter = counter;

            //CounterSize = counterSize;
            //CounterTag = counterTag;
            //CounterColor = counterColor;

            base.Radius = radius;
        }

        public GraphicsCounter(GraphicsPage page, Coordinate center, Counter counter, string guid) : base(page, center, GlobalSettings.CounterMediumCircleRadius, guid)
        {
            this.counter = counter;

            //CounterSize = counterSize;
            //CounterTag = counterTag;
            //CounterColor = counterColor;

            base.Radius = radius;
        }

        public void Draw()
        {
            Shape = Page.DrawCircle(Guid, Center, radius, CounterColor);

            base.Guid = Shape.Guid;

            if (this.ShowCircle)
            {
                Shape.ShowShapeOutline(true);
            }

            else
            {
                Shape.ShowShapeOutline(false);
            }

            Shape.VisioShape.Data1 = "[Counter]";

            Shape.SetShapeText(CounterTag.ToString(), CounterColor, fontSizeInPts);
            Shape.SetFill("0");
            Shape.SetLineWidth(1.5);
        }

        public void UpdateColor(Color color)
        {
            //CounterColor = color;

            Shape.SetShapeText(CounterTag.ToString(), CounterColor, fontSizeInPts);
            Shape.SetLineColor(CounterColor);
        }

        public void UpdateSize(CounterDisplaySize counterSize)
        {
            //CounterSize = counterSize;

            Delete();

            Draw();
        }

        public void UpdateShowCircle(bool showCircle)
        {
            //this.ShowCircle = showCircle;

            if (this.ShowCircle)
            {
                Shape.ShowShapeOutline(true);
            }

            else
            {
                Shape.ShowShapeOutline(false);
            }

        }

        internal void Refresh()
        {

            Delete();

            Draw();
        }

        internal void UpdateText()
        {
            Shape.SetShapeText(CounterTag.ToString(), CounterColor, fontSizeInPts);
        }

        public void Delete()
        {
            VisioInterop.DeleteShape(this.Shape);
        }

    }
}