using System;
using System.Collections.Generic;

namespace Mathematics.classes
{
    class Integration
    {
        double integral;
        double integralTrap = 0.0;
        double eps; //= 0.0001;
        int nodesNumberInt = 0;
        double Integral4nodes = 0.0, Integral5nodes = 0.0;
        int nodesCount;
        double gaussError;

        //List<double> xInitialNodes; //= new List<double>();
        //List<double> yInitialNodes; // = new List<double>();
        List<double> xNewNodes = new List<double>();
        List<double> yNewNodes = new List<double>();
     
        public Integration(double eps, int nodeNumber, double xfirst, double xlast)
        {
            Eps = eps;
            NodesNumber = nodeNumber;
            XFirst = xfirst;
            XLast = xlast;
       }

        public double Integral { get => integral; set => integral = value; }
        public double Eps { get => eps; set => eps = value; }
        public int NodesNumber { get ; set ; }

        // получаем узлы исходной сетки
        //public List<double> XInitialNodes
        //{
        //    get { return xInitialNodes; }
        //    set
        //    {
        //        if (xInitialNodes == null)
        //            xInitialNodes = new List<double>();
        //        else
        //        xInitialNodes = value;
        //    }
        //}

        //// получаем значения функции в узлах исходной сетки
        //public List<double> YInitialNodes
        //{
        //    get
        //    {
        //        return yInitialNodes;
        //    }
        //    set
        //    {
        //        if (yInitialNodes == null)
        //            yInitialNodes = new List<double>();
        //        else
        //            yInitialNodes = value;
        //    }
        //}
        public double XFirst { get ; set; }
        public double XLast { get ; set ; }
        public double IntegralTrap { get => integralTrap; set => integralTrap = value; }
        public int NodesCount { get => nodesCount; set => nodesCount = value; }
        public double GaussError { get => gaussError; set => gaussError = value; }

 
        // функция для интегрирования
        public double BasicFunction(double var)
        {
            return Math.Pow(var, 3.0)/(Math.Sqrt(Math.Pow(var, 2.0)+1));
        }


        // получаем сетку для заданного интервала интегрирования
        //public List<double> GetxInitialNodes()
        //{
        //    double delta = (XLast - XFirst) / NodesNumber;

        //    XInitialNodes = new List<double>(NodesNumber);

        //    for(int i=1; i<=NodesNumber; i++)
        //    {
        //        XInitialNodes.Add(XFirst+i*delta);
        //    }
        //    return XInitialNodes;
        //}
   
        //// получаем значения начальной сетки для заданного интервала интегрирования
        //public List<double> GetyInitialNodes()
        //{
       
        //    YInitialNodes = new List<double>();

        //    foreach (double x in XInitialNodes)
        //    {
        //        YInitialNodes.Add(BasicFunction(x));
        //    }
        //    return YInitialNodes;
        //}

        // интегрируем методом Гаусса
        public double IntegrateGauss(double xfirstnew, double xlastnew, int numberOfNodes)
        {
            double X = 0.0,x=0.0, I = 0.0;
            double[] ti, Ai;

            double[] ti5 = new double[] { -0.90617985, -0.53846931, 0, 0.53846931, 0.90617985 };
            double[] Ai5 = new double[] { 0.23692688, 0.47862868, 0.56888889, 0.47862868, 0.23692688 };
            double[] ti4 = { -0.86113631, -0.33998104, 0.33998104, 0.86113631 };
            double[] Ai4 = { 0.34785484, 0.65214516, 0.65214516, 0.34785484 };

            double aAndb = (xlastnew + xfirstnew) / 2;
            double aMinusb = (xlastnew - xfirstnew) / 2;

            if (numberOfNodes == 4)
            {
                ti = ti4;
                Ai = Ai4;
            }
            else
            {
                ti = ti5;
                Ai = Ai5;
            }

            for (int i = 0; i < numberOfNodes; i++)
                {
                    X = BasicFunction(aAndb - aMinusb * ti[i])+ BasicFunction(aAndb + aMinusb * ti[i]);

                    x += Ai[i] * X;
                }
                I = x/2 * aAndb;
          
            return I;
        }

        // считаем погрешность интегрирования методом Гаусса
        public double CountGaussError(double xfirstnew, double xlastnew)
        {
            NodesCount = 5;

            if (Integral4nodes==0 && Integral5nodes == 0)
            {
                Integral4nodes = IntegrateGauss(xfirstnew, xlastnew, 4);
                Integral5nodes = IntegrateGauss(xfirstnew, xlastnew, 5);
            }

            if (Math.Abs(Integral4nodes - Integral5nodes)/Integral5nodes > eps)
            {
                while(Math.Abs(Integral4nodes - Integral5nodes) / Integral5nodes > eps)
                {
                    NodesCount *= 2;
                    if (NodesCount >= 1000) break;
                    Integral4nodes = (IntegrateGauss(xfirstnew, xlastnew / 2, 4) + IntegrateGauss(xlastnew / 2, xlastnew, 4))/(NodesCount/4);
                    Integral5nodes = (IntegrateGauss(xfirstnew, xlastnew / 2, 5) + IntegrateGauss(xlastnew / 2, xlastnew, 5))/(NodesCount/5);
                    
                }
            }
            else
            {
                GaussError = Math.Abs(Integral4nodes - Integral5nodes) / Integral5nodes;
                return Integral5nodes;
            }
                

            return Integral5nodes;
        }
       
        // интегрируем методом трапеции
        public double IntegrateTrapeze(int nodesNumberI, double delta)
        {
           for (int i = 1; i < nodesNumberI; i++)
            {
              Integral += ((BasicFunction(XFirst + (i + 1) * delta) + BasicFunction(XFirst + (i) * delta)) / 2 * ((i + 1) * delta - (i) * delta));// + err;
            }

            return Integral;
        }

        // считаем погрешность интегрирования методом трапеции
        public int CountTrapezeError(int nodesNumber, double e)
        {
            double delta = (XLast - XFirst) / nodesNumber;
            nodesNumberInt = nodesNumber;
                           
              double  err = nodesNumberInt * Math.Pow(delta , 3.0) / 12;

                if (err >= e)
                {
                    nodesNumberInt++;
                    CountTrapezeError(nodesNumberInt, e);
                }
                else
                {
                    IntegralTrap =  IntegrateTrapeze(nodesNumberInt, delta);
                }

            return nodesNumberInt;
        }
    }
}