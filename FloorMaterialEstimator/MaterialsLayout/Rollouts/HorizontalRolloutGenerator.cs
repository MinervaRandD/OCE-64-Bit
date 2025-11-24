#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: HorizontalRolloutGenerator.cs. Project: MaterialsLayout. Created: 10/7/2024         */
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

 

namespace MaterialsLayout
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    using Graphics;
    using SettingsLib;
    using Globals;
    using FinishesLib;

    //using ClipperLib;

    class HorizontalRolloutGenerator
    {
        #region Key Elements

        private GraphicsLayoutArea _graphicsLayoutArea = null;
        public GraphicsLayoutArea GraphicsLayoutArea
        {
            get
            {
                if (_graphicsLayoutArea == null)
                {
                    throw new InvalidOperationException();
                }

                return _graphicsLayoutArea;
            }

            set
            {
                if (_graphicsLayoutArea == value)
                {
                    return;
                }

                _graphicsLayoutArea = value;
            }
        } 

        private FinishesLibElements _finishesLibElements = null;

        public FinishesLibElements FinishesLibElements
        {
            get
            {
                if (_finishesLibElements == null)
                {
                    throw new InvalidOperationException();
                }

                return _finishesLibElements;
            }

            set
            {
                if (value == _finishesLibElements)
                {
                    return;
                }

                _finishesLibElements = value;
            }
        }

        #endregion

        #region Constructors and Cloners

        public HorizontalRolloutGenerator(
            GraphicsWindow window
            , GraphicsPage page
            , GraphicsLayoutArea layoutArea
            , bool generateEmbeddedCuts
            , bool generateEmbededOvers)
        {
            this.window = window;

            this.page = page;

            GraphicsLayoutArea = layoutArea;

            this.generateEmbeddedCuts = generateEmbeddedCuts;
            this.generateEmbeddedOvers = generateEmbededOvers;

            this.FinishesLibElements = layoutArea.FinishLibElements;
        }

        #endregion

        private GraphicsWindow window;

        private GraphicsPage page;

        //private List<Rectangle> boundingBoxes;

        public List<Rollout> RolloutList = new List<Rollout>();

        public List<DirectedPolygon> CutList = new List<DirectedPolygon>();

        private bool generateEmbeddedCuts = false;
        private bool generateEmbeddedOvers = false;

        public void GenerateRollouts(
            double theta
            , double yBase
            , double seamWidth
            , double materialWidth
            , double materialOverlap
            , double drawingScaleInInches
            , LayoutAreaType layoutAreaType)
        {        
            CutList = new List<DirectedPolygon>();

            generateOriginalRollouts(theta, yBase, seamWidth, materialWidth, materialOverlap, layoutAreaType);

            foreach (Rollout rollout in this.RolloutList)
            {
                rollout.GenerateCutsOveragesAndUndrages(
                    window
                    , page
                    , FinishesLibElements
                    , theta
                    , GraphicsLayoutArea
                    , drawingScaleInInches
                    , generateEmbeddedCuts
                    , generateEmbeddedOvers);
            }
        }

        private void generateOriginalRollouts(
            double theta
            , double yBase
            , double seamWidth
            , double materialWidth
            , double materialOverlap
            , LayoutAreaType layoutAreaType)
        {
            Debug.Assert(seamWidth > 0);

            double maxY = GraphicsLayoutArea.MaxY;
            double minY = GraphicsLayoutArea.MinY;

            double y = yBase;

            double? minX = null;
            double? maxX = null;

            RolloutList = new List<Rollout>();


            while (y < maxY)
            {
                minX = null;
                maxX = null;

                GraphicsLayoutArea.GetMinMaxAtLevelY(y, y + seamWidth, out minX, out maxX);

                if (minX.HasValue && maxX.HasValue)
                {
                    Rectangle rolloutRectangle = new Rectangle(new Coordinate(minX.Value, y + seamWidth), new Coordinate(maxX.Value, y));

                    GraphicsRollout rollout = new GraphicsRollout(GraphicsLayoutArea, window, page)
                    {
                        FinishesLibElements = FinishesLibElements
                        , RolloutRectangle = rolloutRectangle
                        , SeamWidth = seamWidth
                        , MaterialWidth = materialWidth
                        , MaterialOverlap = materialOverlap
                        , RolloutAngle = theta

                    };

                    rollout.GraphicsRollout = rollout;

                    RolloutList.Add(rollout);
                }

                y += seamWidth;
            }


            y = yBase - seamWidth;

            if (layoutAreaType == LayoutAreaType.Normal)
            {
                while (y + seamWidth > minY) 
                {
                    minX = null;
                    maxX = null;

                    GraphicsLayoutArea.GetMinMaxAtLevelY(y, y + seamWidth, out minX, out maxX);

                    if (minX.HasValue && maxX.HasValue)
                    {
                        Rectangle rolloutRectangle = new Rectangle(new Coordinate(minX.Value, y + seamWidth), new Coordinate(maxX.Value, y));

                        GraphicsRollout rollout = new GraphicsRollout((GraphicsLayoutArea) GraphicsLayoutArea, window, page)
                            {
                                FinishesLibElements = FinishesLibElements
                                , RolloutRectangle = rolloutRectangle
                                , SeamWidth = seamWidth
                                , MaterialWidth = materialWidth
                                , MaterialOverlap = materialOverlap
                                , RolloutAngle = theta

                        };


                        rollout.GraphicsRollout = rollout;

                        RolloutList.Add(rollout);
                    }

                    y -= seamWidth;
                }
            }

            else if (layoutAreaType== LayoutAreaType.OversGenerator)
            {
                while (y > minY)
                {
                    minX = null;
                    maxX = null;

                    GraphicsLayoutArea.GetMinMaxAtLevelY(y, y + seamWidth, out minX, out maxX);

                    if (minX.HasValue && maxX.HasValue)
                    {
                        Rectangle rolloutRectangle = new Rectangle(new Coordinate(minX.Value, y + seamWidth), new Coordinate(maxX.Value, y));

                        GraphicsRollout rollout = new GraphicsRollout(GraphicsLayoutArea, window, page)
                        {
                            FinishesLibElements = FinishesLibElements
                            ,
                            RolloutRectangle = rolloutRectangle
                            ,
                            SeamWidth = seamWidth
                            ,
                            MaterialWidth = materialWidth
                            ,
                            MaterialOverlap = materialOverlap
                            ,
                            RolloutAngle = theta
                        };

                        rollout.GraphicsRollout = rollout;

                        RolloutList.Add(rollout);
                    }

                    y -= seamWidth;
                }
            }
           
        }
    }
}
