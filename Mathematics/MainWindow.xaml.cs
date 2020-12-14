using System.Collections.Generic;
using System.Windows;

namespace Mathematics
{
    public partial class MainWindow : Window
    {
        classes.Approximation approx = new classes.Approximation();
        classes.Integration integration = new classes.Integration(0.0001, 11, 0, 2);

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += outputFunctionValue;
        }
        
        private void outputFunctionValue(object sender, RoutedEventArgs e)
        {
            List<classes.Integration> obj = new List<classes.Integration>();

            //integration.GetxInitialNodes();
            //integration.GetyInitialNodes();
            obj.Add(integration);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            views.Integration integration_xaml = new views.Integration();
            integration_xaml.ShowInTaskbar = false;
            integration_xaml.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            integration_xaml.ShowDialog();
        }

        private void ApproxMenuItem_Click(object sender, RoutedEventArgs e)
        {
            views.Approx approx_xaml = new views.Approx();
            approx_xaml.ShowInTaskbar = false;
            approx_xaml.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            approx_xaml.ShowDialog();
        }

        private void RootsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            views.Roots roots = new views.Roots();
            roots.ShowInTaskbar = false;
            roots.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            roots.ShowDialog();
        }

        private void MatrixMenuItem_Click(object sender, RoutedEventArgs e)
        {
            views.Matrices matrix = new views.Matrices();
            matrix.ShowInTaskbar = false;
            matrix.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            matrix.ShowDialog();
        }

        private void DerivMenuItem_Click(object sender, RoutedEventArgs e)
        {
            views.Derivation deriv = new views.Derivation();
            deriv.ShowInTaskbar = false;
            deriv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            deriv.ShowDialog();
        }
    }
}
