

namespace TestDriverLayoutAreaEmbeddedLayoutOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Graphics;
    using Geometry;
    using MaterialsLayout;

    using Visio = Microsoft.Office.Interop.Visio;
    public class TestCaseGenerator
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public GraphicsPage page { get; set; }

        public GraphicsWindow window { get; set; }


        public TestCaseGenerator(Visio.Document vsoDocument, Visio.Window vsoWindow, Visio.Page vsoPage)
        {
            this.VsoDocument = vsoDocument;
            this.VsoWindow = VsoWindow;
            this.VsoPage = vsoPage;

            window = new GraphicsWindow(this.VsoWindow);

            page = new GraphicsPage(window, VsoPage);


        }

        public GraphicsLayoutArea BaseCase1()
        {
            Coordinate coord1 = new Coordinate(2, 2);
            Coordinate coord2 = new Coordinate(2, 10);
            Coordinate coord3 = new Coordinate(10, 10);
            Coordinate coord4 = new Coordinate(10, 2);

            GraphicsDirectedLine l1 = new GraphicsDirectedLine(window, page, new DirectedLine(coord1, coord2), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l2 = new GraphicsDirectedLine(window, page, new DirectedLine(coord2, coord3), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l3 = new GraphicsDirectedLine(window, page, new DirectedLine(coord3, coord4), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l4 = new GraphicsDirectedLine(window, page, new DirectedLine(coord4, coord1), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon ea = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l1, l2, l3, l4 });

            Coordinate coord5 = new Coordinate(4, 4);
            Coordinate coord6 = new Coordinate(4, 8);
            Coordinate coord7 = new Coordinate(8, 8);
            Coordinate coord8 = new Coordinate(8, 4);

            GraphicsDirectedLine l5 = new GraphicsDirectedLine(window, page, new DirectedLine(coord5, coord6), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l6 = new GraphicsDirectedLine(window, page, new DirectedLine(coord6, coord7), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l7 = new GraphicsDirectedLine(window, page, new DirectedLine(coord7, coord8), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l8 = new GraphicsDirectedLine(window, page, new DirectedLine(coord8, coord5), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon ia = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l5, l6, l7, l8 });

            GraphicsLayoutArea gla = new GraphicsLayoutArea(window, page, ea, new List<GraphicsDirectedPolygon>() { ia });

            return gla;
        }

        public GraphicsLayoutArea BaseCase2()
        {
            Coordinate coord1 = new Coordinate(2, 2);
            Coordinate coord2 = new Coordinate(2, 10);
            Coordinate coord3 = new Coordinate(10, 10);
            Coordinate coord4 = new Coordinate(10, 2);

            GraphicsDirectedLine l1 = new GraphicsDirectedLine(window, page, new DirectedLine(coord1, coord2), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l2 = new GraphicsDirectedLine(window, page, new DirectedLine(coord2, coord3), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l3 = new GraphicsDirectedLine(window, page, new DirectedLine(coord3, coord4), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l4 = new GraphicsDirectedLine(window, page, new DirectedLine(coord4, coord1), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon ea = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l1, l2, l3, l4 });

            Coordinate coord5 = new Coordinate(4, 4);
            Coordinate coord6 = new Coordinate(4, 5);
            Coordinate coord7 = new Coordinate(8, 5);
            Coordinate coord8 = new Coordinate(8, 4);

            GraphicsDirectedLine l5 = new GraphicsDirectedLine(window, page, new DirectedLine(coord5, coord6), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l6 = new GraphicsDirectedLine(window, page, new DirectedLine(coord6, coord7), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l7 = new GraphicsDirectedLine(window, page, new DirectedLine(coord7, coord8), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l8 = new GraphicsDirectedLine(window, page, new DirectedLine(coord8, coord5), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon ia1 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l5, l6, l7, l8 });

            Coordinate coord9 = new Coordinate(4, 7);
            Coordinate coord10 = new Coordinate(4, 8);
            Coordinate coord11 = new Coordinate(8, 8);
            Coordinate coord12 = new Coordinate(8, 7);

            GraphicsDirectedLine l9 = new GraphicsDirectedLine(window, page, new DirectedLine(coord9, coord10), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l10 = new GraphicsDirectedLine(window, page, new DirectedLine(coord10, coord11), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l11 = new GraphicsDirectedLine(window, page, new DirectedLine(coord11, coord12), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l12 = new GraphicsDirectedLine(window, page, new DirectedLine(coord12, coord9), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon ia2 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l9, l10, l11, l12 });


            GraphicsLayoutArea gla = new GraphicsLayoutArea(window, page, ea, new List<GraphicsDirectedPolygon>() { ia1, ia2 });

            return gla;
        }

        public GraphicsLayoutArea BaseCase3()
        {
            Coordinate coord1 = new Coordinate(2, 2);
            Coordinate coord2 = new Coordinate(2, 10);
            Coordinate coord3 = new Coordinate(10, 10);
            Coordinate coord4 = new Coordinate(10, 2);

            GraphicsDirectedLine l1 = new GraphicsDirectedLine(window, page, new DirectedLine(coord1, coord2), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l2 = new GraphicsDirectedLine(window, page, new DirectedLine(coord2, coord3), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l3 = new GraphicsDirectedLine(window, page, new DirectedLine(coord3, coord4), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l4 = new GraphicsDirectedLine(window, page, new DirectedLine(coord4, coord1), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon ea = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l1, l2, l3, l4 });

            Coordinate coord5 = new Coordinate(4, 4);
            Coordinate coord6 = new Coordinate(4, 5);
            Coordinate coord7 = new Coordinate(7, 5);
            Coordinate coord8 = new Coordinate(7, 8);
            Coordinate coord9 = new Coordinate(8, 8);
            Coordinate coord10 = new Coordinate(8, 4);

            GraphicsDirectedLine l5 = new GraphicsDirectedLine(window, page, new DirectedLine(coord5, coord6), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l6 = new GraphicsDirectedLine(window, page, new DirectedLine(coord6, coord7), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l7 = new GraphicsDirectedLine(window, page, new DirectedLine(coord7, coord8), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l8 = new GraphicsDirectedLine(window, page, new DirectedLine(coord8, coord9), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l9 = new GraphicsDirectedLine(window, page, new DirectedLine(coord9, coord10), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l10 = new GraphicsDirectedLine(window, page, new DirectedLine(coord10, coord5), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon ia1 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l5, l6, l7, l8, l9, l10 });

            GraphicsLayoutArea gla = new GraphicsLayoutArea(window, page, ea, new List<GraphicsDirectedPolygon>() { ia1 });

            return gla;
        }

        public GraphicsLayoutArea BaseCase4()
        {
            List<Coordinate> eaCoordList = new List<Coordinate>()
            {
                new Coordinate(1, 1),
                new Coordinate(1, 13),
                new Coordinate(9, 13),
                new Coordinate(9, 11),
                new Coordinate(3, 11),
                new Coordinate(3, 9),
                new Coordinate(9, 9),
                new Coordinate(9, 6),
                new Coordinate(3, 6),
                new Coordinate(3, 4),
                new Coordinate(9, 4),
                new Coordinate(9, 1)
            };


            GraphicsDirectedPolygon ea = new GraphicsDirectedPolygon(window, page, eaCoordList);

            List<Coordinate> ia1CoordList = new List<Coordinate>()
            {
                new Coordinate(4, 7),
                new Coordinate(8, 7),
                new Coordinate(8, 8),
                new Coordinate(4, 8),
            };

            GraphicsDirectedPolygon ia1 = new GraphicsDirectedPolygon(window, page, ia1CoordList);

            List<Coordinate> ia2CoordList = new List<Coordinate>()
            {
                new Coordinate(2, 2),
                new Coordinate(8, 2),
                new Coordinate(8, 3),
                new Coordinate(2, 3),
            };

            GraphicsDirectedPolygon ia2 = new GraphicsDirectedPolygon(window, page, ia2CoordList);

            GraphicsLayoutArea gla = new GraphicsLayoutArea(window, page, ea, new List<GraphicsDirectedPolygon>() { ia1, ia2 } );

            return gla;
        }

        public GraphicsDirectedPolygon SubdividePolygon1()
        {
            Coordinate coord1 = new Coordinate(5, 2);
            Coordinate coord2 = new Coordinate(5, 6);
            Coordinate coord3 = new Coordinate(7, 6);
            Coordinate coord4 = new Coordinate(7, 2);

            return genSubdividePolygon(coord1, coord2, coord3, coord4);
        }


        public GraphicsDirectedPolygon SubdividePolygon2()
        {
            Coordinate coord1 = new Coordinate(5, 3);
            Coordinate coord2 = new Coordinate(5, 6);
            Coordinate coord3 = new Coordinate(7, 6);
            Coordinate coord4 = new Coordinate(7, 3);

            return genSubdividePolygon(coord1, coord2, coord3, coord4);
        }

        public GraphicsDirectedPolygon SubdividePolygon3()
        {
            Coordinate coord1 = new Coordinate(5, 3);
            Coordinate coord2 = new Coordinate(5, 9);
            Coordinate coord3 = new Coordinate(7, 9);
            Coordinate coord4 = new Coordinate(7, 3);

            return genSubdividePolygon(coord1, coord2, coord3, coord4);
        }

        public GraphicsDirectedPolygon SubdividePolygon4()
        {
            Coordinate coord1 = new Coordinate(5, 2);
            Coordinate coord2 = new Coordinate(5, 10);
            Coordinate coord3 = new Coordinate(7, 10);
            Coordinate coord4 = new Coordinate(7, 2);

            return genSubdividePolygon(coord1, coord2, coord3, coord4);
        }

        public GraphicsDirectedPolygon SubdividePolygon5()
        {
            Coordinate coord1 = new Coordinate(5, 2.5);
            Coordinate coord2 = new Coordinate(5, 3.5);
            Coordinate coord3 = new Coordinate(7, 3.5);
            Coordinate coord4 = new Coordinate(7, 2.5);

            return genSubdividePolygon(coord1, coord2, coord3, coord4);
        }

        public GraphicsDirectedPolygon SubdividePolygon6()
        {
            Coordinate coord1 = new Coordinate(1, 1);
            Coordinate coord2 = new Coordinate(1, 3);
            Coordinate coord3 = new Coordinate(3, 3);
            Coordinate coord4 = new Coordinate(3, 1);

            return genSubdividePolygon(coord1, coord2, coord3, coord4);
        }

        public GraphicsDirectedPolygon SubdividePolygon7()
        {
            Coordinate coord1 = new Coordinate(1, 1);
            Coordinate coord2 = new Coordinate(1, 6);
            Coordinate coord3 = new Coordinate(6, 6);
            Coordinate coord4 = new Coordinate(6, 1);

            return genSubdividePolygon(coord1, coord2, coord3, coord4);
        }

        public GraphicsDirectedPolygon SubdividePolygon8()
        {
            Coordinate coord1 = new Coordinate(1, 1);
            Coordinate coord2 = new Coordinate(1, 9);
            Coordinate coord3 = new Coordinate(9, 9);
            Coordinate coord4 = new Coordinate(9, 1);

            return genSubdividePolygon(coord1, coord2, coord3, coord4);
        }

        public GraphicsDirectedPolygon SubdividePolygon9()
        {
            Coordinate coord1 = new Coordinate(5, 3);
            Coordinate coord2 = new Coordinate(5, 7);
            Coordinate coord3 = new Coordinate(9, 7);
            Coordinate coord4 = new Coordinate(9, 6);
            Coordinate coord5 = new Coordinate(6, 6);
            Coordinate coord6 = new Coordinate(6, 3);

            return genSubdividePolygon(coord1, coord2, coord3, coord4, coord5, coord6);
        }

        public GraphicsDirectedPolygon SubdividePolygon10()
        {
            Coordinate coord1 = new Coordinate(6, 10);
            Coordinate coord2 = new Coordinate(7, 10);
            Coordinate coord3 = new Coordinate(7, 14);
            Coordinate coord4 = new Coordinate(6, 14);

            return genSubdividePolygon(coord1, coord2, coord3, coord4);
        }

        public GraphicsDirectedPolygon SubdividePolygon11()
        {
            Coordinate coord1 = new Coordinate(6, 6);
            Coordinate coord2 = new Coordinate(7, 6);
            Coordinate coord3 = new Coordinate(7, 14);
            Coordinate coord4 = new Coordinate(6, 14);

            return genSubdividePolygon(coord1, coord2, coord3, coord4);
        }

        private GraphicsDirectedPolygon genSubdividePolygon(Coordinate coord1, Coordinate coord2, Coordinate coord3, Coordinate coord4)
        {
            GraphicsDirectedLine l1 = new GraphicsDirectedLine(window, page, new DirectedLine(coord1, coord2), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l2 = new GraphicsDirectedLine(window, page, new DirectedLine(coord2, coord3), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l3 = new GraphicsDirectedLine(window, page, new DirectedLine(coord3, coord4), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l4 = new GraphicsDirectedLine(window, page, new DirectedLine(coord4, coord1), LineRole.ExternalPerimeter);


            GraphicsDirectedPolygon result = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l1, l2, l3, l4 });

            return result;
        }


        private GraphicsDirectedPolygon genSubdividePolygon(Coordinate coord1, Coordinate coord2, Coordinate coord3, Coordinate coord4, Coordinate coord5, Coordinate coord6)
        {
            GraphicsDirectedLine l1 = new GraphicsDirectedLine(window, page, new DirectedLine(coord1, coord2), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l2 = new GraphicsDirectedLine(window, page, new DirectedLine(coord2, coord3), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l3 = new GraphicsDirectedLine(window, page, new DirectedLine(coord3, coord4), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l4 = new GraphicsDirectedLine(window, page, new DirectedLine(coord4, coord5), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l5 = new GraphicsDirectedLine(window, page, new DirectedLine(coord5, coord6), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l6 = new GraphicsDirectedLine(window, page, new DirectedLine(coord6, coord1), LineRole.ExternalPerimeter);


            GraphicsDirectedPolygon result = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l1, l2, l3, l4, l5, l6 });

            return result;
        }
    }
}
