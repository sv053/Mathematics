using System;
using System.Collections.Generic;
using System.Windows;
using Mathematics.classes;


namespace Mathematics.views
{
    public partial class Integration : Window
    {
        public Integration()
        {
            InitializeComponent();

            Loaded += loadValues;
            Unloaded -= unloadValues;
        }
 
        private void loadValues(object sender, RoutedEventArgs e)
        {
           classes.Integration integr = new classes.Integration(0.0001, 11, 0.0, 2.0);

           List<classes.Integration> objIntegr = new List<classes.Integration>();

            //BasicX.ItemsSource = integr.GetxInitialNodes();

            //BasicY.ItemsSource = integr.GetyInitialNodes();
           

            trapez1.Text = "Необходимое количество узлов для достижения заданной точности(метод трапеций): "+integr.CountTrapezeError(17, 0.0001).ToString();
            trapez2.Text = "Значение интеграла методом трапеций: " + integr.Integral.ToString("E");

            gauss1.Text = $"Значение интеграла методом Гаусса: " + integr.CountGaussError(integr.XFirst, integr.XLast).ToString("E");
            gauss2.Text = "Количество узлов методом Гаусса: " + integr.IntegrateGauss(integr.XFirst, integr.XLast, 5).ToString("E") + "; nodes = "+ integr.NodesCount;
           
            gauss3.Text = "Погрешность интеграла методом Гаусса: " + integr.GaussError.ToString("E");


            n.Text = "Начальное количество узлов: " + integr.NodesNumber;
            bounds.Text = "Границы интервала: [" + integr.XFirst + " ;" + integr.XLast + " ]";
            eps.Text = "Относительная точность: " + integr.Eps.ToString("E");
            delt.Text = "Шаг сетки: " + ((integr.XLast - integr.XFirst) / integr.NodesNumber).ToString("E");
        }

        private void unloadValues(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void PrintResult()
        {
            string res = "Значение интеграла методом трапеции - ";
       }
    }
}
