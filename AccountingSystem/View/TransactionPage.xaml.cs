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
            var mainWindow = new MainWindow();
            var currentWindow = Window.GetWindow(this);
            mainWindow.Show();
            currentWindow?.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement transaction submission logic
            MessageBox.Show("Transaction submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
