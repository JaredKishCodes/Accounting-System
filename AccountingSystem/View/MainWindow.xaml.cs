using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AccountingSystem.View;

namespace AccountingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NewTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to TransactionPage
            var transactionPage = new TransactionPage();
            this.Content = transactionPage;
        }

        private void TransactionButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to TransactionPage
            var transactionPage = new TransactionPage();
            this.Content = transactionPage;
        }

        private void AccountsButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement Accounts page navigation
            MessageBox.Show("Accounts page - Coming soon!");
        }

        private void GeneralJournalButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement General Journal page navigation
            MessageBox.Show("General Journal page - Coming soon!");
        }

        private void LedgerButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement Ledger page navigation
            MessageBox.Show("Ledger page - Coming soon!");
        }

        private void BalanceSheetButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement Balance Sheet page navigation
            MessageBox.Show("Balance Sheet page - Coming soon!");
        }
    }
}