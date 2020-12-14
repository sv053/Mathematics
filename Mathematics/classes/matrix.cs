using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics.classes
{
    public class matrix
    {

        double first = 0.0;
        double second = 0.0;
        double third = 0.0;

        public matrix(double[] m)
        {
            First = m[0];
            Second = m[1];
            Third = m[2];
        }

        public double Third { get => third; set => third = value; }
        public double Second { get => second; set => second = value; }
        public double First { get => first; set => first = value; }
    }
}
