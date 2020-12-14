using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace Mathematics.classes
{
    class Graphic
    {
        PointCollection vertices;
        double w;
        double h;

        public Polygon OxArrow = null, OyArrow = null;
        public Line Ox = null, Oy = null;
        public Polyline shp = null;

        public PointCollection Vertices
        {
            get => vertices;
            set => vertices = value;
        }

        public double W { get => w; set => w = value; }
        public double H { get => h; set => h = value; }

        public Graphic(double width, double height, double[] x, double[] y)
        {
            W = width;
            H = height;
            // вершины исходного многоугольника
            Vertices = new PointCollection();

            for (int i = 0; i < x.Length; i++)
            {
                Vertices.Add(new Point(x[i]*(90), y[i]*(-130)));
            }
        }

        public Graphic(double width, double height, List<double> x, List<double> y)
        {
            W = width;
            H = height;
            // вершины исходного многоугольника
            Vertices = new PointCollection();

            var newPC = x.Join(y, s => x.IndexOf(s), i => y.IndexOf(i), (s, i) => new { sv = s, iv = i }).ToList();

            foreach (var q in newPC)
            {
              Vertices.Add(new Point(q.sv * 5, q.iv * 5));
             //   Vertices.Add(new Point(q.sv , q.iv ));
            }
     
        }

        public Polyline BuildGraphic(byte c)
        {
           Random rnd = new Random();

            SolidColorBrush scb = new SolidColorBrush(System.Windows.Media.Color.FromArgb((byte)(255-c), (byte)rnd.Next(0, 255-c), (byte)rnd.Next(100, 255 - (byte)rnd.Next(0, 154)), (byte)rnd.Next(0, 255-c)));

            shp = new Polyline()
            {
                StrokeThickness = 3,
                Points = Vertices,
                Stroke = scb
            };

          return shp;
        }

        // формируем оси координат со стрелками
        public void BuildAxis()
        {
            // ось абсцисс
            Ox = new Line()
            {
                StrokeThickness = 1,
                Stroke = Brushes.Gray,
                X1 = 5,
                X2 = W - 5,
                Y1 = H / 2,
                Y2 = H / 2
            };
            // ось ординат
            Oy = new Line()
            {
                StrokeThickness = 1,
                Stroke = Brushes.Gray,
                X1 = W / 2,
                X2 = W / 2,
                Y1 = 5,
                Y2 = H - 5
            };
            // стрелка оси абсцисс
            OxArrow = new Polygon()
            {
                StrokeThickness = 1,
                Stroke = Brushes.Gray,
                Fill = Brushes.Gray,
                Points = { new Point(W - 5, H / 2), new Point(W - 15, H / 2 - 5), new Point(W - 15, H / 2 + 5) }
            };
            // стрелка оси ординат
            OyArrow = new Polygon()
            {
                StrokeThickness = 1,
                Stroke = Brushes.Gray,
                Fill = Brushes.Gray,
                Points = { new Point(W / 2, 5), new Point(W / 2 - 4, 15), new Point(W / 2 + 4, 15) }
            };

        }
    }
}
