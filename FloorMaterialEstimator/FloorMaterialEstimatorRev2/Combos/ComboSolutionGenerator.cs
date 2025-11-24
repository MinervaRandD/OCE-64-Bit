
//-------------------------------------------------------------------------------//
// <copyright file="ComboSolutionGenerator.cs" company="Bruun Estimating, LLC">  // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

// Important programming notes:
//
// 1. A 'partition' is an array of integers. The value of partition[i] is the number of elements expected in the path
//    that have i+1 elements. For example, if partition[1] = 3, this means that there will be 3 paths of length 2 in the
//    partion.

namespace FloorMaterialEstimator
{
    using ComboLib;
    using FloorMaterialEstimator.CanvasManager;
    using Geometry;
    using MaterialsLayout;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Utilities;

    public class ComboSolutionGenerator
    {
        List<UCComboElement> comboElementList;

        private SortedDictionary<string, Tuple<double, PartitionSolution, GraphicsComboElem[], double[,]>> solutionDictionary = new SortedDictionary<string, Tuple<double, PartitionSolution, GraphicsComboElem[], double[,]>>();

        public ComboSolutionGenerator(List<UCComboElement> comboElementList)
        {
            this.comboElementList = comboElementList;
        }

        public List<Tuple<PartitionSolution, GraphicsComboElem[], double[,]>> GenerateSolution()
        {
            generateSolution(0);

            List<Tuple<PartitionSolution, GraphicsComboElem[], double[,]>> rtrnList = new List<Tuple<PartitionSolution, GraphicsComboElem[], double[,]>>();

            for (int i = solutionDictionary.Count - 1; i >= 0; i--)
            {
                Tuple<double, PartitionSolution, GraphicsComboElem[], double[,]> solution = solutionDictionary.Values.ElementAt(i);

                rtrnList.Add(new Tuple<PartitionSolution, GraphicsComboElem[], double[,]>(solution.Item2, solution.Item3, solution.Item4));
            }
            
            return rtrnList;
        }

        List<GraphicsComboElem> comboElemList = new List<GraphicsComboElem>();

        private void generateSolution(int level)
        {
            if (level >= comboElementList.Count)
            {
                generateSolutionByCase();

                return;
            }

            if (string.IsNullOrEmpty(comboElementList[level].lblFlipped.Text))
            {
                // No flipping (rotating) permitted

                 generateSolutionUnflippedCase(level);

                return;
            }
           
            else if (comboElementList[level].lblFlipped.Text == "O")
            {
                // Flipping (rotating) is optional

                generateSolutionUnflippedCase(level);
                generateSolutionFlippedCase(level);

                return;
            }

            else if (comboElementList[level].lblFlipped.Text == "M")
            {
                // Flipping (rotating) is mandatory
                generateSolutionFlippedCase(level);

                return;
            }

            else
            {
                throw new NotImplementedException();
            }
        }

        private void generateSolutionUnflippedCase(int level)
        {
            GraphicsComboElem graphicsComboElem = comboElementList[level].GraphicsComboElem.Clone();

            graphicsComboElem.IsRotated = false;

            orientGraphicsComboElem(graphicsComboElem);

            comboElemList.Add(graphicsComboElem);

            generateSolution(level + 1);

            comboElemList.RemoveAt(level);
        }

        private void generateSolutionFlippedCase(int level)
        {

            GraphicsComboElem graphicsComboElem = comboElementList[level].GraphicsComboElem.Clone();

            graphicsComboElem.IsRotated = true;

            orientGraphicsComboElem(graphicsComboElem);

            comboElemList.Add(graphicsComboElem);

            generateSolution(level + 1);

            comboElemList.RemoveAt(level);
        }

        private void orientGraphicsComboElem(GraphicsComboElem graphicsComboElem)
        {
            if (graphicsComboElem.IsRotated)
            {
                graphicsComboElem.Rotate(Math.PI);
            }

            else
            {
                //graphicsComboElem.Rotate(graphicsComboElem.CutAngle);
            }

            //graphicsComboElem.Rotate(-graphicsComboElem.CutAngle);

            double minX = graphicsComboElem.GraphicsDirectedPolygon.MinX;
            double minY = graphicsComboElem.GraphicsDirectedPolygon.MinY;

            graphicsComboElem.Translate(new Coordinate(-minX, -minY));
        }

        private void generateSolutionByCase()
        {
            double[,] wghtMtrx = new double[comboElemList.Count, comboElemList.Count];

            for (int i1 = 0; i1 < comboElemList.Count; i1++)
            {
                for (int i2 = 0; i2 < comboElemList.Count; i2++)
                {
                    if (i1 == i2)
                    {
                        wghtMtrx[i1, i2] = 0.0;
                    }

                    else
                    {
                        PolygonDistanceGenerator polygonDistanceGenerator
                            = new PolygonDistanceGenerator((DirectedPolygon)comboElemList[i1].GraphicsDirectedPolygon, (DirectedPolygon)comboElemList[i2].GraphicsDirectedPolygon);

                        wghtMtrx[i1, i2] = polygonDistanceGenerator.GenPolyDistance();
                    }
                }
            }

            CombosSelectionGenerator comboSelectionGenerator = new CombosSelectionGenerator(wghtMtrx);

            //comboSelectionGenerator.GenerateSelections();

            List<PartitionSolution> solutionList = comboSelectionGenerator.GenerateSelections();

            foreach (PartitionSolution solution in solutionList)
            {
                string key = solution.Key();

      
                if (!solutionDictionary.ContainsKey(key))
                {
                    solutionDictionary[key] = new Tuple<double, PartitionSolution, GraphicsComboElem[], double[,]>(solution.SolutionValue, solution, comboElemList.ToArray(), wghtMtrx);
                }

                else if (solutionDictionary[key].Item1 < solution.SolutionValue)
                {
                    solutionDictionary[key] = new Tuple<double, PartitionSolution, GraphicsComboElem[], double[,]>(solution.SolutionValue, solution, comboElemList.ToArray(), wghtMtrx);
                }
            }
        }
    }
}
