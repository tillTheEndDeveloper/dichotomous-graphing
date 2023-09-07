using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkClassLib
{
    public class PointsList : AbstractFunction
    {
        public readonly List<Point> points = new List<Point>();       

        public PointsList() { }                                         

        public int Count() => points.Count();                          

        public void Push(Point point) => points.Add(point);             
        public void Remove(int index) => points.RemoveAt(index);     
        public void Clear() => points.Clear();                          

        public Point At(int index) => points[index];                   

        public override double GetSolution(double x)                    
        {
            int i = 0;
            double y;

            if (x < points[i].X)
            {
                y = x * ((points[1].Y - points[0].Y) / (points[1].X - points[0].X)) + (points[0].Y - points[0].X * 
                    ((points[1].Y - points[0].Y) / (points[1].X - points[0].X)));
                return y;
            }

            while (x >= points[i].X)
            {
                if (points[i] == points.Last())
                {
                    y = x * ((points[i].Y - points[i - 1].Y) / (points[i].X - points[i - 1].X)) + (points[i - 1].Y - points[i - 1].X *
                        ((points[i].Y - points[i - 1].Y) / (points[i].X - points[i - 1].X)));
                    return y;
                }
                i++;
            }
            i--;
            
            y = points[i].Y + ((points[i + 1].Y - points[i].Y) / (points[i + 1].X - points[i].X)) * (x - points[i].X);           

            return y;             
        }

        public override string ToString()
        {
            string s = "PointsList{\n";
            foreach (Point point in points)
                s += "\t" + point + ",\n";
            return s + "}";
        }
    }
}
