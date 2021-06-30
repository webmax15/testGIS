using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestGIS
{
    //сделал 3 класса в одном .cs файле, так как они маленькие и не занимают много места
    class MyPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public MyPoint(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public MyPoint() { }
    }

    class MyLine
    {
        public double X1;
        public double Y1;
        public double X2;
        public double Y2;
        public MyLine(double X1, double Y1, double X2, double Y2)
        {
            this.X1 = X1;
            this.Y1 = Y1;
            this.X2 = X2;
            this.Y2 = Y2;
        }
    }

    class MyPolygon
    {
        public MyPoint Point1 = new MyPoint();
        public MyPoint Point2 = new MyPoint();
        public MyPoint Point3 = new MyPoint();

        public MyPolygon(double X1, double Y1, double X2, double Y2, double X3, double Y3)
        {
            this.Point1.X = X1;
            this.Point1.Y = Y1;
            this.Point2.X = X2;
            this.Point2.Y = Y2;
            this.Point3.X = X3;
            this.Point3.Y = Y3;
        }
    }
}
