using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Triangles_and_Perimeters
{
    class GeometryWork 
    {
        public static Point[,] ConvertToPointArray(int[,] coords)
        {
            Point[,] points = new Point[3, coords.GetLength(1)];
            for (int i = 0; i < coords.GetLength(1); i++)
            {
                points[0, i] = new Point(coords[0, i], coords[1, i]);
                points[1, i] = new Point(coords[2, i], coords[3, i]);
                points[2, i] = new Point(coords[4, i], coords[5, i]);
            }
            return points;
        }

        public static bool PointIntoTriangle(Point point, int[] oneTriangleCoord)
        {
            int expression1 = (oneTriangleCoord[0] - point.X) * (oneTriangleCoord[3] - oneTriangleCoord[1]) -
                (oneTriangleCoord[2] - oneTriangleCoord[0]) * (oneTriangleCoord[1] - point.Y);
            int expression2 = (oneTriangleCoord[2] - point.X) * (oneTriangleCoord[5] - oneTriangleCoord[3]) -
                (oneTriangleCoord[4] - oneTriangleCoord[2]) * (oneTriangleCoord[3] - point.Y);
            int expression3 = (oneTriangleCoord[4] - point.X) * (oneTriangleCoord[1] - oneTriangleCoord[5]) -
                (oneTriangleCoord[0] - oneTriangleCoord[4]) * (oneTriangleCoord[5] - point.Y);
            if ((expression1 > 0 && expression2 > 0 && expression3 > 0) || (expression1 < 0 && expression2 < 0
                && expression3 < 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Point[] PointsGeneration(Random rnd, int pointsCount)
        {
            int[,] pointsLocationsArray = new int[2, pointsCount];
            Point[] points = new Point[pointsCount];
            for (int i = 0; i < pointsCount; i++)
            {
                pointsLocationsArray[0, i] = rnd.Next(21);
                pointsLocationsArray[1, i] = rnd.Next(21);
                points[i] = new Point(pointsLocationsArray[0, i], pointsLocationsArray[1, i]);
                for (int j = 0; j < i; j++)
                {
                    if (points[j] == points[i])
                    {
                        i--;
                    }
                }
            }
            return points;
        }

        public static bool PointsArraysEqualCheck(Point[] p1, Point[] p2)
        {
            if (p1.Length == p2.Length)
            {
                int matches = 0;
                for (int i = 0; i < p1.Length; i++)
                {
                    for (int j = 0; j < p2.Length; j++)
                    {
                        if (p1[i] == p2[j])
                        {
                            matches++;
                        }
                    }
                }
                if (matches == p1.Length)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else { return false; }
        }

        public static void CoordinateGridAdd(PictureBox pb, Graphics grf)
        {
            Pen grid = new Pen(Color.Gray, 1);
            Pen axis = new Pen(Color.Black, 3);

            grf.DrawLine(axis, 0, 0, 0, pb.Height);
            grf.DrawLine(axis, 0, pb.Height - 1, pb.Width, pb.Height - 1);

            for (int i = 20; i < pb.Width; i += 20)
            {
                grf.DrawLine(grid, i, 0, i, pb.Height);
            }

            for (int i = 20; i < pb.Height; i += 20)
            {
                grf.DrawLine(grid, 0, pb.Height - i, pb.Width, pb.Height - i);
            }
        }

        public static void PointsAdd(Point[] points, PictureBox pb, Graphics grf, SolidBrush brush)
        {
            for (int i = 0; i < points.Length; i++)
            {
                grf.FillEllipse(brush, new Rectangle(20 * points[i].X - 5, pb.Height - (20 * points[i].Y + 5), 10, 10));
            }
        }

        public static void PointsAdd(PictureBox pb, Graphics grf, SolidBrush brush, int X, int Y)
        {
            int X1 = (int)Math.Round((double)X / 20) * 20;
            int Y1 = (int)Math.Round((double)Y / 20) * 20;
            grf.FillEllipse(brush, new Rectangle(X1 - 5, Y1 + 2, 10, 10));
            pb.Refresh();
        }

        public static int[,] MaxTriangleSearch(Point[] points)
        {
            double perimeter = 0;
            int[,] coordinates;
            int trianglesCount = 0;
            int counter = 0;
            for (int i = 0; i < points.Length; i++)
            {
                for (int j = 0; j < points.Length; j++)
                {
                    for (int k = 0; k < points.Length; k++)
                    {
                        if (i != j && j != k && i != k)
                        {
                            double distance1 = Math.Sqrt(Math.Pow(points[i].X - points[j].X, 2) +
                                Math.Pow(points[i].Y - points[j].Y, 2));
                            double distance2 = Math.Sqrt(Math.Pow(points[i].X - points[k].X, 2) +
                                Math.Pow(points[i].Y - points[k].Y, 2));
                            double distance3 = Math.Sqrt(Math.Pow(points[k].X - points[j].X, 2) +
                                Math.Pow(points[k].Y - points[j].Y, 2));
                            if (distance1 + distance2 != distance3 && distance1 + distance3 != distance2
                                && distance2 + distance3 != distance1)
                            {
                                if (distance1 + distance2 + distance3 > perimeter)
                                {
                                    trianglesCount = 1;
                                    perimeter = distance1 + distance2 + distance3;
                                }
                                else if (distance1 + distance2 + distance3 == perimeter)
                                {
                                    trianglesCount++;
                                }
                            }
                        }
                    }
                }
            }
            coordinates = new int[6, trianglesCount];

            for (int i = 0; i < points.Length; i++)
            {
                for (int j = 0; j < points.Length; j++)
                {
                    for (int k = 0; k < points.Length; k++)
                    {
                        if (i != j && j != k && i != k)
                        {
                            double distance1 = Math.Sqrt(Math.Pow(points[i].X - points[j].X, 2) +
                                Math.Pow(points[i].Y - points[j].Y, 2));
                            double distance2 = Math.Sqrt(Math.Pow(points[i].X - points[k].X, 2) +
                                Math.Pow(points[i].Y - points[k].Y, 2));
                            double distance3 = Math.Sqrt(Math.Pow(points[k].X - points[j].X, 2) +
                                Math.Pow(points[k].Y - points[j].Y, 2));
                            if (distance1 + distance2 != distance3 && distance1 + distance3 != distance2
                                && distance2 + distance3 != distance1 && distance1 + distance2 + distance3 == perimeter)
                            {
                                coordinates[0, counter] = points[i].X;
                                coordinates[1, counter] = points[i].Y;
                                coordinates[2, counter] = points[j].X;
                                coordinates[3, counter] = points[j].Y;
                                coordinates[4, counter] = points[k].X;
                                coordinates[5, counter] = points[k].Y;
                                counter++;
                            }
                        }
                    }
                }
            }
            return coordinates;
        }

        public static int[,] MaxTrianglesSearchInB(Point[] points, int[,] TriangleCoord)
        {
            int trianglesNumber = TriangleCoord.GetLength(1);
            string strMaxTriangles = "";
            int[,] MaxTriangles;
            for (int j = 0; j < trianglesNumber; j++)
            {
                int[] oneTriangleCoord = new int[6];
                for (int i = 0; i < 6; i++)
                {
                    oneTriangleCoord[i] = TriangleCoord[i, j];
                }
                Point[] intoPoints;
                int[] intoPointsX = new int[0];
                int[] intoPointsY = new int[0];
                int intoPointsCount = 0;
                for (int i = 0; i < points.Length; i++)
                {
                    if (GeometryWork.PointIntoTriangle(points[i], oneTriangleCoord))
                    {
                        intoPointsCount++;
                        Array.Resize(ref intoPointsX, intoPointsCount);
                        Array.Resize(ref intoPointsY, intoPointsCount);
                        intoPointsX[intoPointsCount - 1] = points[i].X;
                        intoPointsY[intoPointsCount - 1] = points[i].Y;
                    }
                }
                intoPoints = new Point[intoPointsCount];
                for (int i = 0; i < intoPointsCount; i++)
                {
                    intoPoints[i] = new Point(intoPointsX[i], intoPointsY[i]);
                }
                int[,] MaxTriangle = MaxTriangleSearch(intoPoints);
                int a = MaxTriangle.GetLength(0);
                int b = MaxTriangle.GetLength(1);
                for (int k = 0; k < b; k++)
                {
                    for (int i = 0; i < a; i++)
                    {
                        strMaxTriangles += MaxTriangle[i, k];
                        strMaxTriangles += ",";
                    }
                }
            }
            int[] listMaxTriangles = strMaxTriangles.Split(',').
            Where(x => !string.IsNullOrWhiteSpace(x)).
            Select(x => int.Parse(x)).ToArray();
            int MiniTrianglesNumber = listMaxTriangles.Length / 6;
            MaxTriangles = new int[6, MiniTrianglesNumber];
            for (int i = 0; i < MiniTrianglesNumber; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    MaxTriangles[j, i] = listMaxTriangles[6 * i + j];
                }
            }
            return MaxTriangles;
        }

        public static Point[] MaxTriangleSearchInB(int[,] TriangleCoord)
        {
            Point[] triangle = new Point[3];
            double perimeter = 0;
            for (int i = 0; i < TriangleCoord.GetLength(1); i++)
            {
                double distance1 = Math.Sqrt(Math.Pow(TriangleCoord[0, i] - TriangleCoord[2, i], 2) +
                    Math.Pow(TriangleCoord[1, i] - TriangleCoord[3, i], 2));
                double distance2 = Math.Sqrt(Math.Pow(TriangleCoord[0, i] - TriangleCoord[4, i], 2) +
                    Math.Pow(TriangleCoord[1, i] - TriangleCoord[5, i], 2));
                double distance3 = Math.Sqrt(Math.Pow(TriangleCoord[2, i] - TriangleCoord[4, i], 2) +
                    Math.Pow(TriangleCoord[3, i] - TriangleCoord[5, i], 2));
                if (distance1 + distance2 + distance3 > perimeter)
                {
                    perimeter = distance1 + distance2 + distance3;
                    triangle[0] = new Point(TriangleCoord[0, i], TriangleCoord[1, i]);
                    triangle[1] = new Point(TriangleCoord[2, i], TriangleCoord[3, i]);
                    triangle[2] = new Point(TriangleCoord[4, i], TriangleCoord[5, i]);
                }
            }
            return triangle;
        }
    }
}
