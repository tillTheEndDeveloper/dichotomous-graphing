using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkClassLib
{
    public class Point               
    {
        public double X = 0;                 
        public double Y = 0;                  
        public Point() { }                  
        public Point(double x, double y)   
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString() => "Point(" + X + "; " + Y + ")";
    }
}
