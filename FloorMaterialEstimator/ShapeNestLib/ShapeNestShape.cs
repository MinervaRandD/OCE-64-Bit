using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Geometry;

namespace ShapeNestLib
{
    public class ShapeNestShape
    {
        public List<PointF> points;
        public int? selectionNmbr;
        public int indxNmbr;
        public Color lineColor;
        public Color fillColor;
        public bool Selected = false;

        public int rotations = 0;

        public float MinX
        {
            get
            {
                return points.Min(p => p.X);
            }
        }

        public float MaxX
        {
            get
            {
                return points.Max(p => p.X);
            }
        }

        public float MinY
        {
            get
            {
                return points.Min(p => p.Y);
            }
        }

        public float MaxY
        {
            get
            {
                return points.Max(p => p.Y);
            }
        }

        public float Width
        {
            get
            {
                return MaxX - MinX;
            }
        }

        public float Height
        {
            get
            {
                return MaxY - MinY;
            }
        }

        public ShapeNestShape(
            List<PointF> points
            , Color lineColor
            , Color fillColor
            , int? selectionNmbr
            , int indxNmbr
            , int rotations = 0)
        {
            this.points = points;
            this.selectionNmbr = selectionNmbr;
            this.lineColor = lineColor;
            this.fillColor = fillColor;
            this.indxNmbr = indxNmbr;
            this.rotations = rotations;
        }

        public double? _horizontalSpan = null;

        public double? _verticalSpan = null;

        public double HorizontalSpan
        {
            get
            {
                if (_horizontalSpan == null)
                {
                    _horizontalSpan = genHorizontalSpan();
                }

                return _horizontalSpan.Value;
            }
        }

        public double VerticalSpan
        {
            get
            {
                if (_verticalSpan == null)
                {
                    _verticalSpan = genVerticalSpan();
                }

                return _verticalSpan.Value;
            }
        }

        public double genHorizontalSpan()
        {
            // Calculate the max end to end horizontal span given rotations.

            // Calculate zero rotation horizontal span

            double maxX = points.Max(p => p.X);
            double minX = points.Min(p => p.X);
            
            double maxHorizontalSpan = maxX - minX;


            double rotationTheta = Math.PI / rotations;

            
            for (int rotation = 1; rotation <= rotations; rotation++)
            {
                double theta = rotation * rotationTheta;

                double cosTheta = Math.Cos(theta);
                double sinTheta = Math.Sin(theta);

                maxX = double.MinValue;
                minX = double.MaxValue;

                foreach (var point in points)
                {
                    double x = point.X * cosTheta - point.Y * sinTheta;
                   
                    if (x > maxX)
                    {
                        maxX = x;
                    }

                    if (x < minX)
                    {
                        minX = x;
                    }
                }

                double horizontalSpan = maxX - minX;

                if (horizontalSpan > maxHorizontalSpan)
                {
                    maxHorizontalSpan = horizontalSpan;
                }
            }

            return maxHorizontalSpan;
        }

        public double genVerticalSpan()
        {
            // Calculate the max end to end vertical span given rotations.

            // Calculate zero rotation vertical span

            double maxY = points.Max(p => p.Y);
            double minY = points.Min(p => p.Y);

            double maxVerticalSpan = maxY - minY;


            double rotationTheta = Math.PI / rotations;


            for (int rotation = 1; rotation <= rotations; rotation++)
            {
                double theta = rotation * rotationTheta;

                double cosTheta = Math.Cos(theta);
                double sinTheta = Math.Sin(theta);

                maxY = double.MinValue;
                minY = double.MaxValue;

                foreach (var point in points)
                {
                    double y = point.X * sinTheta + point.Y* cosTheta;

                    if (y > maxY)
                    {
                        maxY = y;
                    }

                    if (y < minY)
                    {
                        minY = y;
                    }
                }

                double verticalSpan = maxY - minY;

                if (verticalSpan > maxVerticalSpan)
                {
                    maxVerticalSpan = verticalSpan;
                }
            }

            return maxVerticalSpan;
        }
    }
}
