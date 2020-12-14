using System;
using System.Collections.Generic;

namespace Mathematics.classes
{
    class Derivation
    {
        private double[] xsBasic = new double[11];
        private double[] ysBasic = new double[11];
        private double[] xNew = new double[31];
        private double[] yNew = new double[31];
        private int[] ab = new int[] { 2, 4 };
        private double h = 0.2;
        private int j = 31;
        private double p;
        double[] error = new double[] { };


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
        public int J { get => j; }
        public double H { get => h; set => h = value; }
        public Dictionary<int, double> DeltasY { set; get; } = new Dictionary<int, double>();
       
        // заданная функция
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
        public double[] GetNewXs()
        {
            for (int i = 0; i < J; i++)
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

        public Dictionary<int, double> CountDeltaY(int pp)
        {
            List<double> exTmpDeltaY = new List<double>();


            for (int i = 0; i < YsBasic.Length - 1; i++)
            {
                List<double> tmpDeltaY = new List<double>();

                if (i == 0)
                {
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

        // рассчитать новую сетку
        public double[] GetNewGrid(int po)
        {
            int n = 31;
         
            XNew = new double[31];
         
            for (int i = 0; i < n; i++)
                XNew[i] = 2 + i * H / 3;

            CountDeltaY(po);

            return XNew;
        }

        // считаем первую производную для одной точки
        public double CountFirstDerivative(double q, double x, int po)
        {
            double PQ = 0.0, h = 0.0, tmpPQ;

            for (int j = 0; j <= po; j++)
            {
                tmpPQ = 0.0;

                for (int k = 0; k < j; k++)
                {
                    double tmpmult = 1.0;
                   
                    for (int l = 0; l < j; l++)
                    {
                        if (k != l)
                            tmpmult *= q - l;

                    }
                    tmpPQ += tmpmult / H;
                }
                PQ += (1 / CountFactorial(j)) * DeltasY[j] * tmpPQ;
            }
            return PQ;
        }

        // считаем первую производную для всех точек
        public List<double> GetFirstDerivative(int po)
        {
            List<double> derivs = new List<double>();
            GetNewGrid(po);

            foreach (var nx in XNew)
            {
               double q = ((nx - XsBasic[0]) / H);
                derivs.Add(CountFirstDerivative(q, nx, po));
            }
            return derivs;
        }
        
        // считаем погрешность первой производной 
        public double CountFirstDerivativeError(int sp)
        {
         //   return (Math.Pow(-1, sp - 1) / Count_h()) * DeltasY[sp - 1] / sp;
            return Math.Pow(-1, sp - 1) / H * DeltasY[sp - 1] / sp;
        }

        // считаем вторую производную для одной точки
        public double CountSecondDerivative(double q, double x, int po)
        {
            double PQ = 0.0;
        
            if (po < 2) po = 2;

            for (int j = 2; j <= po; j++)
            {
                double multisum = 0.0;

                for (int k = 0; k < j; k++)
                {
                    double tmpPQ = 0.0;

                    for (int p = 0; p < j; p++)
                    {
                        if (p != k)
                        {
                            double tmpmult = 1.0;

                            for (int l = 0; l < j; l++)
                            {
                                if (l != k && l != p)
                                    tmpmult *= q - l;
                            }
                            tmpPQ += tmpmult / H;
                        }
                    }
                    multisum += tmpPQ / H;
                }
                PQ += (1 / CountFactorial(j)) * DeltasY[j - 1] * multisum;

            }
            return PQ;
        }

        // считаем вторую производную для всех точек
        public List<double> GetSecondDerivative(int po)
        {
            List<double> derivs = new List<double>();

            foreach (var nx in XNew)
            {
                double q = ((nx - XsBasic[0]) / H);
                derivs.Add(CountSecondDerivative(q, nx, po));
            }

            return derivs;
        }

        // считаем погрешность второй производной для заданной точки
        public double CountSecondDerivativeError(int sp)
        {
            double err = 0.0;
            double sumi = 0.0;

            for (int i = 0; i < sp; i++)
            {
                sumi += 1 / (i + 1);
            }
            err = 2 * Math.Pow(-1, sp) * DeltasY[sp - 1] / (Math.Pow(H, 2) * (sp)) * sumi;

            return err;
        }

        // считаем погрешности для всей сетки
        public List<double> GetErrors(double[] newYs, bool derivOrd, int po, int n = 31)
        {
            List<double> Error = new List<double>();

            for (int i = 0; i < n; i++)
            {

                if (derivOrd == false)
                {
                    Error.Add(CountFirstDerivativeError(po));
                }

                else
                {
                    Error.Add(CountSecondDerivativeError(po));
                }
            }
            return Error;
        }


    }
}
