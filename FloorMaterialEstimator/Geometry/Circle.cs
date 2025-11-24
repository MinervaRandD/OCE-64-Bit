namespace Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Circle
    {
        private Coordinate _center = Coordinate.NullCoordinate;

        public double Radius { get; set; } = double.NaN;

        private double _x = double.NaN;
        private double _y = double.NaN;

        public Coordinate Center
        {
            get
            {
                if (_center == Coordinate.NullCoordinate)
                {
                    _center = new Coordinate(X, Y);
                }

                return _center;
            }

            set
            {
                if (_center == value)
                {
                    return;
                }

                _center = value;

                _x = value.X; _y = value.Y;
            }
        }

        public double X
        {
            get
            {
                return _x;
            }

            set
            {
                if (_x == value)
                {
                    return;
                }

                _x = value;

                _center = new Coordinate(_x, _y);
            }
        }

        public double Y
        {
            get
            {
                return _y;
            }

            set
            {
                if (_y == value)
                {
                    return;
                }

                _y = value;

                _center = new Coordinate(_x, _y);
            }
        }

        public Circle()
        {

        }

        public Circle(Coordinate center, double radius)
        {
            _center = center;
            Radius = radius;
        }

        public Circle(Tuple<double, double> center, double radius)
        {
            _center = new Coordinate(center.Item1, center.Item2);
            Radius = radius;
        }

    }
}
