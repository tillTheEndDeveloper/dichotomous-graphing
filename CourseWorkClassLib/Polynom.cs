using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkClassLib
{
    public class Polynom : AbstractFunction
    {
        public readonly List<XdegreePoint> coefficients = new List<XdegreePoint>();

        public Polynom() { }

        public int Count() => coefficients.Count();                         

        public void Push(XdegreePoint Xpoint) => coefficients.Add(Xpoint);          
        public void Remove(int index) => coefficients.RemoveAt(index);       
        public void Clear() => coefficients.Clear();                          

        public XdegreePoint At(int index) => coefficients[index];               

        public int Degree
        {
            get { return coefficients.Count; }
        }

        public override string ToString()
        {
            string s = "";
            for (int i = coefficients.Count - 1; i >= 0; i--)
            {
                if ((coefficients[i].X > 0) && (i == (coefficients.Count - 1)))
                {
                    if (coefficients[i].X == 1)
                    {
                    s = string.Format("x^" + i + " ");
                    }
                    else
                    s = string.Format(coefficients[i] + "x^" + i + " ");
                }
                else if (coefficients[i].X > 0)
                {
                    s = string.Format("+ " + coefficients[i].X + "x^" + i + " ");
                }
                else if (coefficients[i].X < 0)
                {
                    s = string.Format("- " + coefficients[i].X + "x^" + i + " ");
                }
            }
            return s;
        }

        
        public override double GetSolution(double x)
        {
            double result = 0;
            for (int i = 0; i < coefficients.Count; i++)
            {
                result += coefficients[i].X * Math.Pow(x, i);
            }
            return result;
        }
    }
}
