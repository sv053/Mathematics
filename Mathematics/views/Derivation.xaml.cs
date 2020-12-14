using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Mathematics.classes;

namespace Mathematics.views
{
    public partial class Derivation : Window
    {
        classes.Derivation der = new classes.Derivation();
        int PO;
        public Derivation()
        {
            InitializeComponent();

            this.Loaded += drawGraphics;
            Loaded += outputData;

            PO = der.XsBasic.Length - 2;
        }

        private void outputData(object sender, RoutedEventArgs e)
        {
             BasicX.ItemsSource = der.GetBasicXs();
             BasicY.ItemsSource = der.GetBasicYs();

            var newXs = der.GetNewXs();
            XNew.ItemsSource = newXs;

            Fderiv.ItemsSource = der.GetFirstDerivative(PO);
            var firstErr = der.GetErrors(newXs, false, PO);
            FDerivErr.ItemsSource = firstErr;

            secDeriv.ItemsSource = der.GetSecondDerivative(PO);
            var secErr = der.GetErrors(newXs, true, PO);
            secDerivErr.ItemsSource = secErr;

            //data.Text = "Погрешность 1-ой производной: "+der.GetErrors(der.XNew, 30, PO).ToString("E");
            //format.Text = "Погрешность 2-ой производной: " + der.GetErrors(der.XNew, 30, PO).ToString("E");
            data.Text = "Погрешность 1-ой производной: " + firstErr.First();
            format.Text = "Погрешность 2-ой производной: " + secErr.First();
            //   format.Text = PO.ToString();
        }

        private void drawGraphics(object sender, RoutedEventArgs e)
        {
            der.GetBasicYs();
            der.GetNewXs();

        double w = drawArea.RenderSize.Width-50; //получаем координату х 
        double h = drawArea.RenderSize.Height; //и координату у центра холста

        Graphic graphic = new Graphic(w, h, der.XsBasic, der.YsBasic);

        graphic.BuildAxis();
        drawArea.Children.Add(graphic.Ox);
        drawArea.Children.Add(graphic.Oy);
        drawArea.Children.Add(graphic.OxArrow);
        drawArea.Children.Add(graphic.OyArrow);

        graphic.BuildGraphic(5);

        Canvas.SetLeft(graphic.shp, graphic.W / 2-200);
        Canvas.SetTop(graphic.shp, graphic.H / 2);
        drawArea.Children.Add(graphic.shp);

        }

        private void poBtn1_Click(object sender, RoutedEventArgs e)
        {
            int ord = 1;
            classes.Derivation deriv = new classes.Derivation();

            if (po1.Text != null)
            {
                
                if (int.TryParse(po1.Text, out ord))
                {
                 
                    if ((ord > 0) && (ord < PO))
                    {
                        BasicX.ItemsSource = deriv.GetBasicXs();
                        BasicY.ItemsSource = deriv.GetBasicYs();
                        var newXs = der.GetNewXs();

                        Fderiv.ItemsSource = deriv.GetFirstDerivative(ord);
                        var firstErr = deriv.GetErrors(newXs, false, ord);
                        FDerivErr.ItemsSource = firstErr;

                        secDeriv.ItemsSource = deriv.GetSecondDerivative(ord);
                        var secErr = deriv.GetErrors(newXs, true, ord);
                        secDerivErr.ItemsSource = secErr;

                        data.Text = "" ;
                        format.Text = "" ;

                        poLbl1.Content = "" ;

                        data.Text = "Погрешность 1-ой производной: " + firstErr.First();
                        format.Text = "Погрешность 2-ой производной: " + secErr.First();

                        poLbl1.Content = "Порядок полинома - " + ord.ToString();

                    }
                    else
                    {
                      //  data.Text = "no way";
                        poLbl1.Content = "Выбрать число из интервала [1; " + PO + "]";
                    }
                }
                

            }
        }
    }
}
