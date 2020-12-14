using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathematics.classes
{

   
    class Approximation
    {
        //    double[] x = new double[]{ -1.4, -1.0, -0.6, -0.2, 0.2, 0.6, 1.0, 1.4, 1.8 };
        //double[] x = new double[] { -1.4E+0, -1.0E+0, -0.6E+0, -0.2E+0, 0.2E+0, 0.6E+0, 1.0E+0, 1.4E+0, 1.8E+0 };
        //double[] y = new double[] { 0.17E+0, 0.54E+0, 0.825E+0, 0.98, 0.98, 0.825, 0.54, 0.17, -0.227 };
     //   double[] y = new double[] { 0.17E+0, 0.54E+0, 0.225E+0, -0.98, 0.98, 0.825, 0.54, 0.17, -0.227 };
        //double[] x = new double[] { -5.5, -5.3, -4.8, -4.3,   -4.2,  -3.9, -3.3, -2.5, -1.8, -1.3, -0.26, 0.78, 0.85, 1.11,  1.59,  3.17,  3.53,  3.78,  4.8, 5.42 };
        //double[] y = new double[] {2.1, 3.14,  0.18, -0.825, 0.98, 2.6, -1.4, 0.54, -0.17, -0.227, 3.78, 1.25, -2.37, 0.95, -1.2, 0.25, -0.12, -5.28, -2.36, 2.8 };
        //    double[] x = new double[] { -5.5, -5.1, -4.7, -4.3, -1.3, -0.9, -0.5, -0.1, 0.3,  0.7,  1.1,  1.5,  1.9,  2.3, 2.7 };
        //double[] y = new double[] { 2.1, 3.14, 0.18, -0.825,-0.227, 3.78, 1.25, -2.37, 0.95, -1.2, 0.25, -0.12, -5.28, -2.36, 2.8 };
        //    double[] yforPolinomial2 = new double[] { 0.98, 0.825, 0.54, 0.17, -0.227 };
      //  double p;
        double[] error = new double[] { };

        private double[] xsBasic = new double[11];
        private double[] ysBasic = new double[11];

        private double[] xNew = new double[31];
        private double[] yNew = new double[31];
        private int[] ab = new int[] { 2, 4 };
        private double h = 0.2;
        private int j = 31;
        public double[] XNew { get => xNew; set => xNew = value; }
        public double[] YNew
        {
            get => yNew;
            set
            {
                if (YNew == null)
                    YNew = new double[j];
                yNew = value;
            }
        }
        public double[] YsBasic { get => ysBasic; set => ysBasic = value; }
        public double[] XsBasic { get => xsBasic; set => xsBasic = value; }
        public int[] Ab { get => ab; set => ab = value; }
        public double H { get => h; set => h = value; }
       public double[] Error { get => error; set => error = value; }
        public Dictionary<int, double> DeltasY { set; get; } = new Dictionary<int, double>();

        
        public Approximation()
        { }

        public Approximation(double[] xs )
        {
            XsBasic = xs;
        }
        //public double Count_h()
        //{
        //    return (Math.Abs(X[X.Length - 1] - X[0])) / (X.Length-1);
        //}

        public double CountBasicFunction(double x)
        {
            return 1 / (1 - Math.Pow(x, 3));
        }

        // исходная сетка
        public double[] GetBasicXs()
        {
            XsBasic = new double[11];

            for (int i = 0; i < 11; i++)
            {
                XsBasic[i] = Ab[0] + i * H;
            }
            return XsBasic;
        }

        // значения Y исходной сетки
        public double[] GetBasicYs()
        {
            GetBasicXs();

            for (int i = 0; i < XsBasic.Length - 1; i++)
            {
                YsBasic[i] = CountBasicFunction(XsBasic[i]);
            }

            return YsBasic;
        }

        // новая сетка
        //public double[] GetNewXs()
        //{
        //    for (int i = 0; i < 31; i++)
        //    {
        //        XNew[i] = (2 + i * H / 3);
        //    }

        //    GetNewYs();

        //    return XNew;
        //}

        public double[] GetNewXs()
        {
            for (int i = 0; i < 31; i++)
            {
                XNew[i] = (2 + i * H / 3);
            }

            GetNewYs();

            return XNew;
        }

        // значения Y новой сетки
        public double[] GetNewYs()
        {
            for (int i = 0; i < XNew.Length - 1; i++)
            {
                YNew[i] = CountBasicFunction(XNew[i]);
            }
            return YNew;
        }
        // pp - порядок полинома, считаем разницы Y для расчета полинома
        //public Dictionary<int, double> CountDeltaY(int pp)
        //    {
        //    GetBasicYs();
        //    GetNewXs();
        //        //Array.ConstrainedCopy(Y, 4, YforPolinomial2, 0, 5);
        //    List<double> exTmpDeltaY = new List<double>();


        //        for (int i = 0; i < YsBasic.Length-1; i++)
        //        {
        //            List<double> tmpDeltaY = new List<double>();

        //            if (i == 0)
        //            {
        //             //   for (int k = 0; k < pp-1; k++)
        //                    for (int k = 0; k < YsBasic.Length-1; k++)
        //                    {
        //                    exTmpDeltaY.Add(YsBasic[k + 1] - YsBasic[k]);
        //                }
        //                DeltasY.Add(i, exTmpDeltaY[0]);
        //            }
        //            else
        //            {
        //                 for(int j = 0; j< YsBasic.Length - 1 - i; j++)
        //                    {
        //                        tmpDeltaY.Add(exTmpDeltaY[j + 1] - exTmpDeltaY[j]);
        //                    }

        //                    if(tmpDeltaY.Count > 0)
        //                    {
        //                        DeltasY.Add(i, tmpDeltaY[0]);
        //                        exTmpDeltaY = tmpDeltaY;
        //                    }
        //           }
               
        //        }

        //        return DeltasY;
        //    }

        public Dictionary<int, double> CountDeltaY(int pp)
        {
            GetBasicYs();
            GetNewXs();
            //Array.ConstrainedCopy(Y, 4, YforPolinomial2, 0, 5);
            List<double> exTmpDeltaY = new List<double>();


            for (int i = 0; i < YsBasic.Length - 1; i++)
            {
                List<double> tmpDeltaY = new List<double>();

                if (i == 0)
                {
                    //   for (int k = 0; k < pp-1; k++)
                    for (int k = 0; k < YsBasic.Length - 1; k++)
                    {
                        exTmpDeltaY.Add(YsBasic[k + 1] - YsBasic[k]);
                    }
                    DeltasY.Add(i, exTmpDeltaY[0]);
                }
                else
                {
                    for (int j = 0; j < YsBasic.Length - 1 - i; j++)
                    {
                        tmpDeltaY.Add(exTmpDeltaY[j + 1] - exTmpDeltaY[j]);
                    }

                    if (tmpDeltaY.Count > 0)
                    {
                        DeltasY.Add(i, tmpDeltaY[0]);
                        exTmpDeltaY = tmpDeltaY;
                    }
                }

            }

            return DeltasY;
        }
        // pp - порядок полинома, считаем разницы Х для расчета полинома
        //public double CountDeltaXs(double x, int pp)
        // {
        //     double DeltasX = 1.0;

        //        for (int i = 0; i < pp; i++)
        //         {
        //             DeltasX *= (x - XsBasic[i]);

        //        }
        //     return DeltasX;
        // }

        public double CountDeltaXs(double x, int pp)
        {
            double DeltasX = 1.0;

            for (int i = 0; i < pp; i++)
            {
                DeltasX *= (x - XsBasic[i]);

            }
            return DeltasX;
        }

        // считаем факториал
        public double CountFactorial(double n)
        {
            double f = 1.0;

            if (n > 0)
            {
                for (int i = 1; i <= n; i++)
                    f *= i;
            }
            return f;
        }

        // считаем полином заданной степени
          //public double CountPolinomial(double tmpx, int sp)
          //  {
          //     double P = 0.0;
          //      double deltasxl = 0.0;
          //  double yBasic = 0.0;

          //      for (int j = 0; j < sp-1; j++)
          //      {
          //          var dx = CountDeltaXs((tmpx), j + 1);
          //          var fact = CountFactorial(j + 1);
          //          var pow = Math.Pow(H, j + 1);
          //          var dy = DeltasY[j];

          //      deltasxl += (CountDeltaXs((tmpx), j + 1)) / (CountFactorial(j + 1) *
          //              Math.Pow(H, j + 1)) * DeltasY[j];
          //  }

          //      P = YsBasic[0] + deltasxl;


          //      return P;
          //  }

        public double CountPolinomial(double tmpx, int sp)
        {
            double P = 0.0;
            double deltasxl = 0.0;
            double yBasic = 0.0;
            double[] YsBasicAdapt = new double[YsBasic.Length-sp];

            for (int j = 0; j < sp - 1; j++)
            {
                var dx = CountDeltaXs((tmpx), j + 1);
                var fact = CountFactorial(j + 1);
                var pow = Math.Pow(H, j + 1);
                var dy = DeltasY[j];

                deltasxl += dx / fact * pow * dy;
                        
            }

            P = YsBasicAdapt[0] + deltasxl;


            return P;
        }

        // рассчитать новую сетку
        public double[] GetNewGrid(int n, int po)
            {
                if (XNew == null)
                    XNew = new double[n+1];

                double newDelta = (XsBasic[XsBasic.Length - 1] - XsBasic[0]) / n;

                for (int i = 0; i <= n; i++)
                    XNew[i] = XsBasic[0] + newDelta * i;

                CountDeltaY(po);

                GetNewGridValues(n, po);
                return XNew;
            }

        // рассчитать значения в новой сетке
        public double[] GetNewGridValues(int n, int pp)
        {
         //   YNew = new double[n+1];
            for (int i = 1; i < XNew.Length; i++)
            {
                
                YNew[i] = CountPolinomial(XNew[i], pp);
            }
            return YNew;
        }

        // рассчитать погрешность приближения для точки х
        public double CountPolinomialError(double x, int pp)
        {
            double err = 0.0;
         
            err = CountDeltaXs(x, pp) / (CountFactorial(pp) * Math.Pow(H, pp)) * 0.1;
        
            return err;
        }

        // рассчитать погрешность приближения для всех значений новой сетки
        public double[] GetErrors(double[] xs, int n, int pp)
        {
            double[] errs = new double[n+1];

            for (int i = 0; i < n; i++)
            {
                errs[i] = CountPolinomialError(xs[i], pp);
             }
            return errs;
        }

       
    }
}
