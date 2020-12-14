using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Mathematics.classes;

namespace Mathematics.views
{
      public partial class Matrices : Window
    {
        
        public Matrices()
        {
            InitializeComponent();
            Loaded += outputData;
        }

        private void outputData(object sender, RoutedEventArgs e)
        {
            classes.Matrices mrx = new classes.Matrices();

            List<double> checkMr = new List<double>();
            List<double> sth = new List<double>();

            mrx.CountMatrixBC();
            mrx.CountEquations(mrx.MatrixB, mrx.YMatrixA);
            mrx.InverseMatrix();

            checkMr = mrx.CheckResults(mrx.MatrixA, mrx.YMatrixA).OfType<double>().ToList();
            sth = mrx.MultiplyMatrices(mrx.TransposeMatrix(mrx.MatrixA), mrx.MatrixT).OfType<double>().ToList();

            det.Content = "Детерминант: "+mrx.CountDet();

            AMatrix.ItemsSource = OutputMatrix(mrx.MatrixA);
            yA.ItemsSource = mrx.YMatrixA.OfType<double>().ToList();
            BMatrix.ItemsSource = OutputMatrix(mrx.MatrixB);
            CMatrix.ItemsSource = OutputMatrix(mrx.MatrixC); 
            x.ItemsSource = mrx.X.OfType<double>().ToList(); 
            yB.ItemsSource = mrx.Y.OfType<double>().ToList();
         //   transM.ItemsSource = OutputMatrix(mrx.InverseMatrix());
            reversM.ItemsSource = OutputMatrix(mrx.MatrixT);
            reversMY.ItemsSource = OutputMatrix(mrx.MatrixYtoPresent);
         //   reversMY.ItemsSource = OutputMatrix(mrx.InverseMatrix());
            checkM.ItemsSource = checkMr;

            var mtr = mrx.MultiplyMatrices(mrx.TransposeMatrix(mrx.MatrixA), mrx.MatrixT);
           
            forSth.ItemsSource = OutputMatrix(mtr);
            errorM.ItemsSource = OutputMatrix(mrx.GetErrorMatrices(mtr));

            MatrixNorma.Content = "Норма матрицы: " + mrx.CountMatrixNorma();
            VectorNorma.Content = "Норма вектора: " + mrx.CountVectorNorma();
        }

        // преобразуем матрицу в виде двумерного массива в три коллекции объектов для 
        // представления в разметке
       public List<matrix> OutputMatrix(double[,] ar)
        {
            classes.Matrices mrx = new classes.Matrices();
            List<matrix> mList = new List<matrix>();
            double[] m;

            for (int i = 0; i < ar.GetLength(0); i++)
            {
                m = new double[ar.GetLength(0)];

                for (int j = 0; j < ar.GetLength(0); j++)
                {
                  //  m[j] = ar[j, i];
                    m[j] = ar[i,j];
                }
                mList.Add(new matrix(m));
            }
            return mList;
        }
    }
}
