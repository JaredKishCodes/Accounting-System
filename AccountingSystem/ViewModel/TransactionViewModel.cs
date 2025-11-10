using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AccountingSystem.Interfaces;
using AccountingSystem.Models;
using AccountingSystem.Models.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace AccountingSystem.ViewModel
{
    public partial class TransactionViewModel : ObservableObject
    {
        private readonly ITransactionRepository _transactionRepository;

        [ObservableProperty]
        private Transaction newTransaction = new Transaction();

        [ObservableProperty]
        private DateTime? selectedDate;

        [ObservableProperty]
        private string searchTerm;

        [ObservableProperty]
        private string amountText = string.Empty;

        public ObservableCollection<Transaction> Transactions { get; } = new();
        
        public List<DebitAccount> DebitAccountOptions { get; } = Enum.GetValues(typeof(DebitAccount)).Cast<DebitAccount>().ToList();
        
        public List<CreditAccount> CreditAccountOptions { get; } = Enum.GetValues(typeof(CreditAccount)).Cast<CreditAccount>().ToList();

        public TransactionViewModel(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
            Task.Run(LoadTransactionsAsync);
            
            // Initialize date to today
            SelectedDate = DateTime.Today;
            
            // Sync selected date with transaction date and amount text
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectedDate))
                {
                    if (SelectedDate.HasValue)
                    {
                        NewTransaction.Date = DateOnly.FromDateTime(SelectedDate.Value);
                    }
                    else
                    {
                        NewTransaction.Date = null;
                    }
                }
                else if (e.PropertyName == nameof(AmountText))
                {
                    // Convert amount text to decimal (only update if parsing succeeds or is empty)
                    if (string.IsNullOrWhiteSpace(AmountText))
                    {
                        NewTransaction.Amount = 0;
                    }
                    else if (decimal.TryParse(AmountText, out decimal amount))
                    {
                        NewTransaction.Amount = amount;
                    }
                }
            };
        }


        [RelayCommand]
        public async Task CreateTransactionsAsync(Transaction transaction)
        {
            if (transaction == null) return;
            
            // Ensure date is set
            if (SelectedDate.HasValue && transaction.Date == null)
            {
                transaction.Date = DateOnly.FromDateTime(SelectedDate.Value);
            }
            
            // Ensure amount is set from AmountText
            if (decimal.TryParse(AmountText, out decimal amount))
            {
                transaction.Amount = amount;
            }
            else if (string.IsNullOrWhiteSpace(AmountText))
            {
                MessageBox.Show("Please enter an amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                MessageBox.Show("Please enter a valid amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            try
            {
                await _transactionRepository.CreateTransactionAsync(transaction);
                await LoadTransactionsAsync();

                // Reset form
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewTransaction = new Transaction();
                    SelectedDate = DateTime.Today;
                    AmountText = string.Empty;
                });
                
                MessageBox.Show("Transaction created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating transaction: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        public async Task LoadTransactionsAsync()
        {
            var result = await _transactionRepository.GetTransactionsAsync();

            // Update on UI thread
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Transactions.Clear();
                foreach (var item in result)
                {
                    Transactions.Add(item);
                }
            });
        }

        [RelayCommand]
        public async Task SearchTransactionsAsync()
        {
            var result = await _transactionRepository.SearchTransactionsAsync(searchTerm);
            
            // Update on UI thread
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Transactions.Clear();
                foreach (var item in result)
                {
                    Transactions.Add(item);
                }
            });
        }
    }
}
