#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: SubdivisionGenerator.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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

using FinishesLib;
using Geometry;
using Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialsLayout.Subdivision
{
    public class SubdivisionGenerator
    {
        private LayoutArea layoutArea;

        private DirectedPolygon subdivisionArea;

        public AreaFinishLayers AreaFinishLayers { get; set; } = null;

        public SubdivisionGenerator(LayoutArea layoutArea, DirectedPolygon subdivisionArea, AreaFinishLayers areaFinishLayers)
        {
            this.layoutArea = layoutArea;

            this.subdivisionArea = subdivisionArea;

            this.AreaFinishLayers = areaFinishLayers;
        }

        public List<LayoutArea> GenerateSubdivision()
        {
            // Such a hard problem that we address it in steps just to be sure that basic cases are covered.

            //return GenerateSubdivisionGeneral();
            //if (layoutArea.Contains(subdivisionArea))
            //{
            //    return generateSubdivisionSimpleCase(layoutArea, subdivisionArea);
            //}

            List<LayoutArea> rtrnList;

            if (layoutArea.ExternalArea.Contains(subdivisionArea))
            {
                rtrnList = generateSubdivisionInternal(layoutArea, subdivisionArea);
            }

            else
            {
                rtrnList = generateSubdivisionExternal(layoutArea, subdivisionArea);
            }

            return rtrnList;
        }

        //private List<LayoutArea> generateSubdivisionSimpleCase(LayoutArea layoutAreaParm, DirectedPolygon subdivisionAreaParm)
        //{
        //    LayoutArea result1 = new LayoutArea(layoutAreaParm.ExternalArea, layoutAreaParm.InternalAreas);

        //    result1.InternalAreasAdd(subdivisionAreaParm);

        //    LayoutArea result2 = new LayoutArea(subdivisionAreaParm);

        //    return new List<LayoutArea>() { result1, result2 };
        //}

        public List<LayoutArea> generateSubdivisionInternal(LayoutArea layoutAreaParm, DirectedPolygon subdivisionAreaParm)
        {
            if (!layoutAreaParm.ExternalArea.Contains(subdivisionAreaParm))
            {
                return new List<LayoutArea>();
            }

            HashSet<DirectedPolygon> nonadjctAreas = new HashSet<DirectedPolygon>(layoutAreaParm.InternalAreas);

            HashSet<DirectedPolygon> adjacentAreas = new HashSet<DirectedPolygon>();

            foreach (DirectedPolygon dp in nonadjctAreas.ToList())
            {
                if (subdivisionAreaParm.Intersects(dp))
                {
                    adjacentAreas.Add(dp);
                    nonadjctAreas.Remove(dp);
                }
            }

            //if (adjacentAreas.Count <= 0)
            //{
            //    if (layoutAreaParm.Contains(subdivisionAreaParm))
            //    {
            //        return generateSubdivisionSimpleCase(layoutAreaParm, subdivisionAreaParm);
            //    }
            //}

            List<DirectedPolygon> subtrahendList = new List<DirectedPolygon>();

            subtrahendList.Add(subdivisionAreaParm);

            subtrahendList.AddRange(adjacentAreas);

            // Note: for this to work the subdivison area must be added first
            //
            // Note that there is an assumption that the takeout areas (internal areas) do not overlap for this to work.

            List<LayoutArea> results1 = layoutAreaParm.Subtract1(subtrahendList);

            foreach (LayoutArea la in results1)
            {
                foreach (DirectedPolygon dp in nonadjctAreas.ToList())
                {
                    if (la.ExternalArea.Contains(dp))
                    {
                        la.InternalAreasAdd(dp);
                    }
                }
            }

            LayoutArea subdivisionLayoutArea = new LayoutArea(subdivisionAreaParm, AreaFinishLayers);

            List<LayoutArea> results2 = subdivisionLayoutArea.Subtract2(adjacentAreas.ToList());

            List<LayoutArea> rtrnList = new List<LayoutArea>();

            rtrnList.AddRange(results1);
            rtrnList.AddRange(results2);

            return rtrnList;
        }

        public List<LayoutArea> generateSubdivisionExternal(LayoutArea layoutAreaParm, DirectedPolygon subdivisionAreaParm)
        {
            List<DirectedPolygon> subdivisionAreaList = layoutAreaParm.ExternalArea.Intersect(subdivisionAreaParm);

            HashSet<LayoutArea> layoutAreaSet = new HashSet<LayoutArea>() { layoutAreaParm };

            foreach (DirectedPolygon subdivisionPoly in subdivisionAreaList)
            {
                foreach (LayoutArea layoutArea1 in layoutAreaSet.ToList())
                {
                    List<LayoutArea> layoutAreaList = generateSubdivisionInternal(layoutArea1, subdivisionPoly);

                    if (layoutAreaList.Count > 0)
                    {
                        layoutAreaSet.Remove(layoutArea1);
                        foreach (LayoutArea la in layoutAreaList)
                        {
                            layoutAreaSet.Add(la);
                        }
                    }
                }
            }

            return layoutAreaSet.ToList();
        }

        //public List<LayoutArea> GenerateSubdivisionGeneral()
        //{
        //    List<LayoutArea> finalResults = new List<LayoutArea>() { layoutArea } ;

        //    List<DirectedPolygon> subdivisionList = layoutArea.ExternalArea.Intersect(subdivisionArea);

        //    // Important to note that there may be more than one external area resulting from this intersection, and that 
        //    // the processing must be sequential

        //    foreach (DirectedPolygon subdivisionAreaElement in subdivisionList)
        //    {
        //        for (int i = finalResults.Count - 1; i >= 0; i--)
        //        {
        //            LayoutArea layoutAreaElement = finalResults[i];

        //            if (!layoutAreaElement.Intersects(subdivisionAreaElement))
        //            {
        //                continue;
        //            }

        //            finalResults.RemoveAt(i);

        //            List<LayoutArea> results1 = layoutAreaElement.Subtract(subdivisionAreaElement);

        //            finalResults.AddRange(results1);

        //            LayoutArea subdivisionLayoutArea = new LayoutArea(subdivisionAreaElement);

        //            List<LayoutArea> results2 = subdivisionLayoutArea.Subtract(layoutAreaElement.InternalAreas);

        //            finalResults.AddRange(results2);
        //        }
        //    }

        //    return finalResults;
        //}

    }
}
