using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Mathematics.classes;

namespace Mathematics.views
{
    public partial class Roots : Window
    {
       classes.LinearEquationRoots r = new LinearEquationRoots();
       public ObservableCollection<Root> RootsList { set; get; } = new ObservableCollection<Root>();

        public Roots()
        {
            System.Diagnostics.Stopwatch sw = new Stopwatch();
            sw.Start();

            InitializeComponent();
            this.DataContext = this;

            Loaded += OutputData;
            Loaded += drawGraphics;

            sw.Stop();
            processtime.Text = "Время работы программы: " + (sw.ElapsedMilliseconds).ToString() +" мс";
        }

        private void drawGraphics(object sender, RoutedEventArgs e)
        {
            double w = drawArea.RenderSize.Width-150; //получаем координату х 
            double h = drawArea.RenderSize.Height; //и координату у центра холста

            r.GetY(r.GetX(0, 30, 0.5));
        
            Graphic graphic = new Graphic(w, h, r.X, r.Y);

            graphic.BuildAxis();
            drawArea.Children.Add(graphic.Ox);
            drawArea.Children.Add(graphic.Oy);
            drawArea.Children.Add(graphic.OxArrow);
            drawArea.Children.Add(graphic.OyArrow);
            
            graphic.BuildGraphic(5);

            Canvas.SetLeft(graphic.shp, graphic.W / 2);
            Canvas.SetTop(graphic.shp, graphic.H / 2);
            drawArea.Children.Add(graphic.shp);

        }

        private void OutputData(object sender, RoutedEventArgs e)
        {
         //   processtime.Content = "Длительн.выполнения: ";

            //List<List<double>> test = new List<List<double>>();
            //List<LinearEquationRoots> genList = new List<LinearEquationRoots>();

            RootsList = r.CountRoots(2, 30, 0.53, 0.001, 0.0014);
            iRoots.ItemsSource = RootsList;
       
        }
     
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            errMsg.Content = "";

            r.RootData.Clear();
           
            if ((a.Text != null)&&(b.Text != null) && (d.Text != null)  && (pres1.Text != null) && (pres1.Text != null))
            {
               
                double aDouble, bDouble, pr1Double, pr2Double, dDouble;

               
                if (double.TryParse(a.Text, out aDouble))
                {
                  //     a.Text = aDouble.ToString();
                } 
                else
                    errMsg.Content = "Введите число через запятую";
                
                if (double.TryParse(b.Text, out bDouble))
                {
                 //   b.Text = bDouble.ToString();
                }
                else
                    errMsg.Content = "Введите число через запятую";

                if(double.TryParse(d.Text, out dDouble))
                {
                    //   b.Text = dDouble.ToString();
                }
                else
                    errMsg.Content = "Введите число через запятую";

                if (double.TryParse(pres1.Text, out pr1Double))
                {
                  //  pres.Text = prDouble.ToString();
                }
                else
                    errMsg.Content = "Введите число через запятую";

                if (double.TryParse(pres2.Text, out pr2Double))
                {
                    //   pres2.Text = pr2Double.ToString();
                }
                else
                    errMsg.Content = "Введите число через запятую";

                if (typeof(double) == aDouble.GetType() && typeof(double) == bDouble.GetType() && typeof(double) == pr1Double.GetType() && typeof(double) == dDouble.GetType())
                    if (aDouble < bDouble)
                        {
                  
                        classes.LinearEquationRoots r1 = new LinearEquationRoots();
                        RootsList = r1.CountRoots(aDouble, bDouble, dDouble, pr1Double, pr2Double);
                        iRoots.ItemsSource = RootsList;

                     
                    }
                    else
                        errMsg.Content = "Начальное значение должно быть больше конечного";
            }
            else
                errMsg.Content = "Не все значения заполнены верно";
        }

        private void A_GotFocus(object sender, RoutedEventArgs e)
        {
        //    a.Text = "";
         //   a.Foreground;
        }

        private void A_LostFocus(object sender, RoutedEventArgs e)
        {
         //   a.Text = "2";

        }

        private void B_GotFocus(object sender, RoutedEventArgs e)
        {
        //    b.Text = "";
        }

        private void B_LostFocus(object sender, RoutedEventArgs e)
        {
        //    b.Text = "30";
        }

        private void Pres_LostFocus(object sender, RoutedEventArgs e)
        {
         //   pres.Text = "0.0014";
        }

        private void Pres_GotFocus(object sender, RoutedEventArgs e)
        {
        //    pres1.Text = "";
        }

        private void D_GotFocus(object sender, RoutedEventArgs e)
        {
         //   d.Text = "";
        }

        private void D_LostFocus(object sender, RoutedEventArgs e)
        {
        //    d.Text = "0.53";
        }

        private void XRoots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
