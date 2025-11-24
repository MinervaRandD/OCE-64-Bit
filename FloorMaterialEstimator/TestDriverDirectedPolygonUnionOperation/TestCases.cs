

namespace TestDriverDirectedPolygonUnionOperations
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

            window = new GraphicsWindow(VsoWindow);

            page = new GraphicsPage(window, VsoPage);

        }

        public List<GraphicsDirectedPolygon> TestCase1()
        {
            Coordinate coord1 = new Coordinate(2, 2);
            Coordinate coord2 = new Coordinate(2, 10);
            Coordinate coord3 = new Coordinate(6, 10);
            Coordinate coord4 = new Coordinate(6, 2);

            GraphicsDirectedLine l1 = new GraphicsDirectedLine(window, page, new DirectedLine(coord1, coord2), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l2 = new GraphicsDirectedLine(window, page, new DirectedLine(coord2, coord3), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l3 = new GraphicsDirectedLine(window, page, new DirectedLine(coord3, coord4), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l4 = new GraphicsDirectedLine(window, page, new DirectedLine(coord4, coord1), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp1 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l1, l2, l3, l4 });

            Coordinate coord5 = new Coordinate(4, 2);
            Coordinate coord6 = new Coordinate(4, 10);
            Coordinate coord7 = new Coordinate(8, 10);
            Coordinate coord8 = new Coordinate(8, 2);

            GraphicsDirectedLine l5 = new GraphicsDirectedLine(window, page, new DirectedLine(coord5, coord6), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l6 = new GraphicsDirectedLine(window, page, new DirectedLine(coord6, coord7), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l7 = new GraphicsDirectedLine(window, page, new DirectedLine(coord7, coord8), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l8 = new GraphicsDirectedLine(window, page, new DirectedLine(coord8, coord1), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp2 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l5, l6, l7, l8 });

            return new List<GraphicsDirectedPolygon>() { gdp1, gdp2 };
        }

        public List<GraphicsDirectedPolygon> TestCase2()
        {
            Coordinate coord1 = new Coordinate(2, 2);
            Coordinate coord2 = new Coordinate(2, 10);
            Coordinate coord3 = new Coordinate(6, 10);
            Coordinate coord4 = new Coordinate(6, 2);

            GraphicsDirectedLine l1 = new GraphicsDirectedLine(window, page, new DirectedLine(coord1, coord2), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l2 = new GraphicsDirectedLine(window, page, new DirectedLine(coord2, coord3), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l3 = new GraphicsDirectedLine(window, page, new DirectedLine(coord3, coord4), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l4 = new GraphicsDirectedLine(window, page, new DirectedLine(coord4, coord1), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp1 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l1, l2, l3, l4 });

            Coordinate coord5 = new Coordinate(4, 2);
            Coordinate coord6 = new Coordinate(10, 8);
            Coordinate coord7 = new Coordinate(4, 10);
       

            GraphicsDirectedLine l5 = new GraphicsDirectedLine(window, page, new DirectedLine(coord5, coord6), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l6 = new GraphicsDirectedLine(window, page, new DirectedLine(coord6, coord7), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l7 = new GraphicsDirectedLine(window, page, new DirectedLine(coord7, coord5), LineRole.ExternalPerimeter);
           
            GraphicsDirectedPolygon gdp2 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l5, l6, l7 });

            return new List<GraphicsDirectedPolygon>() { gdp1, gdp2 };
        }


        public List<GraphicsDirectedPolygon> TestCase3()
        {
            Coordinate coord1 = new Coordinate(2, 2);
            Coordinate coord2 = new Coordinate(2, 10);
            Coordinate coord3 = new Coordinate(5, 10);
            Coordinate coord4 = new Coordinate(5, 2);

            GraphicsDirectedLine l1 = new GraphicsDirectedLine(window, page, new DirectedLine(coord1, coord2), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l2 = new GraphicsDirectedLine(window, page, new DirectedLine(coord2, coord3), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l3 = new GraphicsDirectedLine(window, page, new DirectedLine(coord3, coord4), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l4 = new GraphicsDirectedLine(window, page, new DirectedLine(coord4, coord1), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp1 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l1, l2, l3, l4 });

            Coordinate coord5 = new Coordinate(5, 2);
            Coordinate coord6 = new Coordinate(5, 10);
            Coordinate coord7 = new Coordinate(10, 10);
            Coordinate coord8 = new Coordinate(10, 2);

            GraphicsDirectedLine l5 = new GraphicsDirectedLine(window, page, new DirectedLine(coord5, coord6), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l6 = new GraphicsDirectedLine(window, page, new DirectedLine(coord6, coord7), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l7 = new GraphicsDirectedLine(window, page, new DirectedLine(coord7, coord8), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l8 = new GraphicsDirectedLine(window, page, new DirectedLine(coord8, coord5), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp2 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l5, l6, l7, l8 });

            return new List<GraphicsDirectedPolygon>() { gdp1, gdp2 };
        }

        public List<GraphicsDirectedPolygon> TestCase4()
        {
            Coordinate coord1 = new Coordinate(2, 2);
            Coordinate coord2 = new Coordinate(2, 10);
            Coordinate coord3 = new Coordinate(4, 10);
            Coordinate coord4 = new Coordinate(4, 2);

            GraphicsDirectedLine l1 = new GraphicsDirectedLine(window, page, new DirectedLine(coord1, coord2), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l2 = new GraphicsDirectedLine(window, page, new DirectedLine(coord2, coord3), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l3 = new GraphicsDirectedLine(window, page, new DirectedLine(coord3, coord4), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l4 = new GraphicsDirectedLine(window, page, new DirectedLine(coord4, coord1), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp1 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l1, l2, l3, l4 });

            Coordinate coord5 = new Coordinate(6, 2);
            Coordinate coord6 = new Coordinate(6, 10);
            Coordinate coord7 = new Coordinate(10, 10);
            Coordinate coord8 = new Coordinate(10, 2);

            GraphicsDirectedLine l5 = new GraphicsDirectedLine(window, page, new DirectedLine(coord5, coord6), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l6 = new GraphicsDirectedLine(window, page, new DirectedLine(coord6, coord7), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l7 = new GraphicsDirectedLine(window, page, new DirectedLine(coord7, coord8), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l8 = new GraphicsDirectedLine(window, page, new DirectedLine(coord8, coord5), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp2 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l5, l6, l7, l8 });

            return new List<GraphicsDirectedPolygon>() { gdp1, gdp2 };
        }

        public List<GraphicsDirectedPolygon> TestCase5()
        {
            Coordinate coord1 = new Coordinate(2, 2);
            Coordinate coord2 = new Coordinate(2, 10);
            Coordinate coord3 = new Coordinate(5, 10);
            Coordinate coord4 = new Coordinate(5, 2);

            GraphicsDirectedLine l1 = new GraphicsDirectedLine(window, page, new DirectedLine(coord1, coord2), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l2 = new GraphicsDirectedLine(window, page, new DirectedLine(coord2, coord3), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l3 = new GraphicsDirectedLine(window, page, new DirectedLine(coord3, coord4), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l4 = new GraphicsDirectedLine(window, page, new DirectedLine(coord4, coord1), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp1 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l1, l2, l3, l4 });

            Coordinate coord5 = new Coordinate(4, 2);
            Coordinate coord6 = new Coordinate(4, 10);
            Coordinate coord7 = new Coordinate(7, 10);
            Coordinate coord8 = new Coordinate(7, 2);

            GraphicsDirectedLine l5 = new GraphicsDirectedLine(window, page, new DirectedLine(coord5, coord6), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l6 = new GraphicsDirectedLine(window, page, new DirectedLine(coord6, coord7), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l7 = new GraphicsDirectedLine(window, page, new DirectedLine(coord7, coord8), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l8 = new GraphicsDirectedLine(window, page, new DirectedLine(coord8, coord5), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp2 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l5, l6, l7, l8 });

            Coordinate coord9 = new Coordinate(6, 2);
            Coordinate coord10 = new Coordinate(6, 10);
            Coordinate coord11 = new Coordinate(8, 10);
            Coordinate coord12 = new Coordinate(8, 2);

            GraphicsDirectedLine l9 = new GraphicsDirectedLine(window, page, new DirectedLine(coord9, coord10), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l10 = new GraphicsDirectedLine(window, page, new DirectedLine(coord10, coord11), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l11 = new GraphicsDirectedLine(window, page, new DirectedLine(coord11, coord12), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l12 = new GraphicsDirectedLine(window, page, new DirectedLine(coord12, coord9), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp3 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l9, l10, l11, l12 });

            return new List<GraphicsDirectedPolygon>() { gdp1, gdp2, gdp3 };
        }

        public List<GraphicsDirectedPolygon> TestCase6()
        {
            Coordinate coord1 = new Coordinate(2, 2);
            Coordinate coord2 = new Coordinate(2, 10);
            Coordinate coord3 = new Coordinate(4, 10);
            Coordinate coord4 = new Coordinate(4, 2);

            GraphicsDirectedLine l1 = new GraphicsDirectedLine(window, page, new DirectedLine(coord1, coord2), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l2 = new GraphicsDirectedLine(window, page, new DirectedLine(coord2, coord3), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l3 = new GraphicsDirectedLine(window, page, new DirectedLine(coord3, coord4), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l4 = new GraphicsDirectedLine(window, page, new DirectedLine(coord4, coord1), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp1 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l1, l2, l3, l4 });

            Coordinate coord5 = new Coordinate(5, 2);
            Coordinate coord6 = new Coordinate(5, 10);
            Coordinate coord7 = new Coordinate(7, 10);
            Coordinate coord8 = new Coordinate(7, 2);

            GraphicsDirectedLine l5 = new GraphicsDirectedLine(window, page, new DirectedLine(coord5, coord6), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l6 = new GraphicsDirectedLine(window, page, new DirectedLine(coord6, coord7), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l7 = new GraphicsDirectedLine(window, page, new DirectedLine(coord7, coord8), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l8 = new GraphicsDirectedLine(window, page, new DirectedLine(coord8, coord5), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp2 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l5, l6, l7, l8 });

            Coordinate coord9 = new Coordinate(6, 2);
            Coordinate coord10 = new Coordinate(6, 10);
            Coordinate coord11 = new Coordinate(8, 10);
            Coordinate coord12 = new Coordinate(8, 2);

            GraphicsDirectedLine l9 = new GraphicsDirectedLine(window, page, new DirectedLine(coord9, coord10), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l10 = new GraphicsDirectedLine(window, page, new DirectedLine(coord10, coord11), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l11 = new GraphicsDirectedLine(window, page, new DirectedLine(coord11, coord12), LineRole.ExternalPerimeter);
            GraphicsDirectedLine l12 = new GraphicsDirectedLine(window, page, new DirectedLine(coord12, coord9), LineRole.ExternalPerimeter);

            GraphicsDirectedPolygon gdp3 = new GraphicsDirectedPolygon(window, page, new List<GraphicsDirectedLine>() { l9, l10, l11, l12 });

            return new List<GraphicsDirectedPolygon>() { gdp1, gdp2, gdp3 };
        }

    }
}
