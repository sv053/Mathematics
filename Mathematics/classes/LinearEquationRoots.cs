using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mathematics.classes
{
    public class Root
    {
        public double X { set; get; }
        public double Y { set; get; }
        public double ErrX { set; get; }
        public double ErrY { set; get; }
        public int Att { set; get; }
        public int Iter { set; get; }
        public double Sjod { set; get; }
        public Root(double x, double y, double err, double ery, int at, int it, double sj)
        {
            X    = x;
            Y    = y;
            ErrX = err;
            ErrY = ery;
            Att  = at;
            Iter = it;
            Sjod = sj;
        }
    }
   public class LinearEquationRoots
    {
        public List<double> x = new List<double>();
        public List<double> y = new List<double>();
        //List<List<double>> roots = new List<List<double>>();
        //double[] yNew = new double[100];
        List<double> rootData = new List<double>();
        //List<double> justRootsX = new List<double>();
        //List<double> justRootsY = new List<double>();
        //List<double> justErrors = new List<double>();
        //List<double> justAttempts = new List<double>();
        int k = 0;
        //List<double> sjod = new List<double>();

        public LinearEquationRoots()
        {}

        public List<double> X { get => x; set => x = value; }
        public List<double> Y { get => y; set => y = value; }
     //   public List<List<double>> Roots { get => roots; set => roots = value; }
        public List<double> RootData { get => rootData; set => rootData = value; }
     
    //    public List<double> Sjod { get => sjod; set => sjod = value; }

        // заданная функция
        public double CountBasicFunction(double x)
        {
            return x * Math.Sin(x) - 1;
        }

        // ищем начальную сетку с заданной точностью
        public List<double> GetX(double a, double b, double delta)
        {
            for (int i = 0; i < (Math.Abs(b - a) / delta); i++)
            {
                X.Add(a + i * delta);
            }
                            
            return X;
        }

        // ищем значения функции в узлах начальной сетки
        public List<double> GetY(List<double> xs)
        {
            foreach (double xi in xs)
                Y.Add(CountBasicFunction(xi));

            return Y;
        }

        // ищем корень на заданном интервале
        public Root LocateRoot(double a, double b, double eps, double epsy)
        {
          
            double tmp = 0.0, root = 0.0, err = 0.0, shod=0.0, shod1=0.0, shod2=0.0,  errY = 0.0;
            int iter = 0;
          
            if (CountBasicFunction(a) * CountSecondDerivative(a) > 0)
            {
                iter++;
                tmp = a;
            }
            else
            {
                iter++;
                tmp = b;
            }
            do
            {
                iter++;
                shod2 = Math.Abs(tmp - root);
                k++;
              //  shod = 0.0;
                root = tmp;
                tmp = tmp - (CountBasicFunction(tmp) / CountFirstDerivative(tmp));
                k+=3;
                shod1 = Math.Abs(tmp - root);
                k++;
                shod = shod1 / (shod2*shod2);
                k++;
                
            }
            while (Math.Abs(tmp - root) > eps || Math.Abs(CountBasicFunction(tmp)) > epsy) ;

            err = Math.Abs(tmp - root);
            errY = Math.Abs(CountBasicFunction(tmp) - epsy);
            Root tmpRoot = new Root(tmp, CountBasicFunction(tmp), err, errY, k, iter, shod);
           
            return tmpRoot;
        }
        
        // первая производная функции
        public double CountFirstDerivative(double x)
        {
            return x * Math.Cos(x) + Math.Sin(x);
        }

        // вторая производная функции
        public double CountSecondDerivative(double x)
        {
            return 2 * Math.Cos(x) - x * Math.Sin(x);
        }

        // ищем все корни на заданной сетке
        public ObservableCollection<Root> CountRoots(double a, double b, double delta, double eps, double epsY)
        {
            ObservableCollection<Root> roots = new ObservableCollection<Root>();

            for (int i = 0; i < (Math.Abs(b - a) / delta); i++)
            {
                double temp_a = a + i * delta;
                double temp_b = a + (i + 1) * delta;

                if (CountBasicFunction(temp_a) * CountBasicFunction(temp_b) < 0)
                {
                    roots.Add(LocateRoot(temp_a, temp_b, eps, epsY));
                     
                }
            }
            return roots;
        }
    }
}
