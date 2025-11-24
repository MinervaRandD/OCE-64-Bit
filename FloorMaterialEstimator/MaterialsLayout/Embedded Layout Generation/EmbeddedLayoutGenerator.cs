#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: EmbeddedLayoutGenerator.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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


namespace MaterialsLayout.Embedded_Layout_Generation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FinishesLib;
    using Geometry;
    using Utilities;

    public class EmbeddedLayoutGenerator
    {
        private DirectedPolygon externalPolygon;

        private List<LayoutArea> layoutAreaList;

        public AreaFinishLayers AreaFinishLayers { get; set; } = null;

        public EmbeddedLayoutGenerator(DirectedPolygon externalPolygon, List<LayoutArea> layoutAreaList, AreaFinishLayers areaFinishLayers)
        {
            this.externalPolygon = externalPolygon;
            this.layoutAreaList = new List<LayoutArea>(layoutAreaList);
            this.AreaFinishLayers = areaFinishLayers;
        }

        public List<LayoutArea> GenerateEmbeddedLayoutAreas()
        {
            // Eliminate all layout areas that do not intersect with the polygon to embed.

            for (int i = layoutAreaList.Count - 1; i>= 0; i--)
            {
                LayoutArea layoutArea = layoutAreaList[i];

                if (!layoutArea.Intersects(externalPolygon))
                {
                    layoutAreaList.RemoveAt(i);
                }
            }

            if (layoutAreaList.Count <= 0)
            {
                // No overlapping layout areas. Return original polygon as a layout area.

                LayoutArea layoutArea = new LayoutArea(externalPolygon, AreaFinishLayers);

                return new List<LayoutArea>() { layoutArea };
            }

            // Generate list of interior regions, to be used later.

            List<DirectedPolygon> interiorRegionList = new List<DirectedPolygon>();

            // Don't filll internal areas
            // layoutAreaList.ForEach(l => interiorRegionList.AddRange(l.InternalAreas));

            List<LayoutArea> rtrnList = new List<LayoutArea>();

            // Generate a list of all layout areas that are contained within the directed polygon and remove 
            // them from the layout list.

            List<DirectedPolygon> containedLayoutAreas = new List<DirectedPolygon>();

            for (int i = layoutAreaList.Count - 1; i >= 0; i--)
            {
                LayoutArea layoutArea = layoutAreaList[i];

                if (externalPolygon.Contains(layoutArea.ExternalArea))
                {
                    containedLayoutAreas.Add(layoutArea.ExternalArea);
                    layoutAreaList.RemoveAt(i);
                }
            }

            List<DirectedPolygon> inptList = new List<DirectedPolygon>();
            List<DirectedPolygon> outpList = new List<DirectedPolygon>();
            
            inptList.Clear();
      
            inptList.Add(externalPolygon);

            for (int i = 0; i < layoutAreaList.Count; i++)
            {
                outpList.Clear();

                foreach (DirectedPolygon dp in inptList)
                {
                    List<DirectedPolygon> results = dp.Subtract(layoutAreaList[i].ExternalArea);
                    outpList.AddRange(results);
                }

                inptList.Clear();

                inptList.AddRange(outpList);
            }

            if (outpList.Count <= 0)
            {
                outpList.Add(externalPolygon);
            }

            foreach (DirectedPolygon dp in outpList)
            {
                LayoutArea la = new LayoutArea(dp, AreaFinishLayers);

                foreach (DirectedPolygon cdp in containedLayoutAreas)
                {
                    if (dp.Contains(cdp))
                    {
                        la.InternalAreasAdd(cdp);
                    }
                }

                rtrnList.Add(la);
            }

            // Generate additional elements as the intersection of the embedded items and the interior regions
            // of the origianl layout list.

            foreach (DirectedPolygon dpi in interiorRegionList)
            {
                List<DirectedPolygon> results = externalPolygon.Intersect(dpi);

                rtrnList.AddRange(results.Select(r => new LayoutArea(r, AreaFinishLayers)));
            }


            return rtrnList;
        }
    }
}
