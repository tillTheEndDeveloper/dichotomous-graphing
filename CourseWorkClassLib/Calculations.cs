using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkClassLib
{
    public class Calculations                     
    {
        public delegate double Function(double a, double b);

        public readonly AbstractFunction F;
        public readonly AbstractFunction G;
        public readonly Function Func;

        public Calculations(AbstractFunction f, AbstractFunction g, Function func)
        {
            this.F = f;
            this.G = g;
            this.Func = func;
        }

        public double GetSolutionDichotomy(double A, double B, double eps)
        {
            double a = A;
            double b = B;
            double c;

            if (Func(F.GetSolution(a), G.GetSolution(a)) * Func(F.GetSolution(b), G.GetSolution(b)) >= 0)
            {
                throw new ArgumentException("no root");
            }

            while (b - a > eps)
            {
                c = (b + a) / 2;
                if (Func(F.GetSolution(a), G.GetSolution(a)) * Func(F.GetSolution(c),G.GetSolution(c)) < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                }
            }

            return (a + b) / 2;
        }

        public Point[] GetPoints(double a, double b, double step)
        {
            List<Point> points = new List<Point>();
            for (double x = a; x <= b; x += step)
                points.Add(new Point(x, G.GetSolution(x)));
            return points.ToArray();
        }
    }
}
