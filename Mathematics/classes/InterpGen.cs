using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics.classes
{
    class InterpUnit
    {

        public double XNew { get; set; } = new double();
        public double YNew { get; set; } = new double();
        public double YNewPol { get; set; } = new double();
        public double Err { get; set; } = new double();

        public InterpUnit(double x, double y, double y_pol=0,  double e=0.0)
        {
            XNew = x;
            YNew = y;
            YNewPol = y_pol;
            Err = e;
        }
    }

    class BasicPoint
    {

        public double X { get; set; } = new double();
        public double Y { get; set; } = new double();
      
        public BasicPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    class InterpGen
    {
        public List<InterpUnit> NewValuesListForGrid { get; set; } = new List<InterpUnit>();

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
           
            public double H { get => h; set => h = value; }
          

        public List<BasicPoint> BasicPoints { set; get; } = new List<BasicPoint>();
        public List<InterpUnit> NewPoints { set; get; } = new List<InterpUnit>();

        public double CountBasicFunction(double x) // расчёт основной функции
            {
                return 1 / (1 - Math.Pow(x, 3));
            }
                 
        public List<BasicPoint> GetBasicPoints() // получить точки исходной сетки
        {
         
            for (int i = 0; i < 11; i++)
            {
                double XBasic = 2 + i * H;
                double YBasic = CountBasicFunction(XBasic);
                BasicPoints.Add(new BasicPoint(XBasic, YBasic));
            }
            return BasicPoints;
        }

        public void StartGettingNewPoints(int po)
        {

            int pp = po - 1;

            if (BasicPoints.Count == 0)
                GetBasicPoints(); // получаем список объектов точек исходной сетки
            List<BasicPoint> basicPoints = BasicPoints;

            GetNewPoints(basicPoints, po);
        }
       
        // получаем список объектов точек новой сетки
        public List<InterpUnit> GetNewPoints(List<BasicPoint> bp, int po)
        {
            int pp = po - 1;
          
            for (int i = 0; i < 31; i++)
            {
               double XNew = (2 + i * H / 3);
               double YNew = CountBasicFunction(XNew);

            //    var validXs = BasicPoints.Where(point => point.X > XNew).ToList();
                var validX = bp.Where(point => point.X >= XNew).FirstOrDefault(); // ищем х больше текущего

                if (validX != null)  // если он существует,
                {
                    if(bp.IndexOf(validX) < pp) // и находится в интервале расчёта полинома
                    {
                        double[] xbase = new double[po];  // создаём массивы для расчёта полиномов
                        double[] ybase = new double[po];

                        for(int j = 0; j<pp; j++)
                        {
                            xbase[j] = bp[j].X;  // и копируем в них нужные для расчёта полиномов значения
                            ybase[j] = bp[j].Y;
                        }

                        double YNewPol = CountPolinomial(xbase, ybase, XNew, pp);
                        double errs = CountPolinomialError(xbase, XNew, pp);

                        NewPoints.Add(new InterpUnit(XNew, YNew, YNewPol, errs));
                    }
                    else   // если х находится за пределами значений, подходящих для расчёта полинома
                    {
                       for(int l = 0; l<pp; l++)
                        {
                            if(bp.First() != null)
                                bp.RemoveAt(0);   // удаляем неподходящие более значения 
                        }

                        if(bp.Count > 0)
                        {

                                double[] xbase;
                                double[] ybase;

                                int restX = bp.Count - pp; // проверяем, покрывает ли степень полинома оставшийся ряд значений

                                if(restX >= 0 )  // если значений больше, чем заданная степень полинома,
                                {
                                    xbase = new double[po]; 
                                    ybase = new double[po];

                                    for (int k = 0; k < pp; k++)
                                        {
                                            xbase[k] = bp[k].X;  // готовим массивы для расчёта полиномов
                                            ybase[k] = bp[k].Y;
                                        }

                                double YNewPol = CountPolinomial(xbase, ybase, XNew, pp);  // получаем значение полинома
                                double errs = CountPolinomialError(xbase, XNew, pp);      // и считаем для него погрешность

                                NewPoints.Add(new InterpUnit(XNew, YNew, YNewPol, errs));
                                }
                                else
                                {
                                    xbase = new double[bp.Count];  // если значений меньше, чем степень полинома
                                    ybase = new double[bp.Count];

                                    for (int p = 0; p < bp.Count ; p++)
                                    {
                                        xbase[p] = bp[p].X;   // тоже готовим массивы, но
                                        ybase[p] = bp[p].Y;

                                        pp = bp.Count; // меняем степень полинома исходя из остатка значений для расчёта
                                    }

                                double YNewPol = CountPolinomial(xbase, ybase, XNew, pp);
                                double errs = CountPolinomialError(xbase, XNew, pp);

                                NewPoints.Add(new InterpUnit(XNew, YNew, YNewPol, errs));
                            }
                        }
                    }
                }
            }
            return NewPoints;
        }
     
        public Dictionary<int, double> CountDeltaY(double[] ysBase, int pp)
        {
            Dictionary<int, double> deltasY = new Dictionary<int, double>();
            //Array.ConstrainedCopy(Y, 4, YforPolinomial2, 0, 5);
            List<double> exTmpDeltaY = new List<double>();


            for (int i = 0; i < ysBase.Length - 1; i++)
            {
                List<double> tmpDeltaY = new List<double>();

                if (i == 0)
                {
                    for (int k = 0; k < ysBase.Length - 1; k++)
                    {
                        exTmpDeltaY.Add(ysBase[k + 1] - ysBase[k]);
                    }
                    deltasY.Add(i, exTmpDeltaY[0]);
                }
                else
                {
                    for (int j = 0; j < ysBase.Length - 1 - i; j++)
                    {
                        tmpDeltaY.Add(exTmpDeltaY[j + 1] - exTmpDeltaY[j]);
                    }

                    if (tmpDeltaY.Count > 0)
                    {
                        deltasY.Add(i, tmpDeltaY[0]);
                        exTmpDeltaY = tmpDeltaY;
                    }
                }
            }

            return deltasY;
        }

       public double CountDeltaXs(double[]xsBase, double x, int pp)
        {
            double deltasX = 1.0;

               for (int i = 0; i < pp; i++)
                {
                deltasX *= (x - xsBase[i]);

            }
            return deltasX;
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
            public double CountPolinomial(double[] xsBase, double[] ysBase, double tmpx, int sp)
            {
                double P = 0.0;
                double deltasxl = 0.0;

                for (int j = 0; j < sp - 1; j++)
                {
                    var dx = CountDeltaXs(xsBase, tmpx, j + 1);
                    var fact = CountFactorial(j + 1);
                    var pow = Math.Pow(H, j + 1);
                    var tmp = CountDeltaY(ysBase, sp);
                    var dy = CountDeltaY(ysBase, sp)[j];
               
                deltasxl += dx / (fact * pow) * dy;
                           
                }
            P = ysBase[0] + deltasxl;

            return P;
            }

            // рассчитать погрешность приближения для точки х
            public double CountPolinomialError(double[] xsBase, double x, int pp)
            {
                double err = 0.0;

                err = CountDeltaXs(xsBase, x, pp) / (CountFactorial(pp) * Math.Pow(H, pp)) * 0.1;

                return err;
            }
        }
    }

