

namespace CombosOld
{
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

        private SortedDictionary<string, Tuple<double, PartitionSolution, GraphicsCut[], double[,]>> solutionDictionary = new SortedDictionary<string, Tuple<double, PartitionSolution, GraphicsCut[], double[,]>>();

        public ComboSolutionGenerator(List<UCComboElement> comboElementList)
        {
            this.comboElementList = comboElementList;
        }

        public List<Tuple<PartitionSolution, GraphicsCut[], double[,]>> GenerateSolution()
        {
            generateSolution(0);

            List<Tuple<PartitionSolution, GraphicsCut[], double[,]>> rtrnList = new List<Tuple<PartitionSolution, GraphicsCut[], double[,]>>();

            for (int i = solutionDictionary.Count - 1; i >= 0; i--)
            {
                Tuple<double, PartitionSolution, GraphicsCut[], double[,]> solution = solutionDictionary.Values.ElementAt(i);

                rtrnList.Add(new Tuple<PartitionSolution, GraphicsCut[], double[,]>(solution.Item2, solution.Item3, solution.Item4));
            }
            
            return rtrnList;
        }

        List<GraphicsCut> cutList = new List<GraphicsCut>();

        private void generateSolution(int level)
        {
            if (level >= comboElementList.Count)
            {
                generateSolutionByCase();

                return;
            }

            GraphicsCut cut = comboElementList[level].Cut.Clone();

            cut.IsRotated = false;

            orientCut(cut);

            cutList.Add(cut);

            generateSolution(level + 1);

            cutList.RemoveAt(level);

            if (comboElementList[level].lblFlipped.Text != "X")
            {
                return;
            }

            cut = comboElementList[level].Cut.Clone();

            cut.IsRotated = true;

            orientCut(cut);

            cutList.Add(cut);

            generateSolution(level + 1);

            cutList.RemoveAt(level);
        }

        private void orientCut(GraphicsCut cut)
        {
            if (cut.IsRotated)
            {
                cut.Rotate(-cut.CutAngle);
            }

            else
            {
                cut.Rotate(-cut.CutAngle + Math.PI);
            }

            double minX = cut.CutPolygon.MinX;
            double minY = cut.CutPolygon.MinY;

            cut.Translate(new Coordinate(-minX, -minY));
        }

        private void generateSolutionByCase()
        {
            double[,] wghtMtrx = new double[cutList.Count, cutList.Count];

            for (int i1 = 0; i1 < cutList.Count; i1++)
            {
                for (int i2 = 0; i2 < cutList.Count; i2++)
                {
                    if (i1 == i2)
                    {
                        wghtMtrx[i1, i2] = 0.0;
                    }

                    else
                    {
                        PolygonDistanceGenerator polygonDistanceGenerator
                            = new PolygonDistanceGenerator((DirectedPolygon)cutList[i1].CutPolygon, (DirectedPolygon)cutList[i2].CutPolygon);

                        wghtMtrx[i1, i2] = polygonDistanceGenerator.GenPolyDistance();
                    }
                }
            }

            CombosSelectionGenerator comboSelectionGenerator = new CombosSelectionGenerator(wghtMtrx);

            List<PartitionSolution> solutionList = comboSelectionGenerator.GenerateSelections();

            foreach (PartitionSolution solution in solutionList)
            {
                string key = solution.Key();

                if (!solutionDictionary.ContainsKey(key))
                {
                    solutionDictionary[key] = new Tuple<double, PartitionSolution, GraphicsCut[], double[,]>(solution.SolutionValue, solution, cutList.ToArray(), wghtMtrx);
                }

                else if (solutionDictionary[key].Item1 < solution.SolutionValue)
                {
                    solutionDictionary[key] = new Tuple<double, PartitionSolution, GraphicsCut[], double[,]>(solution.SolutionValue, solution, cutList.ToArray(), wghtMtrx);
                }
            }
        }
    }
}
