using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorLib
{
    public class Vector
    {
        public double X;
        public double Y;

        // Constructors.
        public Vector(double x, double y) { X = x; Y = y; }
        public Vector() : this(double.NaN, double.NaN) { }

        public static Vector operator -(Vector v, Vector w)
        {
            return new Vector(v.X - w.X, v.Y - w.Y);
        }

        public static Vector operator +(Vector v, Vector w)
        {
            return new Vector(v.X + w.X, v.Y + w.Y);
        }

        public static double operator *(Vector v, Vector w)
        {
            return v.X * w.X + v.Y * w.Y;
        }

        public static Vector operator *(Vector v, double mult)
        {
            return new Vector(v.X * mult, v.Y * mult);
        }

        public static Vector operator *(double mult, Vector v)
        {
            return new Vector(v.X * mult, v.Y * mult);
        }

        public double Cross(Vector v)
        {
            return X * v.Y - Y * v.X;
        }

        public override bool Equals(object obj)
        {
            var v = (Vector)obj;
            return isZero(X - v.X) && isZero(Y - v.Y);
        }

        private const double Epsilon = 1e-10;

        private bool isZero(double x)
        {
            return Math.Abs(x) < Epsilon;
        }

        private static bool IsZero(double x)
        {
            return Math.Abs(x) < Epsilon;
        }

        public static bool LineSegmentsOverlap(
             Vector p
            , Vector p2
            , Vector q
            , Vector q2)
        {
            var r = p2 - p;
            var s = q2 - q;
            var rxs = r.Cross(s);
            var qpxr = (q - p).Cross(r);

            // If r x s = 0 and (q - p) x r = 0, then the two lines are collinear.
            if (IsZero(rxs) && IsZero(qpxr))
            {
                // 1. If either  0 <= (q - p) * r <= r * r or 0 <= (p - q) * s <= * s
                // then the two lines are overlapping,
                
                if ((0 <= (q - p) * r && (q - p) * r <= r * r) || (0 <= (p - q) * s && (p - q) * s <= s * s))
                    return true;

                // 2. If neither 0 <= (q - p) * r = r * r nor 0 <= (p - q) * s <= s * s
                // then the two lines are collinear but disjoint.
                // No need to implement this expression, as it follows from the expression above.
                return false;
            }

            return false;
        }

        /// <summary>
        /// Test whether two line segments intersect. If so, calculate the intersection point.
        /// <see cref="http://stackoverflow.com/a/14143738/292237"/>
        /// </summary>
        /// <param name="p">Vector to the start point of p.</param>
        /// <param name="p2">Vector to the end point of p.</param>
        /// <param name="q">Vector to the start point of q.</param>
        /// <param name="q2">Vector to the end point of q.</param>
        /// <param name="intersection">The point of intersection, if any.</param>
        /// <param name="considerOverlapAsIntersect">Do we consider overlapping lines as intersecting?
        /// </param>
        /// <returns>True if an intersection point was found.</returns>
        public static bool LineSegementsIntersect(
            Vector p
            , Vector p2
            , Vector q
            , Vector q2
            , out Vector intersection
            , bool considerCollinearOverlapAsIntersect = false)
        {
            intersection = new Vector();

            var r = p2 - p;
            var s = q2 - q;
            var rxs = r.Cross(s);
            var qpxr = (q - p).Cross(r);

            // If r x s = 0 and (q - p) x r = 0, then the two lines are collinear.
            if (IsZero(rxs) && IsZero(qpxr))
            {
                // 1. If either  0 <= (q - p) * r <= r * r or 0 <= (p - q) * s <= * s
                // then the two lines are overlapping,
                if (considerCollinearOverlapAsIntersect)
                    if ((0 <= (q - p) * r && (q - p) * r <= r * r) || (0 <= (p - q) * s && (p - q) * s <= s * s))
                        return true;

                // 2. If neither 0 <= (q - p) * r = r * r nor 0 <= (p - q) * s <= s * s
                // then the two lines are collinear but disjoint.
                // No need to implement this expression, as it follows from the expression above.
                return false;
            }

            // 3. If r x s = 0 and (q - p) x r != 0, then the two lines are parallel and non-intersecting.
            if (IsZero(rxs) && !IsZero(qpxr))
                return false;

            // t = (q - p) x s / (r x s)
            var t = (q - p).Cross(s) / rxs;

            // u = (q - p) x r / (r x s)

            var u = (q - p).Cross(r) / rxs;

            // 4. If r x s != 0 and 0 <= t <= 1 and 0 <= u <= 1
            // the two line segments meet at the point p + t r = q + u s.
            if (!IsZero(rxs) && (0 <= t && t <= 1) && (0 <= u && u <= 1))
            {
                // We can calculate the intersection point using either t or u.
                intersection = p + t * r;

                // An intersection was found.
                return true;
            }

            // 5. Otherwise, the two line segments are not parallel but do not intersect.
            return false;
        }

        //public static bool LineSegementsOverlap(
        //   Vector p
        //   , Vector p2
        //   , Vector q
        //   , Vector q2)
        //{
        //    Vector r = p2 - p;
        //    Vector s = q2 - q;

        //    double test1 = (q - p) * r ;
        //    double test2 = (p - q) * s;

        //    double cmpr1 = r * r;
        //    double cmpr2 = s * s;

        //    if ((0 <= (q - p) * r && (q - p) * r <= r * r) || (0 <= (p - q) * s && (p - q) * s <= s * s))
        //    {
        //        return true;
        //    }
            
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public static bool LineSegementsProperOverlap(
        //   Vector p
        //   , Vector p2
        //   , Vector q
        //   , Vector q2)
        //{
        //    Vector r = p2 - p;
        //    Vector s = q2 - q;

        //    double test1 = (q - p) * r;
        //    double test2 = (p - q) * s;

        //    double cmpr1 = r * r;
        //    double cmpr2 = s * s;

        //    if ((0 < (q - p) * r && (q - p) * r < r * r) || (0 < (p - q) * s && (p - q) * s < s * s))
        //    {
        //        return true;
        //    }

        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
