using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathematics.classes
{
    class Matrices
    {
        List<List<double>> basicMatrix = new List<List<double>>();

          double[,] matrixA = new double[,] { { 6, -1, -1 }, { -1, 6, -1 }, { -1, -1, 6 } };
        double[,] matrix1 = new double[,] { { 1.0, 0, 0 }, { 0, 1.0, 0 }, { 0, 0, 1.0 } };
        //   double[,] matrixA = new double[,] { { 5, 8, 1 }, { 3, -2, 6 }, { 2, 1, -1 } };
        double[] yMatrixA = new double[] {-11.33, 32, 42};
     //   double[] yMatrixA = new double[] { 2, -7, -5 };
        double[,] matrixB = new double[3, 3];
        double[,] matrixC = new double[3, 3];
        double[,] matrixT = new double[3, 3];
        int n;
        double[] x = new double[3];
        double[] y = new double[3];
        double[,] eMatrix = new double[3, 3];
        List<double> epsilon = new List<double>();
        double[,] matrixY = new double[,] { };
        double[,] product;

        public double[,] MatrixB { get => matrixB; set => matrixB = value; }
        public double[,] MatrixC { get => matrixC; set => matrixC = value; }
        public int N { get => n; set { n = value; } }
        public double[,] MatrixA { get => matrixA; set => matrixA = value; }
        public double[] X { get => x; set => x = value; }
        public double[] Y { get => y; set => y = value; }
        public double[] YMatrixA { get => yMatrixA; set => yMatrixA = value;}
        public double[,] MatrixT { get => matrixT; set => matrixT = value; }
        public double[,] EMatrix { get => eMatrix; set => eMatrix = value; }
        public List<double> Epsilon { get => epsilon; set => epsilon = value;}
        public double[,] MatrixY { get => matrixY; set => matrixY = value; }

        public double[,] MatrixYtoPresent { get => matrixY; set => matrixY = value; }
        public double[,] Product { get => product; set => product = value; }
        public double[,] Matrix1 { get => matrix1; set => matrix1 = value; }

        // раскладываем матрицу а на матрицы b и c
        public void CountMatrixBC()
        {
            N = MatrixA.GetLength(0);

            for (int j = 0; j < N; j++)
            {
                for (int i = 0; i < N; i++)
                {
                    double tmp1 = 0.0, tmp2 = 0.0;

                    for (int k = 0; k < j; k++)
                    {
                        tmp1 += MatrixB[i, k] * MatrixC[k, j];
                        tmp2 += MatrixB[j, k] * MatrixC[k, i];
                    }

                    MatrixB[i, j] = MatrixA[i, j] - tmp1;

                    if (MatrixB[j, j] != 0)
                        MatrixC[j, i] = 1.0 / MatrixB[j, j] * (MatrixA[j, i] - tmp2);
                    else MatrixC[j, i] = 0;
                }
            }
        }

        // считаем детерминант матрицы
        public double CountDet()
        {
            double det = 1.0;

            for (int i = 0; i < N; i++)
                det *= MatrixB[i, i];

            return det;
        }

        // находим матрицу
        public void CountEquations(double[,] MatrB, double[] YMatrA)
        {
            double tmp;

            Y[0] = YMatrA[0] / MatrB[0, 0];
            Y[1] = 1.0 / MatrB[1, 1] * (YMatrA[1] - MatrB[1, 0] * Y[0]);
            Y[2] = 1.0 / MatrB[2, 2] * (YMatrA[2] - MatrB[2, 0] * Y[0] - MatrB[2, 1] * Y[1]);

            for (int i = n - 1; i >= 0; i--)
            {
                tmp = 0;
                for (int k = i + 1; k < n; k++)
                {
                    tmp += MatrixC[i, k] * X[k];
                }
                X[i] = Y[i] - tmp;
            }
        }

        // проверяем правильность расчёта матрицы
        public double[] CheckResults(double[,] arr1, double[] arr2)
        {
            double[] check = new double[N];

            for (int i = 0; i < n; i++)
            {
                check[i] = 0;
                for (int j = 0; j < n; j++)
                    check[i] += arr1[i, j] * X[j];

                check[i] = arr2[i] - check[i];
            }
            return check;
        }

        // находим е-матрицу
        public double[,] CountEMatrix()
        {

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i == j) EMatrix[i, j] = 1;
                    else EMatrix[i, j] = 0;
                }
            }
            return EMatrix;
        }

        // транспонируем матрицу
        public double[,] TransposeMatrix(double[,] matrix)
        {
            double[,] newMtr = new double[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    newMtr[j, i] = matrix[i, j]*(-1);
                }
            }
            return newMtr;
        }

        // считаем обратную матрицу 
        public double[,] InverseMatrix()
        {
            MatrixY = new double[N,N];

            CountEMatrix();

         //   double tmp = 0.0;
            
            for (int i = 0; i < N; i++)
            {

                MatrixY[0, i] = EMatrix[0, i] / MatrixB[0, 0];
                MatrixY[1, i] = (EMatrix[1, i] - MatrixB[1, 0] * MatrixY[0, i]) / MatrixB[1, 1];
                MatrixY[2, i] = (EMatrix[2, i] - MatrixB[2, 0] * MatrixY[0, i] - MatrixB[2, 1] * MatrixY[1, i]) / MatrixB[2, 2];

                MatrixT[2, i] = MatrixY[2, i];
                MatrixT[1, i] = MatrixY[1, i] - MatrixC[1, 2] * MatrixT[2, i];
                MatrixT[0, i] = MatrixY[0, i] - MatrixC[0, 1] * MatrixT[1, i] - MatrixC[0, 2] * MatrixT[2, i];
            }
            /* 
             for (int i = 0; i < N; i++)
             {
                 for (int j = 0; j < N; j++)
                 {
                     for (int k = 0; k <= j; k++)
                     {
                         tmp += MatrixB[i, k] * MatrixY[k, j];
                     }

                     MatrixY[j, i] = (EMatrix[j, i] - tmp) / MatrixB[i, i];
                 }
             }

             tmp = 0.0;

             for (int i = 0; i < N; i++)
             {
                 for (int j = 0; j < N; j++)
                 {
                     for (int k = 0; k <= j; k++)
                     {
                         tmp += MatrixC[i, k] * MatrixT[k, j];
                     }
                     MatrixT[j, i] = MatrixY[j, i] - tmp;
                 }
             }
           */
            MatrixYtoPresent = MatrixY;
        //    MatrixY = TransposeMatrix(MatrixY);
            MatrixT = TransposeMatrix(MatrixT);

            return MatrixY;

        }
        
        // перемножаем матрицы
        public double[,] MultiplyMatrices(double[,] mtr1, double[,] mtr2)
        {
            Product = new double[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        Product[j,i] += mtr1[k, i] * mtr2[j, k];
                    }
                }
            }
            return Product;
        }

        // матрица невязки
        //public double[,] GetErrorMatrices(double[,] mtr)
        //{
        //    double[,] err_mtr = new double[N, N];

        //    for (int i = 0; i < N; i++)
        //    {
        //        for (int j = 0; j < N; j++)
        //        {
        //            if (i == j)
        //                err_mtr[i, j] = mtr[i, j] - 1.0;
        //            else
        //                err_mtr[i, j] = mtr[i, j];
        //        }
        //    }
        //    return err_mtr;
        //}

        public double[,] GetErrorMatrices(double[,] mtr)
        {
            double[,] err_mtr = new double[,]{ { 0,0,0}, {0,0,0}, {0,0,0} };

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    err_mtr[i, j] = mtr[i, j] - Matrix1[i,j];
                   
                }
            }
            return err_mtr;
        }

        // проверяем обратную матрицу
        public List<double> CheckInverseMatrix()
        {
            double[,] tmpT = new double[N, N];
            double[] tmpY = new double[N];
            List<double> t = new List<double>();
            
            CountEMatrix();

            tmpT = MatrixT;
        //    MatrixY = TransposeMatrix(MatrixY);
               
            for (int i = 0; i < N; i++)
            {
               Array.Clear(tmpY, 0, tmpY.Length);

                for (int j = 0; j < N; j++)
                {
                    tmpY[j] = MatrixY[i, j];
                }
                t = CheckResults(tmpT, tmpY).OfType<double>().ToList();
        
                for (int j = 0; j < N; j++)
                {
                     Epsilon.Add(t[j]);
                }
            }
        return Epsilon;
        }

        public double CountMatrixNorma()
        {
            double norma = 0;

            for (int j = 0; j < N; j++)
            {
                for (int k = 0; k < N; k++)
                {
                    norma += Math.Pow(matrixA[j, k], 2);
                }
            }

            return Math.Sqrt(norma);
        }

        public double CountVectorNorma()
        {
            double norma = 0;

            CheckInverseMatrix();

            for (int j = 0; j < N; j++)
            {
               norma += Math.Pow(Epsilon[j], 2);
               
            }

            return Math.Sqrt(norma);
        }
    }
}
