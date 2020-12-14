using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Mathematics.classes;
using System.Windows.Media;

namespace Mathematics.views
{
    public partial class Approx : Window
    {

        classes.Approximation approx = new classes.Approximation();
        int PO = 1;

        public Approx()
        {
            InitializeComponent();
                        
            Loaded += outputData;
            Loaded += drawGraphics;
            PO = approx.XsBasic.Length-1;
            this.DataContext = this;
        }

        private void outputData(object sender, RoutedEventArgs e)
        {
           InterpGen interpGen = new InterpGen();
            interpGen.StartGettingNewPoints(PO);
            List<InterpUnit> newValues = interpGen.NewPoints;
            List<BasicPoint> newBasicValues = interpGen.BasicPoints;

            dgForInterp.ItemsSource = newValues;
            poLbl.Content = "Порядок полинома - "+PO;

         }

        private void drawGraphics(object sender, RoutedEventArgs e)
        {
            InterpGen interpGen = new InterpGen();
            interpGen.StartGettingNewPoints(PO);
           
            List<double> basicxs = new List<double>();
            List<double> basicys = new List<double>();
            List<double> newxs = new List<double>();
            List<double> newys = new List<double>();

            foreach (var v in interpGen.BasicPoints)
            {
                basicxs.Add(v.X);
                basicys.Add(v.Y);
            }
            foreach (var v in interpGen.NewPoints)
            {
                newxs.Add(v.XNew);
                newys.Add(v.YNewPol);
            }
            double w = drawArea.RenderSize.Width; //получаем координату х 
            double h = drawArea.RenderSize.Height; //и координату у центра холста

            Graphic graphic = new Graphic(w, h, basicxs.ToArray(), basicys.ToArray());

            graphic.BuildAxis();
            drawArea.Children.Add(graphic.Ox);
            drawArea.Children.Add(graphic.Oy);
            drawArea.Children.Add(graphic.OxArrow);
            drawArea.Children.Add(graphic.OyArrow);

            graphic.BuildGraphic(5);
                        
            Canvas.SetLeft(graphic.shp, graphic.W / 2-200);
            Canvas.SetTop(graphic.shp, graphic.H / 2);
            drawArea.Children.Add(graphic.shp);
            //-------------------------------------------------------------------------------

            Graphic graphicNew = new Graphic(w, h, newxs.ToArray(), newys.ToArray());

                        graphicNew.BuildGraphic(70);

                        Canvas.SetLeft(graphicNew.shp, graphicNew.W / 2-200);
                        Canvas.SetTop(graphicNew.shp, graphicNew.H / 2);
                        drawArea.Children.Add(graphicNew.shp);
           
            //---------------------------------------------------------------------
      }
        // пересчёт с заданным порядком полинома
        private void PoBtn_Click(object sender, RoutedEventArgs e)
        {
            InterpGen interpGen1 = new InterpGen();

            if (po.Text != null)
            {
               
                if (int.TryParse(po.Text, out int ord))
                {
                    if ((ord > 1) && (ord <= PO))
                    {
                        poLbl.Content = "Порядок полинома - " + ord.ToString();
                        
                        interpGen1.StartGettingNewPoints(ord);
                         dgForInterp.ItemsSource = interpGen1.NewPoints;
                     }
                }
                else
                    {
                    poLbl.Content = "";
                    int p = PO - 1;
                        poLbl.Content = "Выбрать число из интервала [2,"+ p +"]";
                    }
                
            }
        }
    }
}
