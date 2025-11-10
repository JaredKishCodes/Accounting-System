using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AccountingSystem.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingSystem.View
{
    /// <summary>
    /// Interaction logic for TransactionPage.xaml
    /// </summary>
    public partial class TransactionPage : Page
    {
        public TransactionPage()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<TransactionViewModel>();
            // Load transactions when page loads
            if (DataContext is TransactionViewModel viewModel)
            {
                Task.Run(async () => await viewModel.LoadTransactionsAsync());
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // For a Page, we need to get the parent Window to drag
                var window = Window.GetWindow(this);
                window?.DragMove();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window?.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to MainWindow
            var currentWindow = Window.GetWindow(this);
            var mainWindow = new MainWindow();
            mainWindow.Show();
            currentWindow?.Close();
        }
    }
}
