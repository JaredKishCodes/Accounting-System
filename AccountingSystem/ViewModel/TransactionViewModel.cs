using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.Interfaces;
using AccountingSystem.Models;
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
        private string searchTerm;

        public ObservableCollection<Transaction> Transactions { get; } = new();

        public TransactionViewModel(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
            Task.Run(LoadTransactionsAsync);

        }


        [RelayCommand]
        public async Task CreateTransactionsAsync(Transaction transaction)
        {
            await _transactionRepository.CreateTransactionAsync(transaction);
            await LoadTransactionsAsync();

            NewTransaction = new Transaction();
        }

        [RelayCommand]
        public async Task LoadTransactionsAsync()
        {
            Transactions.Clear();

            var result = await _transactionRepository.GetTransactionsAsync();

            foreach (var item in result)
            {
                Transactions.Add(item);
            }

        }

        [RelayCommand]
        public async Task SearchTransactionsAsync()
        {
            Transactions.Clear();
            var result = await _transactionRepository.SearchTransactionsAsync(searchTerm);
            foreach (var item in result)
            {
                Transactions.Add(item);
            }
        }
    }
}
